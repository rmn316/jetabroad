using NUnit.Framework;
using ImprovedNoise.Image;
using ImprovedNoise.Noise;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Moq;

namespace ImprovedNoise.test.Image
{   
    [TestFixture()]
    public class GeneratorTest
    {
        /// <summary>
        /// Mock instance of a INoise object
        /// </summary>
        private Mock<INoise> mock;

        [SetUp]
        public void SetUp()
        {
            mock = new Mock<INoise>();
        }

        [TestCase("terrain")]
        [TestCase("greyscale")]
        public void TestGenerateImage(string type)
        {
            var image = new Generator(mock.Object, 200, 200, 1, type).Generate();
            Assert.IsInstanceOf(typeof(Image<Rgba32>), image);
        }

        [Test]
        public void TestGenerateThrowsException()
        {     
            Assert.That(() => new Generator(mock.Object, 200, 200, 1, "some-junk").Generate(), Throws.Exception);
        }
    }
}
