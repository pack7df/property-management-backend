using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Domain.Abstractions;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Infrastructure.Persistence
{
    public class SeedGenerator
    {
        private CustomDbContext dbContext;
        private ISecurityServices securityServices;
        private IHostServices hostServices;
        private IPropertyServices propertyServices;
        public SeedGenerator(CustomDbContext dbContext, ISecurityServices securityServices, IHostServices hostServices, IPropertyServices propertyServices)
        {
            this.dbContext = dbContext;
            this.securityServices = securityServices;
            this.hostServices = hostServices;
            this.propertyServices = propertyServices;
        }

        public async Task GenerateAsync()
        {
            await dbContext.Database.MigrateAsync();
            var users = dbContext.Users.ToList();
            foreach (var u in users) dbContext.Users.Remove(u);
            dbContext.SaveChanges();

            var hosts = dbContext.Hosts.ToList();
            foreach (var h in hosts)
            {
                dbContext.Hosts.Remove(h);
            }

            dbContext.SaveChanges();


            await securityServices.RegisterAsync(new User
            {
                Email = "emailTest@gmail.com",
                Username = "userTest"
            }, "1234567");

            dbContext.SaveChanges();

            for (var i = 0; i < 50; i++)
            {

                var host = new Host
                {
                    Email = $"HostEmail{i}@gmail.com",
                    FullName = $"HostName {i}",
                    Phone = $"1234{i}",
                };
                host = await hostServices.CreateAsync(host);
                dbContext.SaveChanges();
                for (var j = 0; j < 50; j++)
                {
                    var property = new Property
                    {
                        HostId = host.Id,
                        Location = $"Location Test {i * 100 + j}",
                        Name = $"Property Name {i * 100 + j}",
                        PricePerNight = j * 10,
                    };
                    property = await propertyServices.CreateAsync(property);
                }
                dbContext.SaveChanges();
            }
        }
    }
}
