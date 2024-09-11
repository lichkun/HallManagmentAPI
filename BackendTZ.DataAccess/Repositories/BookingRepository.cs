using BackendTZ.Core.Models;
using BackendTZ.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTZ.DataAccess.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        private readonly DataContext _context;

        public BookingRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetAll()
        {
            var bookingEntities = await _context.Bookings
                .Include(b => b.ConferenceHall)
                .Include(b => b.Services)
                .AsNoTracking()
                .ToListAsync();

            var bookings = bookingEntities
                .Select(b => Booking.Create(
                    b.Id,
                    b.StartTime,
                    b.EndTime,
                    b.ConferenceHallId,
                    b.Services.Select(s => s.Id).ToList(),
                    b.TotalPrice).hall)
                .ToList();

            return bookings;
        }

        public async Task<Booking> GetById(Guid id)
        {
            var bookingEntity = await _context.Bookings
                .Include(b => b.ConferenceHall)
                .Include(b => b.Services)
                .Where(b => b.Id == id)
                .SingleAsync();

            var booking = Booking.Create(
                bookingEntity.Id,
                bookingEntity.StartTime,
                bookingEntity.EndTime,
                bookingEntity.ConferenceHallId,
                bookingEntity.Services.Select(s => s.Id).ToList(), 
                bookingEntity.TotalPrice).hall;

            return booking;
        }

        public async Task<Guid> Create(Booking booking)
        {
            var bookingEntity = new BookingEntity
            {
                Id = booking.Id,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                ConferenceHallId = booking.ConferenceHallId,
                Services = await _context.Services
                    .Where(s => booking.ServicesIds.Contains(s.Id))
                    .ToListAsync(),
                TotalPrice = booking.TotalPrice
            };

            await _context.Bookings.AddAsync(bookingEntity);
            await _context.SaveChangesAsync();

            return booking.Id;
        }

        public async Task<Guid> Update(Booking booking)
        {
            var services = await _context.Services
                .Where(s => booking.ServicesIds.Contains(s.Id))
                .ToListAsync();

            await _context.Bookings
                .Where(b => b.Id == booking.Id)
                .ExecuteUpdateAsync(b => b
                    .SetProperty(b => b.StartTime, b => booking.StartTime)
                    .SetProperty(b => b.EndTime, b => booking.EndTime)
                    .SetProperty(b => b.ConferenceHallId, b => booking.ConferenceHallId)
                    .SetProperty(b => b.Services, b => services)
                    .SetProperty(b => b.TotalPrice, b => booking.TotalPrice));

            return booking.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Bookings
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }

}
