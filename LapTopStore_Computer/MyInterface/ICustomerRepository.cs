using LapTopStore_Computer.Data;
using LapTopStore_Computer.Data.Customer;
using LapTopStore_Computer.Interface;
using LapTopStore_Computer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.MyInterface
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<int> Register(CustomerRegisterRequest requestData);
        Task<Customer> Login(CustomerLoginRequestData requestData);
        Task<int> UpdateRefreshToken(UpdateRefreshTokenRequest requestData);
    }
}
