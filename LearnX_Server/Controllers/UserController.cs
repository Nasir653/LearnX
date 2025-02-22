using Azure.Core;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnX_Server.Data;

using LearnX_Server.Models;
using LearnX_Server.Models.ViewModels;
using LearnX_Server.Interfaces;


namespace LearnX_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(DbConnection dbContext, ITokenService tokenService, IMailService Mail,IMessageHandler message) : ControllerBase
    {
        private readonly DbConnection _dbContext = dbContext;
        private readonly IMailService _Mail = Mail;
        private readonly IMessageHandler _message = message;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SignUp user)
        {
            try
            {
                if (user == null ||
                    string.IsNullOrWhiteSpace(user.Username) ||
                    string.IsNullOrWhiteSpace(user.Email) ||
                    string.IsNullOrWhiteSpace(user.Password))

                {
                    return _message.ErrorMessage(404, "All Crendentials Required");
                }

                // Use synchronous method if async is not supported
                var existingUser = _dbContext.Users.FirstOrDefault(e => e.Email == user.Email);

                if (existingUser != null)
                {
                    return _message.ErrorMessage(404, "Email Already Exists");
                }

                var newUser = new User
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                    Role = user.Role,
                    ProfilePicture = user.ProfilePicture,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _dbContext.Users.Add(newUser); // Use synchronous Add() if needed
                await _dbContext.SaveChangesAsync();
                return _message.SuccessMessage("User Registered Successfully", newUser);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while registering the user.",
                    Error = ex.Message,
                    InnerException = ex.InnerException?.Message ?? "No inner exception"
                });
            }

        }

          
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login login)
        {

            if (login.Email == "" || login.Password == "")
            {
                return _message.ErrorMessage(404, "All Crendentials Required");
            }

            var FindUser = _dbContext.Users.FirstOrDefault(x => x.Email == login.Email);


            if (FindUser == null)
            {

                return _message.ErrorMessage(404, "Incorrect Email");
            }

            var Verifypass = BCrypt.Net.BCrypt.Verify(login.Password, FindUser.Password);
            if (!Verifypass)
            {
                return _message.ErrorMessage(404, "Incorrect Password");
            }

            var token = tokenService.CreateToken(FindUser.UserId.ToString(), FindUser.Email, FindUser.Username);


            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("token", token, cookieOptions);

            return _message.SuccessMessage("Login Successfully", FindUser);
        }


        [HttpPost("Forget")]
        public async Task<IActionResult> ForgetPass([FromBody] ForgetModel forget)
        {
            var FindUser = _dbContext.Users.FirstOrDefault(e => e.Email == forget.Email);


            if (FindUser == null)
            {
                return BadRequest("Email Not Exists");

            }
            ;

            await _Mail.SendEmailAsync(forget.Email, "Login Successfully", "ForgetPass", false);

            return Ok(new
            {
                Message = "Email send Successfully"
            });
        }


        [HttpGet("GetUserByID/{UserId}")]
        public async Task<IActionResult> FindUserById(Guid UserId)
        {
            try
            {

                if (UserId == Guid.Empty)
                {
                    return BadRequest(new { Message = "Invalid user ID" });
                }


                var findUser = await _dbContext.Users.FindAsync(UserId);


                if (findUser == null)
                {
                    return NotFound(new { Message = "User not found" });
                }


                return Ok(findUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred while retrieving the user.",
                    Error = ex.Message
                });
            }
        }



        [HttpDelete("DeleteUser/{UserId}")]
        public async Task<IActionResult> DeleteUser(Guid UserId)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(UserId);

                if (user == null)
                {
                    return NotFound(new { Message = "User not found" });
                }

                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();

                return Ok(new { Message = "User deleted successfully" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error");
            }
        }



        [HttpPut("editUser/{UserId}")]
        public async Task<IActionResult> EditUser(Guid UserId, EditModel Edit)
        {
            try
            {
                
                var FindUser = await _dbContext.Users.FindAsync(UserId);

                if (FindUser == null)
                {
                    return NotFound(new { message = "User Not Found" });
                }

               
                FindUser.Username = Edit.Username ?? FindUser.Username;
                FindUser.Email = Edit.Email ?? FindUser.Email;
                FindUser.Password = Edit.Password ?? FindUser.Password;
                FindUser.Role = Edit.Role;
                FindUser.ProfilePicture = Edit.ProfilePicture ?? FindUser.ProfilePicture;

                _dbContext.Users.Update(FindUser);
                await _dbContext.SaveChangesAsync();

                return Ok(new { message = "User updated successfully", user = FindUser });
            }
                catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }


    }
}
