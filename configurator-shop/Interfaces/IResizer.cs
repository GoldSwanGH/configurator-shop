using System.Drawing;

namespace configurator_shop.Interfaces
{
    public interface IResizer
    {
        public Image Resize(Image img, int width, int height);
    }
}