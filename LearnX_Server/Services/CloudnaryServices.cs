using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;


namespace LearnX_Server
{
    public class CloudnaryServices : ICloudnaryServices
    {
        private readonly Cloudinary _cloudinary;

        public CloudnaryServices()
        {
            // Load the .env file
            try
            {
                DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to load .env file.", ex);
            }


            // connect to cloudinary 

            var cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");
            if (string.IsNullOrEmpty(cloudinaryUrl))
            {
                throw new InvalidOperationException("CLOUDINARY_URL environment variable is not set.");
            }

            // Initialize Cloudinary instance
            _cloudinary = new Cloudinary(cloudinaryUrl) { Api = { Secure = true } };
        }


        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is invalid.");

            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Image upload failed.");

            return uploadResult.SecureUrl.ToString();
        }


        public async Task<string> UploadVideoAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is invalid.");

            using var stream = file.OpenReadStream();

            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
               
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Video upload failed.");

            return uploadResult.SecureUrl.ToString(); // Return the uploaded video URL
        }


    }
}
