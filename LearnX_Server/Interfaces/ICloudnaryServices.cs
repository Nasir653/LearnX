namespace LearnX_Server
{
    public interface ICloudnaryServices
    {
        Task<string> UploadImageAsync(IFormFile file);
        Task<string> UploadVideoAsync(IFormFile file);
    }
    
}
