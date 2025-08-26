using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageDitheringApp
{
    public static class Dither
    {
        // Bayer矩阵定义
        private static readonly int[,] BAYER_PATTERN_2X2 = {
            { 51, 206 },
            { 153, 102 }
        };

        private static readonly int[,] BAYER_PATTERN_3X3 = {
            { 181, 231, 131 },
            { 50, 25, 100 },
            { 156, 75, 206 }
        };

        private static readonly int[,] BAYER_PATTERN_4X4 = {
            { 15, 195, 60, 240 },
            { 135, 75, 180, 120 },
            { 45, 225, 30, 210 },
            { 165, 105, 150, 90 }
        };

        private static readonly int[,] BAYER_PATTERN_8X8 = {
            { 0, 128, 32, 160, 8, 136, 40, 168 },
            { 192, 64, 224, 96, 200, 72, 232, 104 },
            { 48, 176, 16, 144, 56, 184, 24, 152 },
            { 240, 112, 208, 80, 248, 120, 216, 88 },
            { 12, 140, 44, 172, 4, 132, 36, 164 },
            { 204, 76, 236, 108, 196, 68, 228, 100 },
            { 60, 188, 28, 156, 52, 180, 20, 148 },
            { 252, 124, 220, 92, 244, 116, 212, 84 }
        };

        private static readonly int[,] BAYER_PATTERN_16X16 = {
            { 0, 191, 48, 239, 12, 203, 60, 251, 3, 194, 51, 242, 15, 206, 63, 254 },
            { 127, 64, 175, 112, 139, 76, 187, 124, 130, 67, 178, 115, 142, 79, 190, 127 },
            { 32, 223, 16, 207, 44, 235, 28, 219, 35, 226, 19, 210, 47, 238, 31, 222 },
            { 159, 96, 143, 80, 171, 108, 155, 92, 162, 99, 146, 83, 174, 111, 158, 95 },
            { 8, 199, 56, 247, 4, 195, 52, 243, 11, 202, 59, 250, 7, 198, 55, 246 },
            { 135, 72, 183, 120, 131, 68, 179, 116, 138, 75, 186, 123, 134, 71, 182, 119 },
            { 40, 231, 24, 215, 36, 227, 20, 211, 43, 234, 27, 218, 39, 230, 23, 214 },
            { 167, 104, 151, 88, 163, 100, 147, 84, 170, 107, 154, 91, 166, 103, 150, 87 },
            { 2, 193, 50, 241, 14, 205, 62, 253, 1, 192, 49, 240, 13, 204, 61, 252 },
            { 129, 66, 177, 114, 141, 78, 189, 126, 128, 65, 176, 113, 140, 77, 188, 125 },
            { 34, 225, 18, 209, 46, 237, 30, 221, 33, 224, 17, 208, 45, 236, 29, 220 },
            { 161, 98, 145, 82, 173, 110, 157, 94, 160, 97, 144, 81, 172, 109, 156, 93 },
            { 10, 201, 58, 249, 6, 197, 54, 245, 9, 200, 57, 248, 5, 196, 53, 244 },
            { 137, 74, 185, 122, 133, 70, 181, 118, 136, 73, 184, 121, 132, 69, 180, 117 },
            { 42, 233, 26, 217, 38, 229, 22, 213, 41, 232, 25, 216, 37, 228, 21, 212 },
            { 169, 106, 153, 90, 165, 102, 149, 86, 168, 105, 152, 89, 164, 101, 148, 85 }
        };

        // 颜色离散化数组
        private static readonly byte[] VALUES_6BPP = { 0, 85, 170, 255 };
        private static readonly byte[] VALUES_9BPP = { 0, 36, 72, 108, 144, 180, 216, 255 };
        private static readonly byte[] VALUES_12BPP = { 0, 17, 34, 51, 68, 85, 102, 119, 136, 153, 170, 187, 204, 221, 238, 255 };
        private static readonly byte[] VALUES_15BPP = { 0, 8, 16, 24, 32, 40, 48, 56, 64, 80, 88, 96, 104, 112, 120, 128, 136, 144, 152, 160, 168, 176, 184, 192, 200, 208, 216, 224, 232, 240, 248, 255 };
        private static readonly byte[] VALUES_18BPP = { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 84, 88, 92, 96, 100, 104, 108, 112, 116, 120, 126, 130, 136, 140, 144, 148, 152, 156, 160, 164, 168, 172, 176, 180, 184, 188, 192, 196, 200, 204, 208, 212, 216, 220, 224, 228, 232, 236, 240, 244, 248, 252, 255 };

        // 辅助方法
        private static int Clamp(int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        private static int Gray(Color c)
        {
            return (c.R + c.G + c.B) / 3;
        }

        // 通用Bayer抖动方法
        private static Bitmap BayerDither(Bitmap original, int[,] pattern, int size, bool colorMode)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);

            for (int y = 0; y < original.Height; y++)
            {
                int row = y % size;

                for (int x = 0; x < original.Width; x++)
                {
                    int col = x % size;
                    Color pixel = original.GetPixel(x, y);

                    if (colorMode)
                    {
                        // 彩色模式
                        int r = pixel.R < pattern[col, row] ? 0 : 255;
                        int g = pixel.G < pattern[col, row] ? 0 : 255;
                        int b = pixel.B < pattern[col, row] ? 0 : 255;
                        result.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                    else
                    {
                        // 黑白模式
                        int gray = Gray(pixel);
                        int color = gray < pattern[col, row] ? 0 : 255;
                        result.SetPixel(x, y, Color.FromArgb(color, color, color));
                    }
                }
            }

            return result;
        }

        // Bayer抖动算法
        public static Bitmap Bayer2(Bitmap original, bool colorMode)
        {
            return BayerDither(original, BAYER_PATTERN_2X2, 2, colorMode);
        }

        public static Bitmap Bayer3(Bitmap original, bool colorMode)
        {
            return BayerDither(original, BAYER_PATTERN_3X3, 3, colorMode);
        }

        public static Bitmap Bayer4(Bitmap original, bool colorMode)
        {
            return BayerDither(original, BAYER_PATTERN_4X4, 4, colorMode);
        }

        public static Bitmap Bayer8(Bitmap original, bool colorMode)
        {
            return BayerDither(original, BAYER_PATTERN_8X8, 8, colorMode);
        }

        public static Bitmap Bayer16(Bitmap original, bool colorMode)
        {
            return BayerDither(original, BAYER_PATTERN_16X16, 16, colorMode);
        }

        // 高级Bayer抖动算法（支持更多位深度）
        public static Bitmap BayerRgb3bpp(Bitmap original)
        {
            return BayerRgbNbpp(original, 1);
        }

        public static Bitmap BayerRgb6bpp(Bitmap original)
        {
            return BayerRgbNbpp(original, 3);
        }

        public static Bitmap BayerRgb9bpp(Bitmap original)
        {
            return BayerRgbNbpp(original, 7);
        }

        public static Bitmap BayerRgb12bpp(Bitmap original)
        {
            return BayerRgbNbpp(original, 15);
        }

        public static Bitmap BayerRgb15bpp(Bitmap original)
        {
            return BayerRgbNbpp(original, 31);
        }

        public static Bitmap BayerRgb18bpp(Bitmap original)
        {
            return BayerRgbNbpp(original, 63);
        }

        private static Bitmap BayerRgbNbpp(Bitmap original, int ncolors)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);
            int divider = 256 / ncolors;

            for (int y = 0; y < original.Height; y++)
            {
                int row = y % 16;

                for (int x = 0; x < original.Width; x++)
                {
                    int col = x % 16;
                    Color pixel = original.GetPixel(x, y);

                    int t = BAYER_PATTERN_16X16[col, row];
                    int corr = t / ncolors;

                    int i1 = (pixel.B + corr) / divider; i1 = Clamp(i1, 0, ncolors);
                    int i2 = (pixel.G + corr) / divider; i2 = Clamp(i2, 0, ncolors);
                    int i3 = (pixel.R + corr) / divider; i3 = Clamp(i3, 0, ncolors);

                    int b = Clamp(i1 * divider, 0, 255);
                    int g = Clamp(i2 * divider, 0, 255);
                    int r = Clamp(i3 * divider, 0, 255);

                    result.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return result;
        }

        // Floyd-Steinberg抖动算法
        public static Bitmap FloydSteinberg(Bitmap original, bool colorMode)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);

            // 创建错误扩散数组
            float[,] errorR = new float[original.Width, original.Height];
            float[,] errorG = new float[original.Width, original.Height];
            float[,] errorB = new float[original.Width, original.Height];

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixel = original.GetPixel(x, y);

                    // 应用错误扩散
                    float r = Clamp((int)(pixel.R + errorR[x, y]), 0, 255);
                    float g = Clamp((int)(pixel.G + errorG[x, y]), 0, 255);
                    float b = Clamp((int)(pixel.B + errorB[x, y]), 0, 255);

                    if (colorMode)
                    {
                        // 彩色模式
                        int newR = (int)(r < 128 ? 0 : 255);
                        int newG = (int)(g < 128 ? 0 : 255);
                        int newB = (int)(b < 128 ? 0 : 255);

                        result.SetPixel(x, y, Color.FromArgb(newR, newG, newB));

                        // 计算误差
                        float errR = r - newR;
                        float errG = g - newG;
                        float errB = b - newB;

                        // 扩散误差
                        DiffuseError(errorR, errorG, errorB, errR, errG, errB, x, y, original.Width, original.Height);
                    }
                    else
                    {
                        // 黑白模式
                        float gray = (r + g + b) / 3;
                        int newVal = (int)(gray < 128 ? 0 : 255);

                        result.SetPixel(x, y, Color.FromArgb(newVal, newVal, newVal));

                        // 计算误差
                        float err = gray - newVal;

                        // 扩散误差
                        DiffuseError(errorR, errorG, errorB, err, err, err, x, y, original.Width, original.Height);
                    }
                }
            }

            return result;
        }

        // Sierra Lite抖动算法
        public static Bitmap SierraLite(Bitmap original, bool colorMode)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);

            // 创建错误扩散数组
            float[,] errorR = new float[original.Width, original.Height];
            float[,] errorG = new float[original.Width, original.Height];
            float[,] errorB = new float[original.Width, original.Height];

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixel = original.GetPixel(x, y);

                    // 应用错误扩散
                    float r = Clamp((int)(pixel.R + errorR[x, y]), 0, 255);
                    float g = Clamp((int)(pixel.G + errorG[x, y]), 0, 255);
                    float b = Clamp((int)(pixel.B + errorB[x, y]), 0, 255);

                    if (colorMode)
                    {
                        // 彩色模式
                        int newR = (int)(r < 128 ? 0 : 255);
                        int newG = (int)(g < 128 ? 0 : 255);
                        int newB = (int)(b < 128 ? 0 : 255);

                        result.SetPixel(x, y, Color.FromArgb(newR, newG, newB));

                        // 计算误差
                        float errR = r - newR;
                        float errG = g - newG;
                        float errB = b - newB;

                        // Sierra Lite误差扩散
                        SierraLiteDiffuse(errorR, errorG, errorB, errR, errG, errB, x, y, original.Width, original.Height);
                    }
                    else
                    {
                        // 黑白模式
                        float gray = (r + g + b) / 3;
                        int newVal = (int)(gray < 128 ? 0 : 255);

                        result.SetPixel(x, y, Color.FromArgb(newVal, newVal, newVal));

                        // 计算误差
                        float err = gray - newVal;

                        // Sierra Lite误差扩散
                        SierraLiteDiffuse(errorR, errorG, errorB, err, err, err, x, y, original.Width, original.Height);
                    }
                }
            }

            return result;
        }

        // Sierra抖动算法
        public static Bitmap Sierra(Bitmap original, bool colorMode)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);

            // 创建错误扩散数组
            float[,] errorR = new float[original.Width, original.Height];
            float[,] errorG = new float[original.Width, original.Height];
            float[,] errorB = new float[original.Width, original.Height];

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixel = original.GetPixel(x, y);

                    // 应用错误扩散
                    float r = Clamp((int)(pixel.R + errorR[x, y]), 0, 255);
                    float g = Clamp((int)(pixel.G + errorG[x, y]), 0, 255);
                    float b = Clamp((int)(pixel.B + errorB[x, y]), 0, 255);

                    if (colorMode)
                    {
                        // 彩色模式
                        int newR = (int)(r < 128 ? 0 : 255);
                        int newG = (int)(g < 128 ? 0 : 255);
                        int newB = (int)(b < 128 ? 0 : 255);

                        result.SetPixel(x, y, Color.FromArgb(newR, newG, newB));

                        // 计算误差
                        float errR = r - newR;
                        float errG = g - newG;
                        float errB = b - newB;

                        // Sierra误差扩散
                        SierraDiffuse(errorR, errorG, errorB, errR, errG, errB, x, y, original.Width, original.Height);
                    }
                    else
                    {
                        // 黑白模式
                        float gray = (r + g + b) / 3;
                        int newVal = (int)(gray < 128 ? 0 : 255);

                        result.SetPixel(x, y, Color.FromArgb(newVal, newVal, newVal));

                        // 计算误差
                        float err = gray - newVal;

                        // Sierra误差扩散
                        SierraDiffuse(errorR, errorG, errorB, err, err, err, x, y, original.Width, original.Height);
                    }
                }
            }

            return result;
        }

        // Atkinson抖动算法
        public static Bitmap Atkinson(Bitmap original, bool colorMode)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);

            // 创建错误扩散数组
            float[,] errorR = new float[original.Width, original.Height];
            float[,] errorG = new float[original.Width, original.Height];
            float[,] errorB = new float[original.Width, original.Height];

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixel = original.GetPixel(x, y);

                    // 应用错误扩散
                    float r = Clamp((int)(pixel.R + errorR[x, y]), 0, 255);
                    float g = Clamp((int)(pixel.G + errorG[x, y]), 0, 255);
                    float b = Clamp((int)(pixel.B + errorB[x, y]), 0, 255);

                    if (colorMode)
                    {
                        // 彩色模式
                        int newR = (int)(r < 128 ? 0 : 255);
                        int newG = (int)(g < 128 ? 0 : 255);
                        int newB = (int)(b < 128 ? 0 : 255);

                        result.SetPixel(x, y, Color.FromArgb(newR, newG, newB));

                        // 计算误差
                        float errR = r - newR;
                        float errG = g - newG;
                        float errB = b - newB;

                        // Atkinson误差扩散
                        AtkinsonDiffuse(errorR, errorG, errorB, errR, errG, errB, x, y, original.Width, original.Height);
                    }
                    else
                    {
                        // 黑白模式
                        float gray = (r + g + b) / 3;
                        int newVal = (int)(gray < 128 ? 0 : 255);

                        result.SetPixel(x, y, Color.FromArgb(newVal, newVal, newVal));

                        // 计算误差
                        float err = gray - newVal;

                        // Atkinson误差扩散
                        AtkinsonDiffuse(errorR, errorG, errorB, err, err, err, x, y, original.Width, original.Height);
                    }
                }
            }

            return result;
        }

        // 误差扩散辅助方法
        private static void DiffuseError(float[,] errorR, float[,] errorG, float[,] errorB,
                                        float errR, float errG, float errB,
                                        int x, int y, int width, int height)
        {
            // Floyd-Steinberg误差扩散
            if (x + 1 < width)
            {
                errorR[x + 1, y] += errR * 7 / 16;
                errorG[x + 1, y] += errG * 7 / 16;
                errorB[x + 1, y] += errB * 7 / 16;
            }

            if (x - 1 >= 0 && y + 1 < height)
            {
                errorR[x - 1, y + 1] += errR * 3 / 16;
                errorG[x - 1, y + 1] += errG * 3 / 16;
                errorB[x - 1, y + 1] += errB * 3 / 16;
            }

            if (y + 1 < height)
            {
                errorR[x, y + 1] += errR * 5 / 16;
                errorG[x, y + 1] += errG * 5 / 16;
                errorB[x, y + 1] += errB * 5 / 16;
            }

            if (x + 1 < width && y + 1 < height)
            {
                errorR[x + 1, y + 1] += errR * 1 / 16;
                errorG[x + 1, y + 1] += errG * 1 / 16;
                errorB[x + 1, y + 1] += errB * 1 / 16;
            }
        }

        private static void SierraLiteDiffuse(float[,] errorR, float[,] errorG, float[,] errorB,
                                             float errR, float errG, float errB,
                                             int x, int y, int width, int height)
        {
            // Sierra Lite误差扩散
            if (x + 1 < width)
            {
                errorR[x + 1, y] += errR * 2 / 4;
                errorG[x + 1, y] += errG * 2 / 4;
                errorB[x + 1, y] += errB * 2 / 4;
            }

            if (y + 1 < height)
            {
                if (x - 1 >= 0)
                {
                    errorR[x - 1, y + 1] += errR * 1 / 4;
                    errorG[x - 1, y + 1] += errG * 1 / 4;
                    errorB[x - 1, y + 1] += errB * 1 / 4;
                }

                errorR[x, y + 1] += errR * 1 / 4;
                errorG[x, y + 1] += errG * 1 / 4;
                errorB[x, y + 1] += errB * 1 / 4;
            }
        }

        private static void SierraDiffuse(float[,] errorR, float[,] errorG, float[,] errorB,
                                         float errR, float errG, float errB,
                                         int x, int y, int width, int height)
        {
            // Sierra误差扩散
            if (x + 1 < width)
            {
                errorR[x + 1, y] += errR * 5 / 32;
                errorG[x + 1, y] += errG * 5 / 32;
                errorB[x + 1, y] += errB * 5 / 32;
            }

            if (x + 2 < width)
            {
                errorR[x + 2, y] += errR * 3 / 32;
                errorG[x + 2, y] += errG * 3 / 32;
                errorB[x + 2, y] += errB * 3 / 32;
            }

            if (y + 1 < height)
            {
                if (x - 2 >= 0)
                {
                    errorR[x - 2, y + 1] += errR * 2 / 32;
                    errorG[x - 2, y + 1] += errG * 2 / 32;
                    errorB[x - 2, y + 1] += errB * 2 / 32;
                }

                if (x - 1 >= 0)
                {
                    errorR[x - 1, y + 1] += errR * 4 / 32;
                    errorG[x - 1, y + 1] += errG * 4 / 32;
                    errorB[x - 1, y + 1] += errB * 4 / 32;
                }

                errorR[x, y + 1] += errR * 5 / 32;
                errorG[x, y + 1] += errG * 5 / 32;
                errorB[x, y + 1] += errB * 5 / 32;

                if (x + 1 < width)
                {
                    errorR[x + 1, y + 1] += errR * 4 / 32;
                    errorG[x + 1, y + 1] += errG * 4 / 32;
                    errorB[x + 1, y + 1] += errB * 4 / 32;
                }

                if (x + 2 < width)
                {
                    errorR[x + 2, y + 1] += errR * 2 / 32;
                    errorG[x + 2, y + 1] += errG * 2 / 32;
                    errorB[x + 2, y + 1] += errB * 2 / 32;
                }
            }

            if (y + 2 < height)
            {
                if (x - 1 >= 0)
                {
                    errorR[x - 1, y + 2] += errR * 2 / 32;
                    errorG[x - 1, y + 2] += errG * 2 / 32;
                    errorB[x - 1, y + 2] += errB * 2 / 32;
                }

                errorR[x, y + 2] += errR * 3 / 32;
                errorG[x, y + 2] += errG * 3 / 32;
                errorB[x, y + 2] += errB * 3 / 32;

                if (x + 1 < width)
                {
                    errorR[x + 1, y + 2] += errR * 2 / 32;
                    errorG[x + 1, y + 2] += errG * 2 / 32;
                    errorB[x + 1, y + 2] += errB * 2 / 32;
                }
            }
        }

        private static void AtkinsonDiffuse(float[,] errorR, float[,] errorG, float[,] errorB,
                                           float errR, float errG, float errB,
                                           int x, int y, int width, int height)
        {
            // Atkinson误差扩散
            float atkinsonFactor = 1.0f / 8;

            if (x + 1 < width)
            {
                errorR[x + 1, y] += errR * atkinsonFactor;
                errorG[x + 1, y] += errG * atkinsonFactor;
                errorB[x + 1, y] += errB * atkinsonFactor;
            }

            if (x + 2 < width)
            {
                errorR[x + 2, y] += errR * atkinsonFactor;
                errorG[x + 2, y] += errG * atkinsonFactor;
                errorB[x + 2, y] += errB * atkinsonFactor;
            }

            if (y + 1 < height)
            {
                if (x - 1 >= 0)
                {
                    errorR[x - 1, y + 1] += errR * atkinsonFactor;
                    errorG[x - 1, y + 1] += errG * atkinsonFactor;
                    errorB[x - 1, y + 1] += errB * atkinsonFactor;
                }

                errorR[x, y + 1] += errR * atkinsonFactor;
                errorG[x, y + 1] += errG * atkinsonFactor;
                errorB[x, y + 1] += errB * atkinsonFactor;

                if (x + 1 < width)
                {
                    errorR[x + 1, y + 1] += errR * atkinsonFactor;
                    errorG[x + 1, y + 1] += errG * atkinsonFactor;
                    errorB[x + 1, y + 1] += errB * atkinsonFactor;
                }
            }

            if (y + 2 < height)
            {
                errorR[x, y + 2] += errR * atkinsonFactor;
                errorG[x, y + 2] += errG * atkinsonFactor;
                errorB[x, y + 2] += errB * atkinsonFactor;
            }
        }

        // RGBN算法实现
        public static Bitmap BayerRgbNbpp(Bitmap original, int ncolors, bool colorMode)
        {
            if (ncolors < 1) ncolors = 1;
            if (ncolors > 12) ncolors = 12;

            Bitmap result = new Bitmap(original.Width, original.Height);
            int divider = 256 / ncolors;

            for (int y = 0; y < original.Height; y++)
            {
                int row = y % 16;

                for (int x = 0; x < original.Width; x++)
                {
                    int col = x % 16;
                    Color pixel = original.GetPixel(x, y);

                    int t = BAYER_PATTERN_16X16[col, row];
                    int corr = t / ncolors;

                    if (colorMode)
                    {
                        // 彩色模式
                        int i1 = (pixel.B + corr) / divider; i1 = Clamp(i1, 0, ncolors);
                        int i2 = (pixel.G + corr) / divider; i2 = Clamp(i2, 0, ncolors);
                        int i3 = (pixel.R + corr) / divider; i3 = Clamp(i3, 0, ncolors);

                        int b = Clamp(i1 * divider, 0, 255);
                        int g = Clamp(i2 * divider, 0, 255);
                        int r = Clamp(i3 * divider, 0, 255);

                        result.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                    else
                    {
                        // 黑白模式
                        int gray = Gray(pixel);
                        int i = (gray + corr) / divider; i = Clamp(i, 0, ncolors);
                        int color = Clamp(i * divider, 0, 255);

                        result.SetPixel(x, y, Color.FromArgb(color, color, color));
                    }
                }
            }

            return result;
        }

        public static Bitmap FSRgbNbpp(Bitmap original, int ncolors, bool colorMode)
        {
            if (ncolors < 1) ncolors = 1;
            if (ncolors > 12) ncolors = 12;

            Bitmap result = new Bitmap(original.Width, original.Height);
            int divider = 256 / ncolors;

            // 创建错误扩散数组
            float[,] errorR = new float[original.Width, original.Height];
            float[,] errorG = new float[original.Width, original.Height];
            float[,] errorB = new float[original.Width, original.Height];

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixel = original.GetPixel(x, y);

                    // 应用错误扩散
                    float r = Clamp((int)(pixel.R + errorR[x, y]), 0, 255);
                    float g = Clamp((int)(pixel.G + errorG[x, y]), 0, 255);
                    float b = Clamp((int)(pixel.B + errorB[x, y]), 0, 255);

                    if (colorMode)
                    {
                        // 彩色模式
                        int i1 = (int)(b / divider); i1 = Clamp(i1, 0, ncolors);
                        int i2 = (int)(g / divider); i2 = Clamp(i2, 0, ncolors);
                        int i3 = (int)(r / divider); i3 = Clamp(i3, 0, ncolors);

                        int newB = Clamp(i1 * divider, 0, 255);
                        int newG = Clamp(i2 * divider, 0, 255);
                        int newR = Clamp(i3 * divider, 0, 255);

                        result.SetPixel(x, y, Color.FromArgb(newR, newG, newB));

                        // 计算误差
                        float errR = r - newR;
                        float errG = g - newG;
                        float errB = b - newB;

                        // 扩散误差
                        DiffuseError(errorR, errorG, errorB, errR, errG, errB, x, y, original.Width, original.Height);
                    }
                    else
                    {
                        // 黑白模式
                        float gray = (r + g + b) / 3;
                        int i = (int)(gray / divider); i = Clamp(i, 0, ncolors);
                        int newVal = Clamp(i * divider, 0, 255);

                        result.SetPixel(x, y, Color.FromArgb(newVal, newVal, newVal));

                        // 计算误差
                        float err = gray - newVal;

                        // 扩散误差
                        DiffuseError(errorR, errorG, errorB, err, err, err, x, y, original.Width, original.Height);
                    }
                }
            }

            return result;
        }

        public static Bitmap SierraRgbNbpp(Bitmap original, int ncolors, bool colorMode)
        {
            if (ncolors < 1) ncolors = 1;
            if (ncolors > 12) ncolors = 12;

            Bitmap result = new Bitmap(original.Width, original.Height);
            int divider = 256 / ncolors;

            // 创建错误扩散数组
            float[,] errorR = new float[original.Width, original.Height];
            float[,] errorG = new float[original.Width, original.Height];
            float[,] errorB = new float[original.Width, original.Height];

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixel = original.GetPixel(x, y);

                    // 应用错误扩散
                    float r = Clamp((int)(pixel.R + errorR[x, y]), 0, 255);
                    float g = Clamp((int)(pixel.G + errorG[x, y]), 0, 255);
                    float b = Clamp((int)(pixel.B + errorB[x, y]), 0, 255);

                    if (colorMode)
                    {
                        // 彩色模式
                        int i1 = (int)(b / divider); i1 = Clamp(i1, 0, ncolors);
                        int i2 = (int)(g / divider); i2 = Clamp(i2, 0, ncolors);
                        int i3 = (int)(r / divider); i3 = Clamp(i3, 0, ncolors);

                        int newB = Clamp(i1 * divider, 0, 255);
                        int newG = Clamp(i2 * divider, 0, 255);
                        int newR = Clamp(i3 * divider, 0, 255);

                        result.SetPixel(x, y, Color.FromArgb(newR, newG, newB));

                        // 计算误差
                        float errR = r - newR;
                        float errG = g - newG;
                        float errB = b - newB;

                        // Sierra误差扩散
                        SierraDiffuse(errorR, errorG, errorB, errR, errG, errB, x, y, original.Width, original.Height);
                    }
                    else
                    {
                        // 黑白模式
                        float gray = (r + g + b) / 3;
                        int i = (int)(gray / divider); i = Clamp(i, 0, ncolors);
                        int newVal = Clamp(i * divider, 0, 255);

                        result.SetPixel(x, y, Color.FromArgb(newVal, newVal, newVal));

                        // 计算误差
                        float err = gray - newVal;

                        // Sierra误差扩散
                        SierraDiffuse(errorR, errorG, errorB, err, err, err, x, y, original.Width, original.Height);
                    }
                }
            }

            return result;
        }
    }
}