using NUnit.Framework;
using System;
using ImprovedNoise.Pixel;
using SixLabors.ImageSharp.PixelFormats;

namespace ImprovedNoise.test.Pixel
{
    [TestFixture()]
    public class MonoChromeTest
    {
        [Test()]
        public void TestCreatePixel()
        {
            MonoChrome obj = new MonoChrome();
            Assert.IsInstanceOf<Rgba32>(obj.Create(22));
        }
    }
}
