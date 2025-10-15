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
        private IOTAManager otaManager;

        public PropertyServicesImpl(IPropertyRepository propertyRepository, IOTAManager otaManager)
        {
            this.propertyRepository = propertyRepository;
            this.otaManager = otaManager;
        }

        public async Task<Booking?> BookingAsync(BookingRequest data)  
        {
            var property = await propertyRepository.FetchAsync(data.PropertyId);
            if (property == null) return null;
            var totalPrice = property.PricePerNight * (decimal)((data.CheckOut - data.CheckIn).TotalDays);
            var newBooking = new Booking
            {
                CheckIn = data.CheckIn,
                CheckOut = data.CheckOut,
                Property = property,
                PropertyId = property.Id,
                TotalPrice = totalPrice,
            };
            return await propertyRepository.AddAsync(newBooking);
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

        public async Task<DomainEvent?> SyncronizeAsync(Guid id)
        {
            var propertyEvent = await otaManager.SyncronizeAsync(id);
            if (propertyEvent == null) return null;
            return await propertyRepository.AddAsync(propertyEvent);
        }

        public async Task<Property?> UpdateAsync(Property data)
        {
            return await propertyRepository.UpdateAsync(data);
        }
    }
}
