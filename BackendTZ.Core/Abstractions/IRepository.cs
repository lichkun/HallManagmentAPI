using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTZ.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<Guid> Create(T enitty);
        Task<Guid> Update(T enitty);
        Task<Guid> Delete(Guid id);
    }
}
