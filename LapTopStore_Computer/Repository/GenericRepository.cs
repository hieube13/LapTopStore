using LapTopStore_Computer.Interface;
using LapTopStore_Computer.Models;
using LapTopStore_Computer.MyDapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LapTopStoreContext _context;
        protected readonly IApplicationDbConnection _applicationDbConnection;

        public GenericRepository(LapTopStoreContext context, IApplicationDbConnection applicationDbConnection)
        {
            _context = context;
            _applicationDbConnection = applicationDbConnection;
        }

        public DbSet<T> DbSet => throw new NotImplementedException();

        public async Task<List<T>> GetAllAsync()
        {
            return _context.Set<T>().AsQueryable().ToList();
        }

        public Task<T> GetByID(int id)
        {
            throw new NotImplementedException();
        }

    }
}
