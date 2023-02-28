﻿
using DataAccess.Interface;
using DataAccessLibrary;
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

        public async Task<T> UpdateAsync(T entity)
        {
            EntityEntry entityEntry = _dbContext.Entry<T>(entity);
            EntityState entityState = entityEntry.State;
            return entity;
        }

        public async Task<T> UpdateByIdAsync(int id)
        {
            var result = new T();
            var model = _dbSet.FindAsync(id);
            if (model == null)
            {
                return null;
            }
            var response = _dbContext.Entry(model);
            EntityState entityState = response.State;
            return result;
        }
        

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<int> Save()
        {
            return await _dbContext.SaveChangesAsync();

        }
    }
}