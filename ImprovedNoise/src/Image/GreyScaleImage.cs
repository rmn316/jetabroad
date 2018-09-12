using ImprovedNoise.Pixel;
using SixLabors.ImageSharp.PixelFormats;
using ImprovedNoise.Noise;

namespace ImprovedNoise.Image
{
    public class GreyScaleImage : PerlinImage
    {
        /// <summary>
        /// Construct a GrayScale image using width, height and the increment of perlin used in each pixel. 
        /// A smaller increment: e.g. 0.01, 0.001 will create a smooth effect.
        /// A bigger increment: e.g. 1, 0.9 will create a rough.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="increment"></param>
        public GreyScaleImage(int width, int height, double increment) : base(width, height, increment)
        {

        }

        public override IPixelCreator PixelCreator { get; set; } = new MonoChrome();

        protected override Rgba32 CreatePixel()
        {
            return PixelCreator.Create(
                ImprovedPerlin.Noise(CurrentXAxis, CurrentYAxis)
            );
        }
    }
}
