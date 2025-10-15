using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.DTO
{
    public class BookingRequest
    {
        public Guid PropertyId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

    }
}
