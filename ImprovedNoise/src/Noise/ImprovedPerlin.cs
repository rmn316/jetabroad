using System;

namespace ImprovedNoise.Noise
{
    public class ImprovedPerlin : INoise
    {
        /// <summary>
        /// The permutation. Hash lookup table as defined by Ken Perlin.  This is a randomly
        /// arranged array of all numbers from 0-255 inclusive.
        /// </summary>
        private readonly int[] _permutation = { 151,160,137,91,90,15,
            131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
            190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
            88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
            77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
            102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
            135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
            5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
            223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
            129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
            251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
            49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
            138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180
        };

        /// <summary>
        /// Doubled permutation to avoid overflow
        /// </summary>
        private readonly int[] _doubledPermutation;

        public ImprovedPerlin()
        {
            _doubledPermutation = new int[512];
            for (var x = 0; x < 512; x++)
            {
                _doubledPermutation[x] = _permutation[x % 256];
            }
        }

        public double Noise(double x, double y)
        {
            return Noise(x, y, 0d);
        }

        public double Noise(double x, double y, double z)
        {
            // Calculate the "unit cube" that the point asked will be located in
            var xi = (int)x & 255;
            // The left bound is ( |_x_|,|_y_|,|_z_| ) and the right bound is that
            var yi = (int)y & 255;
            // plus 1.  Next we calculate the location (from 0.0 to 1.0) in that cube.
            var zi = (int)z & 255;
            // We also fade the location to smooth the result.
            var xf = x - (int)x;  
            var yf = y - (int)y;
            var zf = z - (int)z;
            var u = Fade(xf);
            var v = Fade(yf);
            var w = Fade(zf);

            // This here is Perlin's hash function.  We take our x value (remember,
            var a = _doubledPermutation[xi] + yi;
            // between 0 and 255) and get a random value (from our p[] array above) between
            var aa = _doubledPermutation[a] + zi;
            // 0 and 255.  We then add y to it and plug that into p[], and add z to that.
            var ab = _doubledPermutation[a + 1] + zi;
            // Then, we get another random value by adding 1 to that and putting it into p[]
            var b = _doubledPermutation[xi + 1] + yi;
            var ba = _doubledPermutation[b] + zi;
            // we plug aa, ab, ba, and bb back into p[] along with their +1's to get another set.
            // in the end we have 8 values between 0 and 255 - one for each vertex on the unit cube.
            // These are all interpolated together using u, v, and w below.
            var bb = _doubledPermutation[b + 1] + zi;

            double x1, x2, y1, y2;
            x1 = Lerp(Gradient(_doubledPermutation[aa], xf, yf, zf),          // This is where the "magic" happens.  We calculate a new set of p[] values and use that to get
                        Gradient(_doubledPermutation[ba], xf - 1, yf, zf),            // our final gradient values.  Then, we interpolate between those gradients with the u value to get
                        u);                                     // 4 x-values.  Next, we interpolate between the 4 x-values with v to get 2 y-values.  Finally,
            x2 = Lerp(Gradient(_doubledPermutation[ab], xf, yf - 1, zf),          // we interpolate between the y-values to get a z-value.
                        Gradient(_doubledPermutation[bb], xf - 1, yf - 1, zf),
                        u);                                     // When calculating the p[] values, remember that above, p[a+1] expands to p[xi]+yi+1 -- so you are
            y1 = Lerp(x1, x2, v);                               // essentially adding 1 to yi.  Likewise, p[ab+1] expands to p[p[xi]+yi+1]+zi+1] -- so you are adding
                                                                // to zi.  The other 3 parameters are your possible return values (see grad()), which are actually
            x1 = Lerp(Gradient(_doubledPermutation[aa + 1], xf, yf, zf - 1),      // the vectors from the edges of the unit cube to the point in the unit cube itself.
                        Gradient(_doubledPermutation[ba + 1], xf - 1, yf, zf - 1),
                        u);
            x2 = Lerp(Gradient(_doubledPermutation[ab + 1], xf, yf - 1, zf - 1),
                        Gradient(_doubledPermutation[bb + 1], xf - 1, yf - 1, zf - 1),
                        u);
            y2 = Lerp(x1, x2, v);

            // For convenience we bound it to 0 - 1 (theoretical min/max before is -1 - 1)
            return (Lerp(y1, y2, w) + 1) / 2;
        }

        /// <summary>
        /// Gradient the specified hash, x, y and z.
        /// </summary>
        /// <returns>The gradient.</returns>
        /// <param name="hash">Hash.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <exception cref="ArgumentOutOfRangeException">Out of range exception on invalid hash</exception>
        private double Gradient(int hash, double x, double y, double z)
        {
            // switch statement has a speed improvement over the traditional logic by ken perlin.
            switch (hash & 0xF)
            {
                case 0x0: return x + y;
                case 0x1: return -x + y;
                case 0x2: return x - y;
                case 0x3: return -x - y;
                case 0x4: return x + z;
                case 0x5: return -x + z;
                case 0x6: return x - z;
                case 0x7: return -x - z;
                case 0x8: return y + z;
                case 0x9: return -y + z;
                case 0xA: return y - z;
                case 0xB: return -y - z;
                case 0xC: return y + x;
                case 0xD: return -y + z;
                case 0xE: return y - x;
                case 0xF: return -y - z;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Fade function as defined by Ken Perlin.  This eases coordinate values
        /// so that they will "ease" towards integral values.  This ends up smoothing
        /// the final output.
        /// </summary>
        /// <returns>The fade.</returns>
        /// <param name="t">T.</param>
        private double Fade(double t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);         // 6t^5 - 15t^4 + 10t^3
        }

        /// <summary>
        /// Lerp the specified a, b and x.
        /// </summary>
        /// <returns>The lerp.</returns>
        /// <param name="a">The alpha component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="x">The x coordinate.</param>
        private double Lerp(double a, double b, double x)
        {
            return a + x * (b - a);
        }
    }
}
