namespace RyszardopolisPanelSterowania.Utils
{
    using System;
    using System.Drawing;
    using Extensions;

    internal class Noise
    {
        public static Bitmap NoiseFromImage()
        {
            Bitmap orig = new Bitmap(".\\Texture.bmp");
            Bitmap bmp = new Bitmap(orig.Width, orig.Height);
            for (int x = 0; x < orig.Width; x++)
            {
                for (int y = 0; y < orig.Height; y++)
                {
                    var pixel = orig.GetPixel(x, y);
                    int A = (pixel.R + pixel.G + pixel.B) / 3;
                    bmp.SetPixel(x, y, Color.FromArgb(256 - A, Color.Black));
                }
            }
            bmp.Save(".\\AlphaTexture.bmp");
            return bmp;
        }

        public static Bitmap GenerateNoise(int size, float blend = 0, byte min = 0, byte max = byte.MaxValue)
        {
            Bitmap bmp = new Bitmap(size, size);
            Random random = new Random(10);
            for (int i = 0; i < size; i++)
            {
                var buffer = new byte[size];
                random.NextBytes(buffer);
                for (int j = 0; j < size; j++)
                {
                    buffer[j] = (byte) ((buffer[j] * (max - min)) / Byte.MaxValue + min);
                    bmp.SetPixel(i, j, Color.FromArgb(buffer[j], Color.Black));
                }
            }
            for ( ; blend > 0; blend--)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        Color color = bmp.GetPixel(x, y);

                        if (x - 1 >= 0    && y - 1 >= 0    ) color = color.Blend(bmp.GetPixel(x - 1, y - 1));
                        if (x - 0 >= 0    && y - 1 >= 0    ) color = color.Blend(bmp.GetPixel(x - 0, y - 1));
                        if (x + 1 <  size && y - 1 >= 0    ) color = color.Blend(bmp.GetPixel(x + 1, y - 1));
                        if (x - 1 >= 0    && y - 0 >= 0    ) color = color.Blend(bmp.GetPixel(x - 1, y - 0));
                        if (x + 1 <  size && y - 0 >= 0    ) color = color.Blend(bmp.GetPixel(x + 1, y - 0));
                        if (x - 1 >= 0    && y + 1 <  size ) color = color.Blend(bmp.GetPixel(x - 1, y + 1));
                        if (x - 0 >= 0    && y + 1 <  size ) color = color.Blend(bmp.GetPixel(x - 0, y + 1));
                        if (x + 1 <  size && y + 1 <  size ) color = color.Blend(bmp.GetPixel(x + 1, y + 1));

                        bmp.SetPixel(x, y, color);
                    }
                }
            }
            bmp.Save(".\\Noise_0.bmp");
            return bmp;
        }

        public static Bitmap GeneratePerlinNoise(int size, byte min = 0, byte max = byte.MaxValue)
        {
            Bitmap bmp = new Bitmap(size, size);
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    float x1 = (float) x / (float) size;
                    float y1 = (float) y / (float) size;
                    var value = (int) (PerlinNoise.OctaveNoise(x1, y1, 6, 10) * max);
                    Color color = Color.FromArgb(value, Color.Black);
                    bmp.SetPixel(x, y, color);
                }
            }
            bmp.Save(".\\Perlin.bmp");
            return bmp;
        }
    }
}
