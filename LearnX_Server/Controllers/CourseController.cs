using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearnX_Server.Data;
using LearnX_Server.Models;
using LearnX_Server.Models.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace LearnX_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(DbConnection _dbConnection, ITokenService tokenService) : ControllerBase
    {
        private readonly DbConnection dbContext = _dbConnection;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromBody] CourseModel courseModel)
        {
            try
            {
                if (courseModel == null)
                {
                    return BadRequest(new { message = "Invalid course data." });
                }

                var token = Request.Cookies["token"];

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { message = "User is not authenticated." });
                }

                var userId = _tokenService.VerifyTokenAndGetId(token);
                if (userId == Guid.Empty)
                {
                    return Unauthorized(new { message = "Invalid token." });
                }

                var newCourse = new Course
                {
                    CourseId = Guid.NewGuid(),
                    Title = courseModel.Title,
                    Description = courseModel.Description,
                    InstructorId = userId,
                    Price = courseModel.Price,
                    Lessons = courseModel.Lessons?.Select(e => new Lesson
                    {
                        LessonId = Guid.NewGuid(),
                        Title = e.Title,
                        VideoUrl = e.VideoUrl,
                        Duration = e.Duration,
                        CourseId = Guid.NewGuid()
                    }).ToList(),

                    Category = courseModel.Category,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await dbContext.Courses.AddAsync(newCourse);
                await dbContext.SaveChangesAsync();

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


        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> GetAllCourse()
        {
            try
            {
                var allCourse = await dbContext.Courses.ToListAsync();

                if (allCourse == null)
                {
                    return BadRequest("Courses Not Found");
                }

                return Ok(new
                {
                    message = "All Courses",
                    payload = allCourse
                }) ;
            }

            catch (Exception)
            {
                return BadRequest("Server Error");
            }
        }

    }
} 
