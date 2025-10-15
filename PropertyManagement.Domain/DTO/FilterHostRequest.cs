using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Domain.DTO
{
    public class FilterHostRequest : BaseFilterRequest
    {
        public FilterHostRequest()
        {
            this.OrderBy = "FullName";
        }
        public string? FullName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
    }
}
