using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagement.Domain.Abstractions.Repositories;

namespace PropertyManagement.Domain.DTO
{
    public class BaseFilterRequest 
    {
        public SortOrder? Order { get; set; } = SortOrder.Ascending;
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 20;
        public string OrderBy { get; set; } = "";
    }
}
