using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Domain;
using PropertyManagement.Domain.Abstractions.Repositories;
using PropertyManagement.Domain.DTO;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Infrastructure.Persistence.Repositories
{
    public class HostRepositoryImpl : IHostRepository
    {
        private CustomDbContext dbContext;
        public HostRepositoryImpl(CustomDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Host> AddAsync(Host parameters)
        {
            var newHost = new Host
            {
                Email = parameters.Email,
                FullName = parameters.FullName,
                Phone = parameters.Phone,
                Id = Guid.NewGuid()
            };
            await dbContext.Hosts.AddAsync(newHost);
            return newHost;
        }

        public async Task<PaginationResult<Host>> FilterAsync(FilterHostRequest parameters)
        {
            var allQ = dbContext.Hosts;
            
            var email = parameters.Email?.ToLower() ?? "";
            var fullName = parameters.FullName?.ToLower() ?? "";
            var phone = parameters.Phone?.ToLower() ?? "";
            var q = string.IsNullOrEmpty(email) ? allQ : allQ.Where(h => h.Email.ToLower().Contains(email));
            q = string.IsNullOrEmpty(fullName) ? q: q.Where(h => h.FullName.ToLower().Contains(fullName));
            q = string.IsNullOrEmpty(phone) ? q : q.Where(h => h.Phone.ToLower().Contains(phone));

            var pageParameters = new PaginationParams();
            pageParameters.Order = parameters.Order;
            pageParameters.PageIndex = parameters.PageIndex;
            pageParameters.PageSize = parameters.PageSize;
            switch (parameters.OrderBy)
            {
                case nameof(Host.Email):
                    {
                        return await q.GetPagedQueryAsync(pageParameters, h => h.Email);
                    }
                case nameof(Host.FullName):
                    {
                        return await q.GetPagedQueryAsync(pageParameters, h => h.FullName);
                    }
                case nameof(Host.Phone):
                    {
                        return await q.GetPagedQueryAsync(pageParameters, h => h.Phone);
                    }
                default:
                    {
                        throw new OrderParameterNotRecognizedException(typeof(Host), parameters.OrderBy);
                    }
            }
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var entity = await dbContext.Hosts.Where(h => h.Id == id).FirstOrDefaultAsync();
            if (entity == null) return false;
            dbContext.Hosts.Remove(entity);
            return true;
        }

        public async Task<Host?> UpdateAsync(Host parameters)
        {
            var entity = await dbContext.Hosts.Where(h => h.Id == parameters.Id).FirstOrDefaultAsync();
            if (entity == null) return null;
            entity.Email = parameters.Email;
            entity.Phone = parameters.Phone;
            entity.FullName = parameters.FullName;
            return entity;
        }
    }
}
