
using FINAL_INTERN.Business.OrderDetailDetailService;
using FINAL_INTERN.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FINAL_INTERN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _OrderDetailservice;

        public OrderDetailController(IOrderDetailService OrderDetailService)
        {
            _OrderDetailservice = OrderDetailService;
        }
        [HttpGet("GetAllOrderDetail")]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var OrderDetailDetails = await _OrderDetailservice.GetAllAsync();
            return Ok(OrderDetailDetails);
        }

        [HttpGet("GetOrderDetailById/{id}")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var OrderDetails = await _OrderDetailservice.GetByIdAsync(id);
            if (OrderDetails == null)
            {
                return NotFound();
            }
            return Ok(OrderDetails);
        }

        [HttpGet("GetOrderDetailsPaged")]
        public async Task<IActionResult> GetOrderDetailsPaged(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var totalCount = await _OrderDetailservice.GetAllAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount.Count() / pageSize);
            var OrderDetails = await _OrderDetailservice.GetPagedAsync(pageNumber, pageSize);

            if (OrderDetails == null || !OrderDetails.Any())
            {
                return NotFound("No OrderDetails found.");
            }

            return Ok(new { data = OrderDetails, totalPages });
        }


        [HttpPost("CreateOrderDetail")]
        public async Task<IActionResult> AddOrderDetail(OrderDetail OrderDetail)
        {
            var OrderDetails = await _OrderDetailservice.AddAsync(OrderDetail);
            return CreatedAtAction(nameof(OrderDetail), new { id = OrderDetails.Id }, OrderDetails);
        }

        //[HttpPut("UpdateOrderDetail/{id}")]
        //public async Task<IActionResult> UpdateAppointment(int id, int status)
        //{
        //    var OrderDetail = await _OrderDetailservice.GetByIdAsync(id);
        //    if (OrderDetail == null)
        //    {
        //        return NotFound();
        //    }
        //    switch (status)
        //    {
        //        case 0:
        //            OrderDetail.Status = 0;
        //            break;
        //        case 1:
        //            OrderDetail.Status = 1;
        //            break;
        //        case 2:
        //            OrderDetail.Status = 2;
        //            break;
        //    }
        //    var updatedAppointment = await _OrderDetailservice.UpdateAsync(OrderDetail);
        //    if (updatedAppointment == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(updatedAppointment);
        //}

        //[HttpDelete("DeleteOrderDetail{id}")]
        //public async Task<IActionResult> DeleteAppointment(int id)
        //{
        //    var deletedAppointment = await _OrderDetailservice.DeleteOrderDetailAsync(id);
        //    if (deletedAppointment == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(deletedAppointment);
        //}

        [HttpGet("SearchOrderDetail/{items}")]
        public async Task<IActionResult> SearchOrderDetail(string items)
        {
            // Đợi cho đến khi phương thức SearchOrderDetailAsync hoàn thành
            var listOrderDetail = await _OrderDetailservice.SearchOrderDetailAsync(items);

            if (listOrderDetail == null || !listOrderDetail.Any()) // Kiểm tra nếu danh sách trống
            {
                return NotFound();
            }
            return Ok(listOrderDetail);
        }

    }
}
