using System.Drawing;

namespace configurator_shop.Interfaces
{
    public interface ICompresser
    {
        public Image Compress(Image img, long value);
    }
}