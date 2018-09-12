using NUnit.Framework;
using System;
using ImprovedNoise.Image;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace ImprovedNoise.test.Image
{
    [TestFixture]
    public class GreyScaleImageTest
    {
        [Test]
        public void TestCreateImage()
        {
            var image = new GreyScaleImage(1, 1, 1);
            Assert.IsInstanceOf(typeof(GreyScaleImage), image);
            Assert.IsInstanceOf(typeof(Image<Rgba32>), image.CreateImage());
        }
    }
}
