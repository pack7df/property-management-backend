using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagement.Domain.DTO;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Abstractions.Repositories
{
    public interface IPropertyRepository
    {
        public Task<Property?> AddAsync(Property parameters);
        public Task<bool> RemoveAsync(Guid id);

        public Task<Property?> UpdateAsync(Property parameters);

        public Task<PaginationResult<Property>> FilterAsync(FilterPropertyRequest parameters);
    }
}
