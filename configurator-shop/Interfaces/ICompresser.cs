using System.Drawing;
using System.IO;

namespace configurator_shop.Interfaces
{
    public interface ICompresser
    {
        public Image Compress(Image img, long value, MemoryStream stream);
    }
}