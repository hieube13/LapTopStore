using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        DbSet<T> DbSet { get; }
        Task<List<T>> GetAllAsync();
        Task<T> GetByID(int id);
    }
}
