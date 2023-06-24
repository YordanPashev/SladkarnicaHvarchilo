namespace SladkarnicaHvarchilo.Web.Helpers
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class ImageManager
    {
        public async Task SaveImageToFileAsync(IFormFile imageFile)
        {
            string imagesFolderPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\img\cakes\"));
            string imagePath = imagesFolderPath + imageFile.FileName;

            using (Stream fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
        }
    }
}
