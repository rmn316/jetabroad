using SixLabors.ImageSharp.PixelFormats;

namespace ImprovedNoise.Pixel
{
    public interface IPixelCreator
    {
        /// <summary>
        /// Create an rgba pixel
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="noise">Noise.</param>
        Rgba32 Create(double noise);
    }
}
