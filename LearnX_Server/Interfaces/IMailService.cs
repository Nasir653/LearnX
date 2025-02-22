namespace LearnX_Server
{

    public interface IMailService
    {
        Task SendEmailAsync(string to, string subject, string body, bool isHtml = true);
    }
}
