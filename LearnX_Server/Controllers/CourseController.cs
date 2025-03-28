﻿using System.IdentityModel.Tokens.Jwt;
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


                var findUser = await _dbContext.Users.FindAsync(userId);

                if (findUser == null)
                {
                    return _message.ErrorMessage(404, "User Not Found");
                }

                //if (findUser.Role != "Instructor" || findUser.Role != "Admin") {

                //    return _message.ErrorMessage(404, "Only Instructor or Admin Can Create the Courses");
                
                //}


                var imgUrl = await _cloudinary.UploadImageAsync(images);

                var newCourse = new Course
                {
                    CourseId = Guid.NewGuid(),
                    Title = courseModel.Title,
                    Description = courseModel.Description,
                    Image = imgUrl, 
                    InstructorId = userId,
                    Price = courseModel.Price,
                    Category = courseModel.Category,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };


                await _dbContext.Courses.AddAsync(newCourse);
                await _dbContext.SaveChangesAsync();

                if (findUser.Courses == null)
                {
                    findUser.Courses = new List<Course>(); // Initialize if null
                }

                findUser.Courses.Add(newCourse);
                await _dbContext.SaveChangesAsync(); // Save changes to persist in the database


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

        [HttpPost("CreateLessons/{CourseId}")]
        public async Task<IActionResult> CreateLessons(Guid CourseId, [FromForm] LessonModel _lesson, IFormFile videoFile)
        {
            try
            {
                if (_lesson == null || videoFile == null)
                {
                    return _message.ErrorMessage(400, "Invalid Lesson Data or Video File");
                }

                var token = Request.Cookies["token"];
                if (string.IsNullOrEmpty(token))
                {
                    return _message.ErrorMessage(401, "User is not authenticated");
                }

                var userId = _tokenService.VerifyTokenAndGetId(token);
                if (userId == Guid.Empty)
                {
                    return _message.ErrorMessage(401, "Invalid Token");
                }

                var findCourse = await _dbContext.Courses.FindAsync(CourseId);
                if (findCourse == null)
                {
                    return _message.ErrorMessage(404, "Course Not Found");
                }

                // Upload Video
                var videoUrl = await _cloudinary.UploadVideoAsync(videoFile);

                var newLesson = new Lesson
                {
                    LessonId = Guid.NewGuid(),
                    Title = _lesson.Title,
                    VideoUrl = videoUrl,
                    Duration = _lesson.Duration ?? 0,
                    CourseId = CourseId,
                };

                await _dbContext.Lessons.AddAsync(newLesson);
                await _dbContext.SaveChangesAsync();

                return _message.SuccessMessage("Lesson Created Successfully", newLesson);
            }
            catch (Exception ex)
            {
                return _message.ErrorMessage(500, ex.Message);
            }
        }


    }
}
