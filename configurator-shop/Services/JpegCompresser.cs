using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using configurator_shop.Interfaces;

namespace configurator_shop.Services
{
    public class JpegCompresser : ICompresser
    {
        public Image Compress(Image image, long value, MemoryStream outStream)
        {
            var jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            // if we aren't able to retrieve our encoder
            // we should just save the current image and
            // return to prevent any exceptions from happening
            if (jpgEncoder == null)
            {
                image.Save(outStream, ImageFormat.Jpeg);
            }
            else
            {
                var qualityEncoder = Encoder.Quality;
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(qualityEncoder, value);
                image.Save(outStream, jpgEncoder, encoderParameters);
            }

            return Image.FromStream(outStream);
        }
        
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
 
            return null;
        }
    }
}