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
    public class BookingService : IService<Booking>
    {
        public readonly IRepository<Booking> _bookingrepository;
        public BookingService(IRepository<Booking> bookingrepository)
        {
            _bookingrepository = bookingrepository;
        }
        public async Task<List<Booking>> GetAll()
        {
            return await _bookingrepository.GetAll();
        }
        public async Task<Booking> GetById(Guid id)
        {
            return await _bookingrepository.GetById(id);
        }
        public async Task<Guid> Create(Booking service)
        {
            return await _bookingrepository.Create(service);
        }
        public async Task<Guid> Delete(Guid id)
        {
            return await _bookingrepository.Delete(id);
        }
        public async Task<Guid> Update(Booking service)
        {
            return await _bookingrepository.Update(service);
        }
        
    }
}
