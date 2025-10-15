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
        public Guid Id { get; set; }
        public Guid HostId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }
        public PropertyStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Host Host { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public virtual ICollection<DomainEvent> Events { get; set; } = new List<DomainEvent>();

        public Property() { }

    }


    
}
