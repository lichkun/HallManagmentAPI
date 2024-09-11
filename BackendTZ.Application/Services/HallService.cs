using BackendTZ.Core.Abstractions;
using BackendTZ.Core.Models;
using BackendTZ.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTZ.Application.Services
{
    public class HallService : IService<ConferenceHall>
    {
        private readonly IRepository<ConferenceHall> _repository;

        public HallService(IRepository<ConferenceHall> repository)
        {
            _repository = repository;
        }

        public async Task<List<ConferenceHall>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ConferenceHall> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Guid> Create(ConferenceHall entity)
        {
            return await _repository.Create(entity);
        }

        public async Task<Guid> Update(ConferenceHall entity)
        {
            return await _repository.Update(entity);
        }

        public async Task<Guid> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}
