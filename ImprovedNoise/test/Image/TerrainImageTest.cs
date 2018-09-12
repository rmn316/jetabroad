using NUnit.Framework;
using System;
using ImprovedNoise.Image;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace ImprovedNoise.test.Image
{
    [TestFixture()]
    public class TerrainImageTest
    {
        [Test()]
        public void TestCase()
        {
            var image = new TerrainImage(200, 200, 0.1);
            Assert.IsInstanceOf(typeof(TerrainImage), image);
            Assert.IsInstanceOf(typeof(Image<Rgba32>), image.CreateImage());
        }
    }
}
