using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PropertyManagement.Domain.Abstractions;
using PropertyManagement.Domain.Abstractions.Repositories;
using PropertyManagement.Domain.DTO;
using PropertyManagement.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PropertyManagement.Application
{
    public class HostServicesImpl : IHostServices
    {
        private readonly IHostRepository repository;
        public HostServicesImpl(IHostRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Host> CreateAsync(Host data)
        {
            return await repository.AddAsync(data);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await repository.RemoveAsync(id);
        }

        public async Task<PaginationResult<Host>> FilterAsync(FilterHostRequest parameters)
        {
            return await repository.FilterAsync(parameters);
        }

        public async Task<Host?> UpdateAsync(Host data)
        {
            return await repository.UpdateAsync(data);
        }
    }
}
