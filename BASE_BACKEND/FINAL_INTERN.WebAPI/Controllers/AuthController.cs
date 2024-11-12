using FINAL_INTERN.Business.SendEmail;
using FINAL_INTERN.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FINAL_INTERN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly finalInternDbContext _finalInternDbContext;

        private readonly SendEmailService _sendEmailService;


        public AuthController(IConfiguration configuration, finalInternDbContext finalInternDbContext, SendEmailService sendEmailService)
        {
            _configuration = configuration;
            _finalInternDbContext = finalInternDbContext;
            _sendEmailService = sendEmailService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AccountLogin model)
        {
            // Tìm tài khoản với tên đăng nhập và mật khẩu hợp lệ
            Account account = _finalInternDbContext.Accounts.FirstOrDefault(d => d.Username.Equals(model.Username) && d.Password.Equals(model.Password));

            if (account != null)
            {
                // Tạo các claims, bao gồm Username, ID và Role của tài khoản
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, model.Username),                  // Chủ sở hữu token
                    new Claim(JwtRegisteredClaimNames.Jti, account.Id.ToString()),           // Sử dụng ID của tài khoản thay cho Guid
                    new Claim(ClaimTypes.Role, account.Role?.NameOfRole ?? "User")           // Thêm claim vai trò từ NameOfRole (mặc định là "User" nếu Role null)
                };

                // Lấy thông tin JWT từ appsettings
                var jwtSettings = _configuration.GetSection("JwtSettings");

                // Tạo secret key từ appsettings
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));

                // Tạo token descriptor
                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

                // Tạo JWT token
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                // Trả về token
                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] AccountRegister model)
        {


            var existingAccountByUsername = _finalInternDbContext.Accounts
                .FirstOrDefault(d => d.Username.Equals(model.Username));

            if (existingAccountByUsername != null)
            {
                return Conflict(new { message = "Username already exists" });
            }

            var existingAccountByEmail = _finalInternDbContext.Accounts
                .FirstOrDefault(d => d.Email.Equals(model.Email));

            if (existingAccountByEmail != null)
            {
                return Conflict(new { message = "Email already exists" });
            }

            var newAccount = new Account
            {
                Username = model.Username,
                Password = model.Password,
                Birthday = DateTime.UtcNow,
                Email = model.Email,
                RoleId = 1,
                FirstName = "Hello",
                IsActive = true
            };

            _finalInternDbContext.Accounts.Add(newAccount);
            _finalInternDbContext.SaveChanges();

            return Ok(new { message = "Account registered successfully" });
        }



        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestResetPassword([FromBody] string email)
        {
            var account = _finalInternDbContext.Accounts.FirstOrDefault(a => a.Email == email);

            if (account == null)
            {
                return NotFound(new { message = "Email không tồn tại trong hệ thống" });
            }

            var token = Guid.NewGuid().ToString();
            account.ResetPasswordToken = token;
            account.ResetTokenExpiry = DateTime.Now.AddMinutes(30);

            _finalInternDbContext.SaveChanges();

            var resetLink = $"{_configuration["AppUrl"]}/reset-password?token={token}";
            await _sendEmailService.SendEmailAsync(email, "Reset Password", $"Click vào link sau để đặt lại mật khẩu: {resetLink}");

            return Ok(new { message = "Đã gửi mã xác thực tới email của bạn" });
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPassword model)
        {
            var account = _finalInternDbContext.Accounts.FirstOrDefault(a => a.ResetPasswordToken == model.Token && a.ResetTokenExpiry > DateTime.UtcNow);

            if (account == null)
            {
                return BadRequest(new { message = "Mã xác thực không hợp lệ hoặc đã hết hạn" });
            }

            // Cập nhật mật khẩu mới
            account.Password = model.NewPassword;
            account.ResetPasswordToken = null; // Xóa mã xác thực sau khi đặt lại mật khẩu
            account.ResetTokenExpiry = null;

            _finalInternDbContext.SaveChanges();

            return Ok(new { message = "Mật khẩu đã được cập nhật thành công" });
        }

        
    }
}
