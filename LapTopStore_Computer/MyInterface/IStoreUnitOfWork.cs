using LapTopStore_Computer.MyInterface;
using LapTopStore_Computer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Interface
{
    public interface IStoreUnitOfWork : IDisposable
    {
        ICustomerRepository _customerRepository { get; }
        IProductRepository _productRepository { get; }

        int SaveChanges();
    }
}
