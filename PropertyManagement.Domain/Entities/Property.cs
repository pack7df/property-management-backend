using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Domain.Entities
{
    public enum PropertyStatus
    {
        Active,
        Inactive
    }

    public class Property
    {
        public Guid Id { get; private set; }
        public Guid HostId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Location { get; private set; } = string.Empty;
        public decimal PricePerNight { get; private set; }
        public PropertyStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Host? Host { get; private set; }
        public ICollection<Booking> Bookings { get; private set; } = new List<Booking>();

        protected Property() { }

    }


    
}
