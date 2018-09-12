using NUnit.Framework;
using ImprovedNoise.Pixel;

namespace ImprovedNoise.test.Pixel
{
    [TestFixture()]
    public class EarthTest
    {
        [Test(), TestCaseSource("CreateProvider")]
        public void TestCreate(double increment, object expectedImage)
        {
            var pixel = new Earth().Create(increment);
            Assert.AreEqual(expectedImage, pixel);
        }

        static object[] CreateProvider = {
            new object[] {0.06, Earth.OCEAN},
            new object[] {0.10, Earth.WATER},
            new object[] {0.14, Earth.SAND},
            new object[] {0.71, Earth.SNOW},
            new object[] {0.52, Earth.MOUNTAIN},
            new object[] {0.33, Earth.FOREST},
            new object[] {0.25, Earth.GRASS}
        };

    }
}