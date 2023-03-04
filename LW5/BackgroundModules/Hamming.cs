using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LW5.BackgroundModules
{
    public static class Hamming
    {
        public static string ConvertToBits(string text)
        {
            return string.Concat((Encoding.UTF8.GetBytes(text)).Select(x => new byte[] { x }).Select(x => new BitArray(x)).Select(x => BitArrayToString(x)));
        }

        public static string AddControlBits(string text)
        {
            List<char> chars = text.ToCharArray().ToList();            
            // contains position (from 1) to add control bit
            int pos = 1;
            // add bits
            while (pos < chars.Count)
            {
                chars.Insert(pos - 1, '0');
                pos *= 2;
            }

            //count bits values
            pos = 1;
            // cycle for controls bit
            while (pos < chars.Count)
            {
                int ind = pos - 1;
                // cycle for counting one bit
                int counter = 0;
                while (ind < chars.Count)
                {
                    int n = 0;
                    
                    while (n < pos && ind + n < chars.Count)
                    {
                        if (chars[ind + n] == '1')
                        {
                            counter++;
                        }
                        n++;
                    }

                    ind = ind + pos * 2;
                }

                chars[pos - 1] = counter % 2 == 1 ? '1' : '0';
                pos *= 2;                
            }

            return new string(chars.ToArray());
        }

        public static bool IsMessageNotCorrupted(string text, out int corruptedbit)
        {
            bool res = true;
            corruptedbit = 0;

            List<char> chars = text.ToCharArray().ToList();

            //count bits values
            int pos = 1;
            // cycle for controls bit
            while (pos < chars.Count)
            {
                int ind = pos - 1;
                // cycle for counting one bit
                int counter = 0;
                while (ind < chars.Count)
                {
                    int n = 0;

                    while (n < pos && ind + n < chars.Count)
                    {
                        if (chars[ind + n] == '1')
                        {
                            counter++;
                        }
                        n++;
                    }

                    ind = ind + pos * 2;
                }

                if (chars[pos - 1] == '1')
                    counter--;

                char b = counter % 2 == 0 ? '0' : '1';

                if (chars[pos - 1] != b)
                {
                    corruptedbit += pos;
                    res = false;
                }

                pos *= 2;
            }

            return res;
        }

        private static string BitArrayToString(BitArray bits)
        {
            string res = "";
            for (int i = 0; i < bits.Length; i++)
            {
                res += bits[i] ? "1" : "0";
            }
            return res;
        }
    }
}
