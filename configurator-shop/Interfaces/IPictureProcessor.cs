using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace configurator_shop.Interfaces
{
    public interface IPictureProcessor
    {
        public void ProcessPicture(IFormFile image, string filePath, IResizer resizer, ICompresser compresser, int size);
    }
}