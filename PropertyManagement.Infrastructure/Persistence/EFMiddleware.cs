using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PropertyManagement.Infrastructure.Persistence
{
    /// <summary>
    /// Middleware to save change only if there is no error. It ensure a transaction per request.
    /// </summary>
    public class EFMiddleware
    {
        private readonly RequestDelegate _next;

        public EFMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, CustomDbContext dbContext)
        {
            await _next(context);
            if (dbContext.ChangeTracker.HasChanges())
                await dbContext.SaveChangesAsync();
        }
    }
}
