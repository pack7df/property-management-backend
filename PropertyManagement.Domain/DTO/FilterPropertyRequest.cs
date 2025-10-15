using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.DTO
{
    public class FilterPropertyRequest : BaseFilterRequest
    {
        public Guid? HostId { get; set; }
        public string? Location { get; set; }
        public decimal? PricePerNightMin { get; set; }
        public decimal? PricePerNightMax { get; set; }
        public PropertyStatus? Status { get; set; }
        public DateTime? CreatedAtMin { get; set; }
        public DateTime? CreatedAtMax { get; set; }
    }
}
