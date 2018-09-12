using System;
using System.IO;
using ImprovedNoise.Image;
using ImprovedNoise.Command.Option;
using SixLabors.ImageSharp.Formats.Png;
using DocoptNet;
using ImprovedNoise.Noise;

namespace ImprovedNoise.Command
{
    public class Program
    {
        public const string Usage = @"Improved Noise.

Usage:
    improvedNoise [--height=<h>] [--width=<w>] [--increment=<inc>] [--type=<t>]
    improvedNoise (-h | --help)
    improvedNoise (-v | --version)

Options:
    -h --help           Show help screen.
    -v --version        Show version.
    --height=<h>        Height of image [default: 128].
    --width=<w>         Width of image [default: 128].
    --increment=<inc>   Set the increment of Perlin Noise, small numbers generate smooth pattern, big numbers generate rough pattern [default: 0.5].
    --type=<t>          Type of image to generate (Terrain|GreyScale) [default: greyscale].
";

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the improved noise generator!\n");

            var arguments = new Docopt().Apply(Usage, args, version: $"{nameof(ImprovedNoise)} 0.0.1", exit: true);
            var options = new NoiseOption(arguments);

            var generator = new Generator(new ImprovedPerlin(), options.Height, options.Width, options.Increment,
                options.Type);
            var image = generator.Generate();

            string imageName = $"/tmp/{DateTime.Now.Ticks}.png";
            image.Save(new FileStream(imageName, FileMode.CreateNew), new PngEncoder());
            Console.WriteLine($"Done. Image generated at local folder: {imageName}\n");
        }
    }
}