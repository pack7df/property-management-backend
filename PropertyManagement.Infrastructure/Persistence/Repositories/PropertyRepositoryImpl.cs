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
    public class PropertyRepositoryImpl : IPropertyRepository
    {
        private CustomDbContext dbContext;
        public PropertyRepositoryImpl(CustomDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Property?> AddAsync(Property parameters)
        {
            var host = await dbContext.Hosts.FirstOrDefaultAsync(h => h.Id == parameters.HostId);
            if (host == null) return null;
            var newProperty = new Property
            {
                CreatedAt = parameters.CreatedAt,
                Host = host,
                HostId = parameters.HostId,
                Id = Guid.NewGuid(),
                Location = parameters.Location,
                Name = parameters.Name,
                PricePerNight = parameters.PricePerNight,
                Status = parameters.Status
            };
            dbContext.Properties.Add(newProperty);
            return newProperty;
        }

        public async Task<Booking?> AddAsync(Booking booking)
        {
            var property = await FetchAsync(booking.PropertyId);
            if (property == null) return null;
            var newBooking = new Booking
            {
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                Property = property,
                PropertyId = property.Id,
                TotalPrice = booking.TotalPrice,
                Id = Guid.NewGuid()
            };
            dbContext.Bookings.Add(newBooking);
            return newBooking;
        }

        public async Task<DomainEvent?> AddAsync(DomainEvent @event)
        {
            var property = await FetchAsync(@event.PropertyId);
            if (property == null) return null;
            var newEvent = new DomainEvent
            {
                CreatedAt = @event.CreatedAt,
                EventType = @event.EventType,
                Id = Guid.NewGuid(),
                PayloadJSON = @event.PayloadJSON,
                PropertyId = @event.PropertyId,
                Property = @event.Property
            };
            dbContext.Events.Add(newEvent);
            return newEvent;
        }

        public async Task<Property?> FetchAsync(Guid id)
        {
            return await dbContext.Properties.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PaginationResult<Property>> FilterAsync(FilterPropertyRequest parameters)
        {
            //TODO: optimized later
            var allQ = dbContext.Properties;
            var q = (parameters.CreatedAtMax != null) ? allQ.Where(p => p.CreatedAt <= parameters.CreatedAtMax) : allQ;
            q = (parameters.CreatedAtMin != null) ? q.Where(p => p.CreatedAt >= parameters.CreatedAtMin) : q;
            q = (parameters.HostId != null) ? q.Where(p => p.HostId == parameters.HostId) : q;
            q = (parameters.Location != null) ? q.Where(p => p.Location.ToLower().Contains(parameters.Location.ToLower())) : q;
            q = (parameters.PricePerNightMax != null) ? q.Where(p => p.PricePerNight<=parameters.PricePerNightMax) : q;
            q = (parameters.PricePerNightMin != null) ? q.Where(p => p.PricePerNight >= parameters.PricePerNightMin) : q;
            q = (parameters.Status != null) ? q.Where(p => p.Status == parameters.Status) : q;

            var pageParameters = new PaginationParams
            {
                Order = parameters.Order,
                PageIndex = parameters.PageIndex,
                PageSize = parameters.PageSize
            };

            switch (parameters.OrderBy)
            {
                case (nameof(Property.Location)):
                    {
                        return await q.GetPagedQueryAsync(pageParameters, p => p.Location);
                    }
                case (nameof(Property.Status)):
                    {
                        return await q.GetPagedQueryAsync(pageParameters, p=>p.Status);
                    }
                case (nameof(Property.CreatedAt)):
                    {
                        return await q.GetPagedQueryAsync(pageParameters, p=>p.CreatedAt);
                    }
                case (nameof(Property.Name)):
                    {
                        return await q.GetPagedQueryAsync(pageParameters, p=>p.Name);
                    }
                case (nameof(Property.PricePerNight)):
                    {
                        return await q.GetPagedQueryAsync(pageParameters, p=>p.PricePerNight);
                    }
                default:
                    {
                        throw new OrderParameterNotRecognizedException(typeof(Property), parameters.OrderBy);
                    }
            }
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var property = await dbContext.Properties.FirstOrDefaultAsync(p => p.Id == id);
            if (property == null) return false;
            dbContext.Properties.Remove(property);
            return true;
        }

        public async Task<Property?> UpdateAsync(Property parameters)
        {
            var property = await dbContext.Properties.FirstOrDefaultAsync(p => p.Id == parameters.Id);
            if (property == null) return null;
            property.PricePerNight = parameters.PricePerNight;
            property.Location = parameters.Location;
            property.Status = parameters.Status;
            property.Name = parameters.Name;
            return property;
        }
    }
}
