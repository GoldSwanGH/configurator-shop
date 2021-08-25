using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using configurator_shop.Interfaces;
using Microsoft.AspNetCore.Http;

namespace configurator_shop.Services
{
    public class PictureProcessor : IPictureProcessor
    {
        public void ProcessPicture(IFormFile image, string filePath, IResizer resizer, ICompresser compresser, int size)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                using (var img = Image.FromStream(memoryStream))
                {
                    memoryStream.SetLength(0);
                    Image readyImage = resizer.Resize(img, size, size);
                    readyImage = compresser.Compress(readyImage, 30L, memoryStream);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        readyImage.Save(fileStream, ImageFormat.Jpeg);
                    }
                }
            }
        }
    }
}