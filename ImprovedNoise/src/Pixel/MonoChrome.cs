using SixLabors.ImageSharp.PixelFormats;

namespace ImprovedNoise.Pixel
{
    /// <summary>
    /// Create a Mono chrome pixel
    /// </summary>
    public class MonoChrome : IPixelCreator
    {
        /// <summary>
        /// Create the specified noise.
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="noise">Noise.</param>
        public Rgba32 Create(double noise)
        {
            var color = (byte)(noise * 255);
            var r = color;
            var g = color;
            var b = color;
            const byte alpha = 255;
            return new Rgba32(r, g, b, alpha);
        }
    }
}
