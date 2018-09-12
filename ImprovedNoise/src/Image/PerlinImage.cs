using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using ImprovedNoise.Pixel;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace ImprovedNoise.Image
{
    public abstract class PerlinImage
    {
        protected int Height { get; }
        protected int Width { get; }

        protected double Increment { get; }

        protected double InitialXAxis { get; }
        protected double InitialYAxis { get; }
        protected double CurrentXAxis { get; private set; }
        protected double CurrentYAxis { get; private set; }

        protected Image<Rgba32> BaseImage;

        private readonly Random _seed = new Random();

        /// <summary>
        /// Constructor. Setup the initial values.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="increment"></param>
        protected PerlinImage(int width, int height, double increment)
        {
            Width = width;
            Height = height;
            Increment = increment;
            InitialXAxis = GenerateRandom();
            InitialYAxis = GenerateRandom();
            CurrentXAxis = InitialXAxis;
            CurrentYAxis = InitialYAxis;
        }

        /// <summary>
        /// Create an image using perlin pixels
        /// </summary>
        /// <returns></returns>
        public Image<Rgba32> CreateImage()
        {
            using (BaseImage = new Image<Rgba32>(Width, Height))
            {
                for (var y = 0; y < Height; y++)
                {
                    RevertXToOrigin(); // each row we need reset the X value
                    for (var x = 0; x < Width; x++)
                    {
                        BaseImage[x, y] = CreatePixel();
                        IncrementX();
                    }
                    IncrementY();
                }
                return BaseImage.Clone();
            }
        }

        /// <summary>
        /// Generate a random position
        /// </summary>
        /// <returns>Double</returns>
        protected double GenerateRandom()
        {
            return _seed.NextDouble() * _seed.Next();
        }

        /// <summary>
        /// Reset to the original position
        /// </summary>
        protected virtual void RevertXToOrigin()
        {
            CurrentXAxis = InitialXAxis;
        }

        /// <summary>
        /// Add the <code>Increment</code> to <code>CurrentXAxis</code>
        /// </summary>
        protected virtual void IncrementX()
        {
            CurrentXAxis += Increment;
        }

        /// <summary>
        /// Add the <code>Increment</code> to <code>CurrentYAxis</code>
        /// </summary>
        protected virtual void IncrementY()
        {
            CurrentYAxis += Increment;
        }

        protected abstract Rgba32 CreatePixel();

        public abstract IPixelCreator PixelCreator { get; set; }
    }
}