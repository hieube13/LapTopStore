using Dapper;
using LapTopStore_Computer.Data;
using LapTopStore_Computer.Data.Order;
using LapTopStore_Computer.Data.ShoppingCart;
using LapTopStore_Computer.Data.VNPAY;
using LapTopStore_Computer.Models;
using LapTopStore_Computer.MyDapper;
using LapTopStore_Computer.MyInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderDetailViewModel = LapTopStore_Computer.Data.Order.OrderDetailViewModel;

namespace LapTopStore_Computer.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(LapTopStoreContext context, IApplicationDbConnection applicationDbConnection) : base(context, applicationDbConnection)
        {
        }

        public async Task<int> CreateOrder(OrderViewModel requestData)
        {
            var order = new Order()
            {
                CustomerId = requestData.CustomerId,
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now.AddDays(2),
                Phone = requestData.Phone,
                Email = requestData.Email,
                Ward = requestData.Ward,
                District = requestData.District,
                Province = requestData.Province,
                Address = requestData.Address,
                TotalPrice = double.Parse(requestData.TotalPrice),
                Status = requestData.Status,
                PaymentType = requestData.PaymentType
            };

            if(order == null)
            {
                return -1;
            }
            else
            {
                _context.Orders.Add(order);
                _context.SaveChanges();

                int orderId = order.OrderId;

                return orderId;
            }

        }

        public async Task<int> CreateOrderDetail(List<CartProduct> requestData)
        {
            foreach (var item in requestData)
            {
                var orderDetail = new OrderDetail()
                {
                    OrderId = item.OrderID,
                    ProductId = item.ProductId,
                    ProductPrice = double.Parse(item.ProductPrice),
                    Quantity= item.Quantity,
                    Discount = double.Parse(item.ProductDiscount),
                    Total = double.Parse(item.ProductPrice) * item.Quantity
                };

                if (orderDetail == null)
                {
                    return -1;
                }
                else
                {
                    _context.OrderDetail.Update(orderDetail);
                }
            }

            return 1;
        }

        public async Task<Order> GetOrderByID(int? id)
        {
            if(id == null)
            {
                return new Order();
            }

            var result = _context.Orders.Where(x => x.OrderId== id).FirstOrDefault();

            return result;
        }

        public async Task<Order> CreateOrderAndDetail(OrderViewModel requestData)
        {
            var order = new Order()
            {
                CustomerId = requestData.CustomerId,
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now.AddDays(2),
                Phone = requestData.Phone,
                Email = requestData.Email,
                Ward = requestData.Ward,
                District = requestData.District,
                Province = requestData.Province,
                Address = requestData.Address,
                TotalPrice = double.Parse(requestData.TotalPrice),
                Status = requestData.Status,
            };

            if (int.TryParse(requestData.PaymentType, out int paymentType))
            {
                if (paymentType == 1)
                {
                    order.PaymentType = "Thanh toán tiền mặt";
                    order.PaymentTypeVN = "Khong co";
                }
                else if (paymentType == 2)
                {
                    if (int.TryParse(requestData.PaymentTypeVN, out int paymentTypeVN))
                    {
                        switch (paymentTypeVN)
                        {
                            case 0:
                                order.PaymentType = "Chuyển khoản";
                                order.PaymentTypeVN = "Cổng VNPAYQR";
                                break;
                            case 1:
                                order.PaymentType = "Chuyển khoản";
                                order.PaymentTypeVN = "ứng dụng hỗ trợ VNPAYQR";
                                break;
                            case 2:
                                order.PaymentType = "Chuyển khoản";
                                order.PaymentTypeVN = "ATM-nội địa";
                                break;
                            case 3:
                                order.PaymentType = "Chuyển khoản";
                                order.PaymentTypeVN = "ATM-quốc tế";
                                break;
                            default:
                                // Xử lý trường hợp không nằm trong các case trên (nếu cần)
                                break;
                        }
                    }
                }
            }


            if (order == null)
            {
                return new Order();
            }
            
            _context.Orders.Add(order);
            _context.SaveChanges();

            int orderId = order.OrderId;

            foreach (var item in requestData.listProducts)
            {
                var orderDetail = new OrderDetail()
                {
                    OrderId = orderId,
                    ProductId = item.ProductId,
                    ProductPrice = double.Parse(item.ProductPrice),
                    Quantity = item.Quantity,
                    Discount = double.Parse(item.ProductDiscount),
                    Total = double.Parse(item.ProductPrice) * item.Quantity
                };

                if (orderDetail == null)
                {
                    return new Order();
                }
                else
                {
                    _context.OrderDetail.Update(orderDetail);
                }
            }


            return order;

        }

        public async Task UpdateProductAfterPay(PaySuccessModel requestData)
        {
            foreach (var item in requestData.list_products)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@OrderID", requestData.OrderID);
                parameters.Add("@BankCode", requestData.BankCode);
                parameters.Add("@ProductID", item.ProductId);
                parameters.Add("@ProductQuantity", item.Quantity);
                await _applicationDbConnection.ExecuteAsync("updateafterpaysuccess", parameters, commandType: CommandType.StoredProcedure);
            }

        }

        public async Task<List<GetAllOrder>> GetAllOrder()
        {
            var list_order = new List<GetAllOrder>();

            list_order = await _applicationDbConnection.QueryAsync<GetAllOrder>("GetAllOrder", commandType: CommandType.StoredProcedure);

            if(list_order == null || list_order.Count <= 0) 
            {
                return null;
            }

            return list_order;

        }

        public async Task<OrderAndOrderDetail> GetOrderForDetail(int? id)
        {
            var list_order = new List<GetAllOrder>();
            var parameters = new DynamicParameters();
            parameters.Add("@OrderID", id);
            list_order = await _applicationDbConnection.QueryAsync<GetAllOrder>("GetOrderByID", parameters, commandType: CommandType.StoredProcedure);

            GetAllOrder order = list_order[0];

            var list_orderDetail = new List<OrderDetailViewModel>();
            list_orderDetail = await _applicationDbConnection.QueryAsync<OrderDetailViewModel>("GetOrderDetailByID", parameters, commandType: CommandType.StoredProcedure);

            var orderResult = new OrderAndOrderDetail()
            { 
                OrderId= order.OrderId,
                CustomerId= order.CustomerId,
                OrderDate= order.OrderDate,
                RequiredDate= order.RequiredDate,
                ShippedDate= order.ShippedDate,
                Phone = order.Phone,
                Email = order.Email,
                Ward = order.Ward,
                District = order.District,
                Province = order.Province,
                Address= order.Address,
                TotalPrice= order.TotalPrice,
                Status= order.Status,
                PaymentDate= order.PaymentDate,
                PaymentType= order.PaymentType,
                PaymentTypeVN= order.PaymentTypeVN,
                CustomerUserName= order.CustomerUserName,
                listDetail= list_orderDetail
            };


            return orderResult;
        }

        public async Task<List<GetAllOrder>> FilterOrder(FilterOrderModel requestData)
        {
            string paymentTypeText = null;
            string paymentTypeVNText = null;
            if (requestData.PaymentType == 1)
            {
                paymentTypeText = "Thanh toán tiền mặt";
            }
            else if(requestData.PaymentType == 2)
            {
                paymentTypeText = "Chuyển khoản";
            }   
            
            if(requestData.paymentTypeVN == "VNPAYQR")
            {
                paymentTypeVNText = "Cổng VNPAYQR";
            }
            else if(requestData.paymentTypeVN == "NCB")
            {
                paymentTypeVNText = "NCB";
            }
            else if (requestData.paymentTypeVN == "Viettinbank")
            {
                paymentTypeVNText = "Viettinbank";
            }
            else if (requestData.paymentTypeVN == "Vietcombank")
            {
                paymentTypeVNText = "Vietcombank";
            }

            var list_order = new List<GetAllOrder>();

            var parameters = new DynamicParameters();
            parameters.Add("@StartDate", requestData.StartDate);
            parameters.Add("@EndDate", requestData.EndDate);
            parameters.Add("@Status", requestData.Status);
            parameters.Add("@PaymentType", paymentTypeText);
            parameters.Add("@paymentTypeVN", paymentTypeVNText);
            list_order = await _applicationDbConnection.QueryAsync<GetAllOrder>("ListOrderByFilter", parameters, commandType: CommandType.StoredProcedure);

            return list_order;
        }
    }
}
