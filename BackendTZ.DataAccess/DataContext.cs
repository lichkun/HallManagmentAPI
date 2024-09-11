using BackendTZ.Core.Models;
using BackendTZ.DataAccess.Configurations;
using BackendTZ.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTZ.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
        }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<ConferenceHallEntity> Halls { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new ConferenceHallConfiguration());

            modelBuilder.Entity<ConferenceHallEntity>().HasKey(c => c.Id);
            modelBuilder.Entity<BookingEntity>().HasKey(c => c.Id);
            modelBuilder.Entity<ServiceEntity>().HasKey(c => c.Id);

            modelBuilder.Entity<ConferenceHallEntity>().HasData(
                new ConferenceHallEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Зал А",
                    Capacity = 50,
                    BaseRate = 2000
                },
                new ConferenceHallEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Зал B",
                    Capacity = 100,
                    BaseRate = 3500
                },
                new ConferenceHallEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Зал C",
                    Capacity = 30,
                    BaseRate = 1500
                }
            );

            modelBuilder.Entity<ServiceEntity>().HasData(
                new ServiceEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Проєктор",
                    Price = 500
                },
                new ServiceEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Wi-Fi",
                    Price = 300
                },
                new ServiceEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Звук",
                    Price = 700
                }
            );
        }
    }
}
