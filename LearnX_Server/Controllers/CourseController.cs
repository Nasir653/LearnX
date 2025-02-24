using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearnX_Server.Data;
using LearnX_Server.Models;
using LearnX_Server.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using LearnX_Server.Interfaces;


namespace LearnX_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(DbConnection _dbConnection, ITokenService tokenService, ICloudnaryServices cloudinay , IMessageHandler message) : ControllerBase
    {
        private readonly DbConnection _dbContext = _dbConnection;
        private readonly ITokenService _tokenService = tokenService;
        private readonly ICloudnaryServices _cloudinary = cloudinay;
        private readonly IMessageHandler _message = message;



        [HttpPost("CreateCourse")]

        public async Task<IActionResult> CreateCourse([FromForm] CourseModel courseModel, IFormFile images)
        {
            try
            {
                if (courseModel == null || images == null)
                {
                    return BadRequest(new { message = "Invalid course data or image." });
                }

                //var token = Request.Cookies["token"];
                //if (string.IsNullOrEmpty(token))
                //{
                //    return Unauthorized(new { message = "User is not authenticated." });
                //}

                //var userId = _tokenService.VerifyTokenAndGetId(token);

                //if (userId == Guid.Empty)
                //{
                //    return Unauthorized(new { message = "Invalid token." });
                //}

                // Upload Image to Cloudinary (Ensure this method is `awaited`)

                var imgUrl = await _cloudinary.UploadImageAsync(images);

                var newCourse = new Course
                {
                    CourseId = Guid.NewGuid(),
                    Title = courseModel.Title,
                    Description = courseModel.Description,
                    Image = imgUrl, // Ensure `imgUrl` is correct
                    //InstructorId = userId,
                    Price = courseModel.Price,
                    Category = courseModel.Category,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dbContext.Courses.AddAsync(newCourse);
                await _dbContext.SaveChangesAsync();

                return Ok(new { message = "Course created successfully", courseId = newCourse.CourseId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while creating the course.",
                    error = ex.Message,
                    innerException = ex.InnerException?.Message ?? "No inner exception"
                });
            }
        }


        [HttpGet("get/allCourses")]
        public async Task<IActionResult> GetAllCourse()
        {
            try
            {
                var allCourse = await _dbContext.Courses.ToListAsync();

                if (allCourse == null)
                {
                    return BadRequest("Courses Not Found");
                }

                return Ok(new
                {
                    message = "All Courses",
                    payload = allCourse
                });
            }

            catch (Exception)
            {
                return BadRequest("Server Error");
            }
        }


        [HttpGet("get/CourseById/{CourseId}")]

        public async Task <IActionResult> CourseById( Guid CourseId)
        {
            try
            {

                var FindCourse = await _dbContext.Courses.FindAsync(CourseId);

                if(FindCourse == null)
                {
                    return _message.ErrorMessage(404, "Course Not Found");
                }

                return _message.SuccessMessage("CourseData", FindCourse);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return _message.ErrorMessage(500, "Server Error");
            }
        }

    }
}
