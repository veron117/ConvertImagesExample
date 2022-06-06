using System.Drawing;
using System.Drawing.Imaging;
using ImageProcessor.Imaging.Filters.Photo;

namespace ImageConvert.Helpers
{
    public class GrayFilter : MatrixFilterBase
    {
        private static ColorMatrix Gray
        {
            get
            {
                float[][] newColorMatrix = new[]
                {
                    new[] {.3f, .3f, .3f, 0, 0},
                    new[] {.59f, .59f, .59f, 0, 0},
                    new[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                };
                return new ColorMatrix(newColorMatrix);
            }
        }

        public override ColorMatrix Matrix => Gray;

        public override Bitmap TransformImage(Image source, Image destination)
        {
            using (Graphics graphics = Graphics.FromImage(destination))
            {
                using (ImageAttributes imageAttr = new ImageAttributes())
                {
                    imageAttr.SetColorMatrix(this.Matrix);
                    Rectangle destRect = new Rectangle(0, 0, source.Width, source.Height);
                    graphics.DrawImage(source, destRect, 0, 0, source.Width, source.Height, GraphicsUnit.Pixel, imageAttr);
                }
            }
            return (Bitmap)destination;
        }
    }
}