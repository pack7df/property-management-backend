using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Domain.Entities
{
    public class DomainEvent
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public string EventType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string PayloadJSON { get;  set; } = "{}";

        public virtual Property Property { get; set; }

        public DomainEvent() { }

    }
}
