using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Abstractions
{
    public interface IOTAManager
    {
        public Task<DomainEvent> SyncronizeAsync(Guid propertyId);
    }
}
