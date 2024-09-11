using BackendTZ.Core.Models;
using BackendTZ.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTZ.DataAccess.Repositories
{
    public class ServiceRepository : IRepository<Service>
    {
        private readonly DataContext _context;
        public ServiceRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Service>> GetAll()
        {
            var serviceEntities = await _context.Services
                .AsNoTracking()
                .ToListAsync();
            var services  = serviceEntities
                .Select(s=> Service.Create(s.Id,s.Name,s.Price).Service)
                .ToList();
            return services;
        }
        public async Task<Service> GetById(Guid id)
        {
            var serviceEntity =  await _context.Services
                .Where(s => s.Id == id).SingleAsync();
            var service = Service.Create(serviceEntity.Id, serviceEntity.Name, serviceEntity.Price).Service;
            return service;
        }
        public async Task<Guid> Create(Service service)
        {
            var serviceEntity = new ServiceEntity
            {
                Id = service.Id,
                Name = service.Name,
                Price = service.Price,
            };

            await _context.Services.AddAsync(serviceEntity);
            await _context.SaveChangesAsync();

            return service.Id;
        }
        public async Task<Guid> Update(Service service)
        {
            await _context.Services
                .Where(s => s.Id == service.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(s => s.Name, s => service.Name)
                    .SetProperty(s => s.Price, s => service.Price));
            return service.Id;
        }
        public async Task<Guid> Delete(Guid id)
        {
            await _context.Services
                .Where (s => s.Id == id)
                .ExecuteDeleteAsync();
            return id;
        }
    }
}
