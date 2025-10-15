using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagement.Domain.Abstractions;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Infrastructure.Persistence;

namespace PropertyManagement.Infrastructure.ThirdParty
{
    public class OTAManagerMock : IOTAManager
    {
        public async Task<DomainEvent> SyncronizeAsync(Guid propertyId)
        {
            //Simulates a connection wait time
            await Task.Delay(500);
            return await Task.FromResult(new DomainEvent
            {
                CreatedAt = DateTime.Now,
                EventType = "EventType" ,
                PayloadJSON = "{}",
                PropertyId = propertyId
            });
        }
    }
}
