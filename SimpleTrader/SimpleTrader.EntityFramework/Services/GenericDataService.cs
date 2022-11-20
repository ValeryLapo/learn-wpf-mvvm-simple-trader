using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;

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

        private SimpleTraderDbContextFactory _contextFactory;

        public GenericDataService(SimpleTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
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
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();
            
            EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();

            return createdResult.Entity;
        }

        public async Task<T> Update(int id, T entity)
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();

            entity.Id = id;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();

            return entity;
        }
        

        public async Task<bool> Delete(int id)
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();

            T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            context.Set<T>().Remove(entity);

            await context.SaveChangesAsync();

            return true;
        }
    }

}
