using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Domain.Abstractions.Repositories;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Infrastructure.Persistence.Repositories
{
    public class UserRepositoryImpl : IUserRepository
    {
        private CustomDbContext dbContext;
        public UserRepositoryImpl(CustomDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<User?> AddAsync(User parameters)
        {
            var newUser = new User
            {
                Email = parameters.Email,
                Id = Guid.NewGuid(),
                PasswordHash = parameters.PasswordHash,
                Username = parameters.Username
            };
            dbContext.Users.Add(newUser);
            return newUser;
        }

        public async Task<User?> FetchAsync(Guid id)
        {
            return  await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> FetchByEmailAsync(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> FetchByUserNameAsync(string userName)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Username == userName);
        }
    }
}
