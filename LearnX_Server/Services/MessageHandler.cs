using LearnX_Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearnX_Server.Services
{
    public class MessageHandler : IMessageHandler
    {
        public IActionResult SuccessMessage(string message, object? payload)
        {
            return new OkObjectResult(new { success = true, message, payload });
        }

        public IActionResult ErrorMessage(int statusCode, string message)
        {
            return new ObjectResult(new { success = false, message })
            {
                StatusCode = statusCode
            };
        }
    }
}
