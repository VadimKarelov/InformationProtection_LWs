using System.Collections.Generic;
using System.Linq;

namespace LW6
{
    public static class AriphmeticEncoding
    {
        private static readonly int numberOfSymbolsInBlock = 10;

        public static string Compression(string text, out double compressed)
        {
            text = text.ToLower();

            string res = "";

            for (int i = 0; i < text.Length; i += numberOfSymbolsInBlock)
            {
                res += CompressBlock(text, i) + '/';
            }

            // add file length
            res += text.Length;

            compressed = ((double)text.Length - res.Split('/').Length * 4) / text.Length;

            return res;
        }

        public static string Decompression(string text)
        {
            string res = "";

            List<double> blocks = text.Split('/').Where(x => !string.IsNullOrEmpty(x)).Select(double.Parse).ToList();
            // get number of symbols and remove it from blocks
            int numberOfSymbols = (int)blocks.Last();
            blocks.Remove(blocks.Count - 1);

            int symbolsDecompressed = 0;

            foreach (double block in blocks)
            {
                double code = block;

                for (int i = 0; i < numberOfSymbolsInBlock && symbolsDecompressed < numberOfSymbols; i++, symbolsDecompressed++) // just counter
                {
                    // find symbol in dictionary
                    KeyValuePair<char, Pair> pair = WordsStatistic.Statistic.First(x => x.Value.L <= code && x.Value.R > code);

                    char c = pair.Key;

                    res += c.ToString();

                    code = (code - pair.Value.L) / (pair.Value.R - pair.Value.L);
                }
            }

            return res;
        }

        private static string CompressBlock(string text, int startIndex)
        {
            double outNumber = 0, lowBorder = 0, highBorder = 1;

            for (int i = startIndex; i < startIndex + numberOfSymbolsInBlock && i < text.Length; i++)
            {
                Pair cBorders = WordsStatistic.Statistic[text[i]];
                double oldLow = lowBorder, oldHigh = highBorder;
                highBorder = oldLow + (oldHigh - oldLow) * cBorders.R;
                lowBorder = oldLow + (oldHigh - oldLow) * cBorders.L;
            }

            return ((lowBorder + highBorder) / 2).ToString("F30");
        }
    }
}
