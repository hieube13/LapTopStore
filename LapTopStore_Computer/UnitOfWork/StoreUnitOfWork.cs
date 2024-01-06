using LapTopStore_Computer.Interface;
using LapTopStore_Computer.Models;
using LapTopStore_Computer.MyInterface;
using LapTopStore_Computer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.UnitOfWork
{
    public class StoreUnitOfWork : IStoreUnitOfWork
    {
        public ICustomerRepository _customerRepository { get; set; }
        public IProductRepository _productRepository { get; set; }
        private readonly LapTopStoreContext _context;

        public StoreUnitOfWork(ICustomerRepository customerRepository, LapTopStoreContext context, IProductRepository productRepository)
        {
            _customerRepository = customerRepository;
            _context = context;
            _productRepository = productRepository;
        }

        public void Dispose()
        {
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
