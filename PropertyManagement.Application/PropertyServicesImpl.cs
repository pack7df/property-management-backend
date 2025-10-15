using PropertyManagement.Domain.Abstractions;
using PropertyManagement.Domain.Abstractions.Repositories;
using PropertyManagement.Domain.DTO;
using PropertyManagement.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PropertyManagement.Application
{
    public class PropertyServicesImpl : IPropertyServices
    {
        private IPropertyRepository propertyRepository;

        public PropertyServicesImpl(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task<Booking?> BookingAsync(Booking data)
        {
            throw new NotImplementedException();
        }

        public async Task<Property?> CreateAsync(Property data)
        {
            data.Status = PropertyStatus.Active;
            data.CreatedAt = DateTime.Now;
            return await propertyRepository.AddAsync(data);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await propertyRepository.RemoveAsync(id);
        }

        public async Task<PaginationResult<Property>> FilterAsync(FilterPropertyRequest parameters)
        {
            return await propertyRepository.FilterAsync(parameters);
        }

        public async Task<Property?> UpdateAsync(Property data)
        {
            return await propertyRepository.UpdateAsync(data);
        }
    }
}
