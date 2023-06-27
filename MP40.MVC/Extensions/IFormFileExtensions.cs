using System.Drawing;

namespace DwaProject.WEB.Extensions
{
    public static class IFormFileExtensions
    {
        public static async Task<Image> ToImage(this IFormFile formFile)
        {
            using (MemoryStream memoryStream = new())
            {
                await formFile.CopyToAsync(memoryStream);
                return Image.FromStream(memoryStream);
            }
        }
    }
}
