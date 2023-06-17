namespace SladkarnicaHvarchilo.Web.Helpers
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class ImageManager
    {
        public async Task SaveImageToFile(string imagesFolderPath, string imagePath, IFormFile imageFile)
        {
            if (Directory.Exists(imagesFolderPath))
            {
                using (Stream fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
            }
        }
    }
}
