using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetId(int id);

        Task<T> Create(T entity);

        Task<T> Update(int id, T newValue);

        Task<bool> Delete(int id);

    }
}
