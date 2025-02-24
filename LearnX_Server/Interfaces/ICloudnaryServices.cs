namespace LearnX_Server
{
    public interface ICloudnaryServices
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
