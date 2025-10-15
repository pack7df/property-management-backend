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
    public interface IPropertyServices
    {
        public Task<PaginationResult<Property>> FilterAsync(FilterPropertyRequest parameters);
        public Task<Property?> CreateAsync(Property data);
        public Task<bool> DeleteAsync(Guid id);
        public Task<Property?> UpdateAsync(Property data);

        public Task<Booking?> BookingAsync(BookingRequest data);

        public Task<DomainEvent?> SyncronizeAsync(Guid id);
    }
}
