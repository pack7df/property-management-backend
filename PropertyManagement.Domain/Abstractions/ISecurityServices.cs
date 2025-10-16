using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Abstractions
{
    public interface ISecurityServices
    {
        public Task<string?> AuthenticateAsync(string userName, string password);
        public Task<User?> GetCurrentUserAsync();

        public Task<User?> RegisterAsync(User parameters, string password);
    }
}
