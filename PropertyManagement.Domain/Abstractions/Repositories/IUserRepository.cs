using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Abstractions.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> FetchAsync(Guid id);
        public Task<User?> FetchByUserNameAsync(string userName);

        public Task<User?> FetchByEmailAsync(string email);

        public Task<User?> AddAsync(User parameters);
    }
}
