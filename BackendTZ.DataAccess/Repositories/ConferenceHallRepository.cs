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
    public class ConferenceHallRepository : IRepository<ConferenceHall>
    {
        private readonly DataContext _context;

        public ConferenceHallRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ConferenceHall>> GetAll()
        {
            var conferenceHallEntities = await _context.Halls
                .Include(ch => ch.Services)
                .AsNoTracking()
                .ToListAsync();

            var conferenceHalls = conferenceHallEntities
                .Select(ch => ConferenceHall.Create(
                    ch.Id,
                    ch.Name,
                    ch.Capacity,
                    ch.BaseRate,
                    ch.Services.Select(s => s.Id).ToList()).hall)
                .ToList();

            return conferenceHalls;
        }

        public async Task<ConferenceHall> GetById(Guid id)
        {
            var conferenceHallEntity = await _context.Halls
                .Include(ch => ch.Services)
                .Where(ch => ch.Id == id)
                .SingleAsync();

            var conferenceHall = ConferenceHall.Create(
                conferenceHallEntity.Id,
                conferenceHallEntity.Name,
                conferenceHallEntity.Capacity,
                conferenceHallEntity.BaseRate,
                conferenceHallEntity.Services.Select(s => s.Id).ToList()).hall;

            return conferenceHall;
        }

        public async Task<Guid> Create(ConferenceHall conferenceHall)
        {
            var conferenceHallEntity = new ConferenceHallEntity
            {
                Id = conferenceHall.Id,
                Name = conferenceHall.Name,
                Capacity = conferenceHall.Capacity,
                BaseRate = conferenceHall.BaseRate,
                Services = await _context.Services
                    .Where(s => conferenceHall.ServicesIds.Contains(s.Id))
                    .ToListAsync()
            };

            await _context.Halls.AddAsync(conferenceHallEntity);
            await _context.SaveChangesAsync();

            return conferenceHall.Id;
        }

        public async Task<Guid> Update(ConferenceHall conferenceHall)
        {
            var services = await _context.Services
                .Where(s => conferenceHall.ServicesIds.Contains(s.Id))
                .ToListAsync();

            await _context.Halls
                .Where(ch => ch.Id == conferenceHall.Id)
                .ExecuteUpdateAsync(ch => ch
                    .SetProperty(ch => ch.Name, ch => conferenceHall.Name)
                    .SetProperty(ch => ch.Capacity, ch => conferenceHall.Capacity)
                    .SetProperty(ch => ch.BaseRate, ch => conferenceHall.BaseRate)
                    .SetProperty(ch => ch.Services, ch => services));

            return conferenceHall.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Halls
                .Where(ch => ch.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
