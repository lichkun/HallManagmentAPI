using BackendTZ.Core.Abstractions;
using BackendTZ.Core.Models;
using BackendTZ.DataAccess.Repositories;

namespace BackendTZ.Application.Services
{
    public class ServService : IService<Service>
    {
        public readonly IRepository<Service> _servicerepository;
        public ServService(IRepository<Service> servicerepository) 
        { 
            _servicerepository = servicerepository;
        }
        public async Task<List<Service>> GetAll()
        {
            return await _servicerepository.GetAll();
        }
        public async Task<Service> GetById(Guid id)
        {
            return await _servicerepository.GetById(id);
        }
        public async Task<Guid> Create(Service service)
        {
            return await _servicerepository.Create(service);
        }
        public async Task<Guid> Delete(Guid id)
        {
            return await _servicerepository.Delete(id);
        }
        public async Task<Guid> Update(Service service)
        {
            return await _servicerepository.Update(service);
        }
    }
}
