using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Domain.Abstractions.Repositories
{
    public enum SortOrder
    {
        Ascending,
        Descending
    }
    public class PaginationParams
    {
        public SortOrder Order { get; set; } = SortOrder.Ascending;
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 20;
    }

    public class PaginationParams<T, TKey> : PaginationParams
    {
        public Expression<Func<T, TKey>>? OrderBy { get; set; } = null;
        public Expression<Func<T, bool>>? filter { get; set; } = null;
    }


    public class PaginationResult<T>
    {
        public int Total { get; set; }
        public List<T> Items { get; set; } = [];
    }
}
