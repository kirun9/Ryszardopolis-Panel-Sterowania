namespace RyszardopolisPanelSterowania.Utils
{
    using System;

    internal class PerlinNoise
    {
        private static readonly int[] permutation = { 151,160,137,91,90,15,                 // Hash lookup table as defined by Ken Perlin.  This is a randomly
            131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,    // arranged array of all numbers from 0-255 inclusive.
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

        private static readonly int[] p; // Doubled permutation to avoid overflow

        private static readonly int repeat = 0;

        static PerlinNoise()
        {
            p = new int[512];
            for (int x = 0; x < 512; x++)
            {
                p[x] = permutation[x % 256];
            }
        }

        public static float OctaveNoise(float x, float y, int octaves, float persistance)
        {
            float total = 0;
            float frequency = 1;
            float amplitude = 1;
            float maxValue = 0;
            for (int i = 0; i < octaves; i++)
            {
                total += Noise(x * frequency, y * frequency) * amplitude;
                maxValue += amplitude;
                amplitude *= persistance;
                frequency *= 2;
            }

            return total / maxValue;
        }

        public static float Noise(float x, float y)
        {
            if (repeat > 0) // If we have any repeat on, change the coordinates to their "local" repetitions
            {
                x = x % repeat;
                y = y % repeat;
            }

            int xi = (int) x & 255; // Calculate the "unit cube" that the point asked will be located in
            int yi = (int) y & 255; // plus 1. Next we calculate the location (from 0.0 to 1.0) in that cube.
            float xf = x - (int) x;
            float yf = y - (int) y;

            float u = Fade(xf);
            float v = Fade(yf);

            int aa, ab, ba, bb;
            aa = p[p[xi     ] + yi     ];
            ab = p[p[xi     ] + Inc(yi)];
            ba = p[p[Inc(xi)] + yi     ];
            bb = p[p[Inc(xi)] + Inc(yi)];

            float x1, x2, y1;
            x1 = Lerp(Grad(aa, xf, yf), Grad(ba, xf - 1, yf), u);
            x2 = Lerp(Grad(ab, xf, yf-1), Grad(bb, xf-1, yf-1), u);
            y1 = Lerp(x1, x2, v);
            return (y1 + 1) / 2;
        }

        private static float Fade(float t)
        {
            // 6t^5 - 15t^4 + 10t^3
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        private static int Inc(int num)
        {
            num++;
            if (repeat > 0)
                num %= repeat;
            return num;
        }

        private static float Grad(int hash, float x, float y)
        {
            int h = hash & 15;                      // Take the hashed value and take the first 4 bits of it (15 == 0b1111)
            ////float u = h < 8 ? x : y; /*0b1000*/    // If the most significant bit (MSB) of the hash is 0 then set u = x. Otherwise y.
            ////
            ////float v;
            ////if (h < 4)                              // If the first and second significant bits are 0 set v = y
            ////    v = y;
            ////else /*(h == 12 || h == 14)*/           // If the first and second significant bits are 1 set v = x
            ////    v = x;
            ////
            ////return ((h&1) == 0 ? u : -u) + ((h&2) == 0 ? v : -v);   // Use the last 2 bits to decide if u and v are positive or negative. Then return their addition.
            return ((h & 1) == 0 ? x : -x) + ((h & 2) == 0 ? y : -y);   // Use the last 2 bits to decide if u and v are positive or negative. Then return their addition.
        }

        // Linear interpolation
        private static float Lerp(float a, float b, float x)
        {
            return a + x * (b - a);
        }
    }
}
