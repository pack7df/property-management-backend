using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Domain.Abstractions.Repositories;

namespace PropertyManagement.Infrastructure.Persistence
{
    public static class QueryExtensions
    {

        public static async Task<PaginationResult<T>> GetPagedQueryAsync<T, TKey>(this IQueryable<T> query, PaginationParams parameters, Expression<Func<T, TKey>> OrderBy, CancellationToken token = default)
        {
            var orderByQ = parameters.Order == SortOrder.Ascending ? query.OrderBy(OrderBy) : query.OrderByDescending(OrderBy);
            return new PaginationResult<T>
            {
                Total = orderByQ.Count(),
                Items = await orderByQ.Skip(parameters.PageIndex * parameters.PageSize).Take(parameters.PageSize).ToListAsync(token),
            };
        }
    }
}
