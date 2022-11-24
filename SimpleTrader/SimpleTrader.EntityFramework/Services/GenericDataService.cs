using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework.Services.Common;

namespace SimpleTrader.EntityFramework.Services
{
    //this class is going to handle all of out crud operations
    //for all of our domain objects. And we wanna handle them in only 
    //one generic class
    public class GenericDataService<T>: IDataService<T> where T : DomainObject
    {
        //Many people do just like this, create:
        //private readonly SimpleTraderDbContext _context; <- field
        //and then inside methods uses this field to operate this database
        //But this man won't do this. He want to create new dbContext every
        //time he uses methods
        //The reason - he don't want to use the same
        //context because one thing about Entity
        //framework dbContext it is not Thread safe
        //so if multiple threads are trying to use the
        //same context and the same time, you gonna
        //fave some funky things, ypu may exceptions
        //So we are go to use our DbContextFactory
        //to create a new context for each operation

        private readonly SimpleTraderDbContextFactory _contextFactory;
        private readonly NonQueryDataService<T> _nonQueryDataService;

        public GenericDataService(SimpleTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<T>(contextFactory);
        }

        public  async Task<IEnumerable<T>> GetAll()
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();

            IEnumerable<T> entities = await context.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<T> Get(int id)
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();

            T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            return entity;
        }

        public async Task<T> Create(T entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<T> Update(int id, T entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
        

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }
    }

}
