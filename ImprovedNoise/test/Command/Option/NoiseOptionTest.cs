using NUnit.Framework;
using ImprovedNoise.Command.Option;
using System.Collections.Generic;
using DocoptNet;
using ImprovedNoise.Command;

namespace ImprovedNoise.test.Command.Option
{
    [TestFixture]
    public class NoiseOptionTest
    {
        [TestCase("--width=300", 300)]
        public void TestWidth(string arg, int expectation)
        {
            var args = CreateArgs("--width=200");
            var obj = new NoiseOption(args);
            Assert.AreEqual(200, obj.Width);
        }
        
        [TestCase("--height=300", 300)]
        public void TestHeight(string arg, int expectation)
        {
            var args = CreateArgs(arg);
            var obj = new NoiseOption(args);
            
            Assert.AreEqual(expectation, obj.Height);
        }
        
        [TestCase("--increment=0.5", 0.5)]
        public void TestIncrement(string arg, double expectation)
        {
            var args = CreateArgs(arg);
            var obj = new NoiseOption(args);
           
            Assert.AreEqual(expectation, obj.Increment);
        } 
        
        [TestCase("--type=greyscale", "greyscale")]
        public void TestType(string arg, string expectation)
        {
            var args = CreateArgs(arg);
            var obj = new NoiseOption(args);
            
            Assert.AreEqual(expectation, obj.Type);
        } 
        
        /// <summary>
        /// Internal Helper method to generate the ValueObject from <code>Docopt.Apply()</code>
        /// </summary>
        /// <param name="args">string</param>
        /// <returns>IDictionary</returns>
        private IDictionary<string, ValueObject> CreateArgs(string args)
        {
            return new Docopt().Apply(Program.Usage, args);
        }
    }   
}