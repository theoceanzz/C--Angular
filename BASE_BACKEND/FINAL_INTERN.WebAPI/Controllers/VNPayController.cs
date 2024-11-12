using FINAL_INTERN.Business.VNPayService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FINAL_INTERN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly VNPayService _vnPayService;
        private readonly IConfiguration _config;

        public VNPayController(VNPayService vnPayService, IConfiguration config)
        {
            _vnPayService = vnPayService;
            _config = config;
        }

        // API tạo URL thanh toán
        [HttpPost("create-payment")]
        public IActionResult CreatePayment(decimal amount, string orderInfo)
        {
            var returnUrl = _config["VNPay:ReturnUrl"];
            var transactionId = DateTime.Now.Ticks.ToString(); // Sinh mã giao dịch ngẫu nhiên
            var paymentUrl = _vnPayService.CreatePaymentUrl(amount, orderInfo, returnUrl, transactionId);

            return Ok(new { url = paymentUrl });
        }

        [HttpGet("return")]
        public IActionResult PaymentReturn([FromQuery] Dictionary<string, string> responseParams)
        {
            if (_vnPayService.ValidateSignature(responseParams))
            {
                var paymentStatus = responseParams["vnp_ResponseCode"];
                if (paymentStatus == "00")
                {
                    _vnPayService.SavePayment(responseParams);
                    _vnPayService.SaveTransaction(responseParams);

                    return Ok("Thanh toán thành công");
                }
                else
                {
                    return BadRequest("Thanh toán thất bại");
                }
            }
            else
            {
                return BadRequest("Chữ ký không hợp lệ");
            }
        }

    }
}
