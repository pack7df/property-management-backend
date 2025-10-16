using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PropertyManagement.Domain.Abstractions;
using PropertyManagement.Domain.Abstractions.Repositories;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Infrastructure
{
    public class SecurityServiceImpl : ISecurityServices
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor contextAccesor;

        public SecurityServiceImpl(IUserRepository userRepository, IConfiguration configuration, IHttpContextAccessor contextAccesor)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
            this.contextAccesor = contextAccesor;
        }
        public string GenerateToken(User user)
        {
            var jwtConfig = configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: jwtConfig["Issuer"],
                audience: jwtConfig["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string?> AuthenticateAsync(string userName, string password)
        {
            var user = await userRepository.FetchByUserNameAsync(userName);
            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return GenerateToken(user);
        }

        public async Task<User?> GetCurrentUserAsync()
        {
            var userId = contextAccesor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ?? null;
            if (userId == null) return null;
            var id = Guid.Parse(userId ?? "");
            return await userRepository.FetchAsync(id);
        }

        public async Task<User?> RegisterAsync(User parameters, string password)
        {
            if (string.IsNullOrEmpty(parameters.Username)) return null;
            if (string.IsNullOrEmpty(parameters.Email)) return null;
            var currentUser = await userRepository.FetchByUserNameAsync(parameters.Username);
            if (currentUser != null) return null;
            currentUser = await userRepository.FetchByUserNameAsync(parameters.Email);
            if (currentUser != null) return null;
            return await userRepository.AddAsync(new User
            {
                Email = parameters.Email,
                Username = parameters.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            });
        }
    }
}
