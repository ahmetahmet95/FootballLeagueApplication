
using DataAccess.Interface;
using DataAccessLibrary;
using DataAccessLibrary.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class EfRepository<T> : IRepository<T> where T : class, new()
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EfRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _dbSet.ToListAsync();
            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
           var result =  await _dbSet.FindAsync(id);
            return result;
        }

        public T Update(T entity)
        {    

            var entry = _dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            return entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<int> Save()
        {
            return _dbContext.SaveChanges();

        }
    }
}