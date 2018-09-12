using System;
using SixLabors.ImageSharp.PixelFormats;
using ImprovedNoise.Pixel;
using ImprovedNoise.Noise;

namespace ImprovedNoise.Image
{
    /// <summary>
    /// Create a terrain image.
    /// </summary>
    public class TerrainImage : PerlinImage
    {   
        /// <summary>
        /// Construct a TerrainImage using width, height and the increment of perlin noise used in each pixel. 
        /// A smaller increment: e.g. 0.01, 0.001 will create a smooth biomes.
        /// A bigger increment: e.g. 1, 0.9 will create a rough biomes.
        /// </summary>
        /// <param name="noise">INoise</param>
        /// <param name="width">int</param>
        /// <param name="height">int</param>
        /// <param name="increment">double</param>
        public TerrainImage(INoise noise, int width, int height, double increment) : base(noise, width, height, increment)
        {
            
        }

        public override IPixelCreator PixelCreator { get; } = new Earth();

        protected override Rgba32 CreatePixel()
        {
            var e = CalculateElevation();
            return PixelCreator.Create(e);
        }

        /// <summary>
        /// Generate the elevation of the terrain based in multiples perlin noise numbers. 
        /// Multiple based perlin numbers is generated to create a terrain effect 
        /// </summary>
        /// <returns></returns>
        private double CalculateElevation()
        {
            var e = (CalculatePartialElevation(1.00)
                     + CalculatePartialElevation(0.50)
                     + CalculatePartialElevation(0.25)
                     + CalculatePartialElevation(0.13)
                     + CalculatePartialElevation(0.06));
            
            e = e / (1.00 + 0.50 + 0.25 + 0.13 + 0.06);
            e = PowDistribution(e);
            return e;
        }

        /// <summary>
        /// Calculation of elevation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Double</returns>
        private double CalculatePartialElevation(double value)
        {
            return value * NoiseAlgorithm.Noise(CurrentXAxis, CurrentYAxis);
        }

        /// <summary>
        /// Do a pow mathematical distribution
        /// </summary>
        /// <remarks>
        /// big factors will result in a lot of small numbers e.g 0.01 => low elevations; 
        /// small factors will result in a lot of big numbers e.g 0.9 => big elevations;
        /// </remarks>
        /// <param name="number"></param>
        /// <returns>Return the pow of the number passed in argument</returns>
        private double PowDistribution(double number)
        {
            const double factorToDistribute = 2.2;
            return Math.Pow(number, factorToDistribute);
        }
    }
}