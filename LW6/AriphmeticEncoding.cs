using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW6
{
    public static class AriphmeticEncoding
    {
        private static readonly int numberOfSymbolsInBlock = 2;

        public static string Compression(string text, out float compressed)
        {
            text = text.ToLower();

            string res = "";

            for (int i = 0; i < text.Length; i += numberOfSymbolsInBlock)
            {
                res += CompressBlock(text, i) + '/';
            }

            compressed = ((float)text.Length - res.Length) / text.Length;

            return res;
        }

        public static string Decompression(string text)
        {
            string res = "";

            float[] blocks = text.Split('/').Where(x => !string.IsNullOrEmpty(x)).Select(float.Parse).ToArray();

            foreach (float block in blocks)
            {
                for (int i = 0; i < numberOfSymbolsInBlock; i++) // just counter
                {
                    float code = block;

                    // find symbol in dictionary
                    char c = WordsStatistic.Statistic.First(x => x.Value.X <= code && x.Value.Y > code).Key;

                    res += c.ToString();
                }
            }

            return res;
        }

        private static string CompressBlock(string text, int startIndex)
        {
            float outNumber = 0, lowBorder = 0, highBorder = 1;

            for (int i = startIndex; i < startIndex + numberOfSymbolsInBlock && i < text.Length; i++)
            {
                PointF cBorders = WordsStatistic.Statistic[text[i]];
                float oldLow = lowBorder, oldHigh = highBorder;
                highBorder = oldLow + (oldHigh - oldLow) * cBorders.Y;
                lowBorder = oldLow + (oldHigh - oldLow) * cBorders.X;
            }

            return ((lowBorder + highBorder) / 2).ToString("F30");
        }
    }
}
