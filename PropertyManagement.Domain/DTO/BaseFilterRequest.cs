using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagement.Domain.Abstractions.Repositories;

namespace PropertyManagement.Domain.DTO
{
    public class BaseFilterRequest : PaginationParams
    {
        public string OrderBy { get; set; }
    }
}
