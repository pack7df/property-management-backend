using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagement.Domain.DTO;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Abstractions.Repositories
{
    public interface IHostRepository
    {
        public Task<Host> AddAsync(Host parameters);
        public Task<bool> RemoveAsync(Guid id);

        public Task<Host?> UpdateAsync(Host parameters);

        public Task<PaginationResult<Host>> FilterAsync(FilterHostRequest parameters);
    }
}
