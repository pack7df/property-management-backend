using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Infrastructure.Persistence
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options)
           : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<DomainEvent> Events { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Host>()
                .HasMany(h => h.Properties)
                .WithOne(p => p.Host)
                .HasForeignKey(p => p.HostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Property>()
                .HasMany(p => p.Bookings)
                .WithOne(b => b.Property)
                .HasForeignKey(b => b.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
