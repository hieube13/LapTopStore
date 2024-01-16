using LapTopStore_Computer.Data.Order;
using LapTopStore_Computer.Data.ShoppingCart;
using LapTopStore_Computer.Data.VNPAY;
using LapTopStore_Computer.Interface;
using LapTopStore_Computer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.MyInterface
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<int> CreateOrder(OrderViewModel requestData);
        Task<int> CreateOrderDetail(List<CartProduct> requestData);
        Task<Order> GetOrderByID(int? id);
        Task<Order> CreateOrderAndDetail(OrderViewModel requestData);
        Task UpdateProductAfterPay(PaySuccessModel requestData);
        Task<List<GetAllOrder>> GetAllOrder();
        Task<OrderAndOrderDetail> GetOrderForDetail(int? id);
        Task<List<GetAllOrder>> FilterOrder(FilterOrderModel requestData);
    }
}
