using FINAL_INTERN.Business.OrderService;
using FINAL_INTERN.Business.SendEmail;
using FINAL_INTERN.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FINAL_INTERN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderservice;

        private readonly finalInternDbContext _finalInternDbContext;

        private readonly SendEmailService _sendEmailService;

        public OrderController(IOrderService orderService,finalInternDbContext finalInternDbContext,SendEmailService sendEmailService)
        {
            _orderservice = orderService;
            _finalInternDbContext = finalInternDbContext;
            _sendEmailService = sendEmailService;
        }
        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetAllOrders()
        {
            var Orders = await _orderservice.GetAllAsync();
            return Ok(Orders);
        }

        [HttpGet("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var Orders = await _orderservice.GetByIdAsync(id);
            if (Orders == null)
            {
                return NotFound();
            }
            return Ok(Orders);
        }

        [HttpGet("GetOrdersPaged")]
        public async Task<IActionResult> GetOrdersPaged(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var totalCount = await _orderservice.GetAllAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount.Count() / pageSize);
            var Orders = await _orderservice.GetPagedAsync(pageNumber, pageSize);

            if (Orders == null || !Orders.Any())
            {
                return NotFound("No Orders found.");
            }

            return Ok(new { data = Orders, totalPages });
        }


        [HttpPost("CreateOrder")]
        public async Task<IActionResult> AddOrder(Order order)
        {
            var Orders = await _orderservice.AddAsync(order);
            return CreatedAtAction(nameof(order), new { id = Orders.Id }, Orders);
        }

        [HttpPut("UpdateOrder/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, int status)
        {
            var order = await _orderservice.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            switch (status)
            {
                case 0:
                    order.Status = 0;
                    break;
                case 1:
                    order.Status = 1;
                    break;
                case 2:
                    order.Status = 2;
                    break;
            }
            var updatedAppointment = await _orderservice.UpdateAsync(order);
            if (updatedAppointment == null)
            {
                return NotFound();
            }
            return Ok(updatedAppointment);
        }

        //[HttpDelete("DeleteOrder{id}")]
        //public async Task<IActionResult> DeleteAppointment(int id)
        //{
        //    var deletedAppointment = await _orderservice.DeleteOrderAsync(id);
        //    if (deletedAppointment == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(deletedAppointment);
        //}

        [HttpGet("SearchOrder/{items}")]
        public async Task<IActionResult> SearchOrder(string items)
        {
            // Đợi cho đến khi phương thức SearchOrderAsync hoàn thành
            var listOrder = await _orderservice.SearchOrderAsync(items);

            if (listOrder == null || !listOrder.Any()) // Kiểm tra nếu danh sách trống
            {
                return NotFound();
            }
            return Ok(listOrder);
        }

        [HttpGet("SendSuccessOrders/{id}")]
        public async Task<IActionResult> SendSuccessOrder(int id)
        {
            var Order = _finalInternDbContext.Orders.FirstOrDefault(d => d.Id == id && d.Status == 2);

            if (Order == null)
            {
                return NotFound();
            }

            await _sendEmailService.SendEmailAsync("nguyenvanhaivghy2003@gmail.com", "Success Orders", "Buy Success , Thank for using my shop to by your car!");
            return Ok(new {message = "Success to send message!"});
        }
    }
}
