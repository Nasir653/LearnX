using Microsoft.AspNetCore.Mvc;

namespace LearnX_Server.Interfaces
{
    public interface IMessageHandler
    {
      public  IActionResult SuccessMessage(string message, object? payload);
        public IActionResult ErrorMessage(int statusCode, string message);
    }
}
