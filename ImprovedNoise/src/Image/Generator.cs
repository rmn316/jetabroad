using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImprovedNoise.Image
{
    /// <summary>
    /// Image Generator.
    /// </summary>
    public class Generator
    {
        public PerlinImage Image {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ImprovedNoise.src.Image.Generator"/> class.
        /// </summary>
        /// <param name="height">Height.</param>
        /// <param name="width">Width.</param>
        /// <param name="increment">Increment value as a double</param>
        /// <param name="type">Type.</param>
        public Generator(int height, int width, double increment, string type)
        {
         
            switch (type) {
                case "terrain":
                    Image = new TerrainImage(width, height, increment);
                    break;
                case "greyscale":
                    Image = new GreyScaleImage(width, height, increment);
                    break;
                default:
                    throw new ArgumentException("type of image not supported");
            }
        }

        /// <summary>
        /// Generate the image.
        /// </summary>
        /// <returns>Image</returns>
        public Image<Rgba32> Generate()
        {
            return Image.CreateImage();
        }
    }
}
