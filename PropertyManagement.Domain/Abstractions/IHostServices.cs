using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagement.Domain.Abstractions.Repositories;
using PropertyManagement.Domain.DTO;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Abstractions
{
    public interface IHostServices
    {
        public Task<PaginationResult<Host>> FilterAsync(FilterHostRequest parameters);
        public Task<Host> CreateAsync(Host data);
        public Task<bool> DeleteAsync(Guid id);
        public Task<Host?> UpdateAsync(Host data);
    }
}
