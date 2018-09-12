using System.Collections.Generic;
using DocoptNet;
using System;

namespace ImprovedNoise.Command.Option
{
    public class NoiseOption : IOption
    {
        public int Width { get; }
        public int Height { get; }
        public double Increment { get; }
        public string Type { get; }

        public NoiseOption(IDictionary<string, ValueObject> arguments)
        {
            Width = arguments["--width"].AsInt;
            Height = arguments["--height"].AsInt;
            Increment = Double.Parse(arguments["--increment"].Value.ToString());
            Type = arguments["--type"].ToString();
        }
    }
}