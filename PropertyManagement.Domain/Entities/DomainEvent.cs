using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Domain.Entities
{
    public class DomainEvent
    {
        public Guid Id { get; private set; }
        public Guid PropertyId { get; private set; }
        public string EventType { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public string PayloadJSON { get; private set; } = "{}";

        public Property? Property { get; private set; }

        protected DomainEvent() { }

    }
}
