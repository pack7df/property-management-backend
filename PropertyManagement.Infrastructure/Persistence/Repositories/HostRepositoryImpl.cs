using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            var pageParameters = new PaginationParams<Host, string>();
            var email = parameters.Email?.ToLower() ?? "";
            var fullName = parameters.FullName?.ToLower() ?? "";
            var phone = parameters.Phone?.ToLower() ?? "";
            //TODO: optimized later
            pageParameters.filter = (h) => h.Email.ToLower().Contains(email)
                                        && h.FullName.ToLower().Contains(fullName)
                                        && h.Phone.ToLower().Contains(phone);

            pageParameters.Order = parameters.Order;
            pageParameters.PageIndex = parameters.PageIndex;
            pageParameters.PageSize = parameters.PageSize;
            if (parameters.OrderBy.ToLower() == "email")
                pageParameters.OrderBy = (h => h.Email);
            if (parameters.OrderBy.ToLower() == "fullname")
                pageParameters.OrderBy = (h => h.FullName);
            if (parameters.OrderBy.ToLower() == "phone")
                pageParameters.OrderBy = (h => h.Phone);
            return await dbContext.Hosts.GetPagedQueryAsync(pageParameters);
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
