using ImprovedNoise.Noise;
using NUnit.Framework;

namespace ImprovedNoise.test.Noise
{
    [TestFixture]
    public class ImprovedPerlinTest
    {
        /// <summary>
        /// ImprovedPerlin Instance.
        /// </summary>
        private INoise improvedPerlin;
        
        [SetUp]
        public void SetUp()
        {
            improvedPerlin = new ImprovedPerlin();
        }
        
        [TestCase(1, 2, 0, 0.5d)]
        [TestCase(20, 40, 4, 0.5d)]
        public void TestNoise(int x, int y, int z, double expectation)
        {
            Assert.AreEqual(expectation, improvedPerlin.Noise(x, y, z));
        }   
    }   
}