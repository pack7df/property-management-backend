using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Domain.Abstractions.Repositories;

namespace PropertyManagement.Infrastructure.Persistence
{
    public static class QueryExtensions
    {

        public static async Task<PaginationResult<T>> GetPagedQueryAsync<T, TKey>(this IQueryable<T> query, PaginationParams<T, TKey> parameters, CancellationToken token = default)
        {
            var filterQ = (parameters.filter == null) ? query : query.Where(parameters.filter);
            if (parameters.OrderBy == null) return await filterQ.GetPagedQueryAsync(parameters as PaginationParams);
            var orderByQ = parameters.Order == SortOrder.Ascending ? filterQ.OrderBy(parameters.OrderBy) : filterQ.OrderByDescending(parameters.OrderBy);
            return new PaginationResult<T>
            {
                Total = orderByQ.Count(),
                Items = await orderByQ.Skip(parameters.PageIndex * parameters.PageSize).Take(parameters.PageSize).ToListAsync(token),
            };
        }

        public static async Task<PaginationResult<T>> GetPagedQueryAsync<T>(this IQueryable<T> query, PaginationParams parameters, CancellationToken token = default)
        {
            return new PaginationResult<T>
            {
                Total = query.Count(),
                Items = await query.Skip(parameters.PageIndex * parameters.PageSize).Take(parameters.PageSize).ToListAsync(token)
            };
        }

    }
}
