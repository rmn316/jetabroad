using NUnit.Framework;
using System;
using ImprovedNoise.Image;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImprovedNoise.test.Image
{
    [TestFixture()]
    public class GeneratorTest
    {
        [TestCase("terrain")]
        [TestCase("greyscale")]
        public void TestGenerateImage(string type)
        {
            var image = new Generator(200, 200, 1, type).Generate();
            Assert.IsInstanceOf(typeof(Image<Rgba32>), image);
        }

        [Test]
        public void TestGenerateThrowsException()
        {           
            Assert.That(() => new Generator(200, 200, 1, "some-junk").Generate(), Throws.Exception);
        }
    }
}
