using LapTopStore_Computer.Data.Order;
using LapTopStore_Computer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LapTopStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IStoreUnitOfWork _unitOfWork;
        public OrderController(IStoreUnitOfWork unitOfWork) 
        { 
            _unitOfWork= unitOfWork;
        }

        [HttpPost("GetAllOrder")]
        public async Task<IActionResult> GetAllOrder()
        {
            var list_order = new List<GetAllOrder>();

            list_order = await _unitOfWork._orderRepository.GetAllOrder();

            return Ok(list_order);
        }

        [HttpPost("GetOrderForDetail")]
        public async Task<IActionResult> GetOrderForDetail([FromBody] int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var result = new OrderAndOrderDetail();

            result = await _unitOfWork._orderRepository.GetOrderForDetail(id);

            return Ok(result);
        }

        [HttpPost("FilterOrderAPI")]
        public async Task<IActionResult> FilterOrderAPI(FilterOrderModel requestData)
        {
            var result = new List<GetAllOrder>();
            if(requestData == null)
            {
                return BadRequest();
            }

            result = await _unitOfWork._orderRepository.FilterOrder(requestData);

            return Ok(result);
        }
    }
}
