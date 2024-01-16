using LapTopStore_Computer.Data;
using LapTopStore_Computer.Data.Customer;
using LapTopStore_Computer.Models;
using LapTopStore_Computer.MyDapper;
using LapTopStore_Computer.MyInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(LapTopStoreContext context, IApplicationDbConnection applicationDbConnection) : base(context, applicationDbConnection)
        {
        }

        public async Task<int> Register(CustomerRegisterRequest requestData)
        {
            try
            {
                //if(_context.Customers.Any(x => x.CustomerUserName== requestData.CustomerUserName))
                //{
                //    return -2;
                //}

                //if (_context.Customers.Any(x => x.CustomerEmail == requestData.CustomerEmail))
                //{
                //    return -2;
                //}

                var newCustomer = new Customer() {
                    CustomerUserName = requestData.CustomerUserName,
                    CustomerFullName = requestData.CustomerFullName,
                    CustomerPhone = requestData.CustomerPhone,
                    CustomerEmail = requestData.CustomerEmail,
                    CustomerPassword = requestData.CustomerPassword, 
                    CustomerAddress = requestData.CustomerAddress,
                    ConfirmPassword = requestData.ConfirmPassword
                };

                var result = _context.Customers.Add(newCustomer);

                if(result == null)
                {
                    return -1;
                }

                return 1;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Customer> Login(CustomerLoginRequestData requestData)
        {
            try
            {
                //var password = LapTopStore_Common.Securiry.MD5Hash(requestData.Password + "|" + "UxFkTt5siR5dibph8JdUIsixJ2mmhr");

                var customer = _context.Customers.ToList()
                                .FindAll(c => c.CustomerEmail == requestData.Email && c.CustomerPassword == requestData.Password)
                                .FirstOrDefault();
                if(customer == null)
                {
                    return new Customer();
                }

                return customer;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> UpdateRefreshToken(UpdateRefreshTokenRequest requestData)
        {
            var user = _context.Customers.Where(x => x.CustomerId == requestData.UserID).FirstOrDefault();

            if(user == null || user.CustomerId < 0)
            {
                return -1;
            }

            user.RefreshToken = requestData.RefreshToken;
            user.RefreshTokenExpiryTime = requestData.RefreshTokenExpiryTime;
            _context.Customers.Update(user);

            return 1;
        }
    }
}
