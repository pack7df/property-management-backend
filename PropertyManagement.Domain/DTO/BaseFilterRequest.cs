using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagement.Domain.Abstractions.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace PropertyManagement.Domain.DTO
{
    public class BaseFilterRequest 
    {

        public SortOrder Order { get; set; } = SortOrder.Ascending;
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 20;

        [SwaggerSchema("OrderBy. Posible values: 'Email', 'FullName', 'Phone' for Host, and 'Location', 'PricePerNight', 'Status', CreatedAt' for Properties")]
        public string OrderBy { get; set; } = "";
    }
}
