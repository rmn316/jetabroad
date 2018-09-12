using ImprovedNoise.Noise;
using NUnit.Framework;

namespace ImprovedNoise.test.Noise
{
    [TestFixture]
    public class ImprovedPerlinTest
    {
        [TestCase(1, 2, 0, 0.5d)]
        [TestCase(20, 40, 4, 0.5d)]
        public void TestNoise(int x, int y, int z, double expectation)
        {
            Assert.AreEqual(expectation, ImprovedPerlin.Noise(x, y, z));
        }   
    }   
}