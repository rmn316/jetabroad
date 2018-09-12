using NUnit.Framework;
using ImprovedNoise.Image;
using ImprovedNoise.Noise;
using Moq;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace ImprovedNoise.test.Image
{
    [TestFixture]
    public class GreyScaleImageTest
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
        
        [TestCase(1, 1, 1)]
        [TestCase(5, 5, 25)]
        public void TestCreateImage(int width, int height, int expectation)
        {   
            var image = new GreyScaleImage(mock.Object, width, height, 1);
            Assert.IsInstanceOf(typeof(GreyScaleImage), image);
            Assert.IsInstanceOf(typeof(Image<Rgba32>), image.CreateImage());
            mock.Verify(stub => stub.Noise(It.IsAny<double>(), It.IsAny<double>()), Times.Exactly(expectation));
        }
    }
}
