using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; private set; }
        public Guid PropertyId { get; private set; }
        public DateTime CheckIn { get; private set; }
        public DateTime CheckOut { get; private set; }
        public decimal TotalPrice { get; private set; }

        public Property? Property { get; private set; }

        protected Booking() { }

    }
}
