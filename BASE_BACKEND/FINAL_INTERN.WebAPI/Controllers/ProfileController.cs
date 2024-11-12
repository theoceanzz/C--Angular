using FINAL_INTERN.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FINAL_INTERN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {

        private readonly finalInternDbContext _finalInternDbContext;

        public ProfileController(finalInternDbContext finalInternDbContext)
        {
            _finalInternDbContext = finalInternDbContext;
        }

        [HttpPut("update-profile/{id}")]
        public IActionResult UpdateProfile(int id, [FromBody] Account model)
        {
            var account = _finalInternDbContext.Accounts.FirstOrDefault(a => a.Id == id);

            if (account == null)
            {
                return NotFound(new { message = "Account not found" });
            }

            if (!string.IsNullOrEmpty(model.Email) &&
                _finalInternDbContext.Accounts.Any(a => a.Email == model.Email && a.Id != id))
            {
                return Conflict(new { message = "Email already in use" });
            }

            // Update fields except for Username and Password
            account.Email = model.Email;
            account.Gender = model.Gender;
            account.FirstName = model.FirstName;
            account.LastName = model.LastName;
            account.Address = model.Address;
            account.Birthday = model.Birthday;
            account.Img = model.Img;
            account.Alt = model.Alt;
            account.IsActive = model.IsActive;

            _finalInternDbContext.Accounts.Update(account);
            _finalInternDbContext.SaveChanges();

            return Ok(new { message = "Profile updated successfully" });
        }

        [HttpPut("change-password/{id}")]
        public IActionResult ChangePassword(int id, [FromBody] string newPassword)
        {
            var account = _finalInternDbContext.Accounts.FirstOrDefault(a => a.Id == id);

            if (account == null)
            {
                return NotFound(new { message = "Account not found" });
            }

            account.Password = newPassword;

            _finalInternDbContext.Accounts.Update(account);
            _finalInternDbContext.SaveChanges();

            return Ok(new { message = "Password updated successfully" });
        }
    }
}
