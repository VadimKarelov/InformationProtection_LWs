using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LW5.BackgroundModules
{
    public static class Hamming
    {
        // notice: BitArray хранит число в обратном порядке - от младшего бита к старшему
        // то есть число 1 представлено в массиве: 1000 0000
        // число 2: 0100 0000

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

            if (res)
                corruptedbit = -1;

            return res;
        }

        public static string CorrectMessageAndConvertToString(string message, int wrongBitPosition)
        {
            if (string.IsNullOrEmpty(message))
                return "";

            List<char> chars = message.ToCharArray().ToList();
            // correct wrong bit            
            if (wrongBitPosition > -1)
                chars[wrongBitPosition - 1] = chars[wrongBitPosition - 1] == '1' ? '0' : '0';

            // find max position of control bit
            int pos = 1;
            while (pos < chars.Count) { pos *= 2; }
            pos /= 2;

            // removing bits
            while (pos != 0)
            {
                chars.RemoveAt(pos - 1);
                pos /= 2;
            }

            if (chars.Count % 8 != 0)
                return "";

            // convert to string
            string correctedMessage = "";
            for (int i = 0; i < chars.Count; i += 8)
            {
                correctedMessage += GetSymbol(chars, i);
            }

            return correctedMessage;
        }

        private static string GetSymbol(List<char> chars, int startIndex)
        {
            // порядок битов инвертирован - смотреть в начало класса
            byte pow2 = 1;
            byte symbol = 0;
            for (int i = 0; i < 8; i++)
            {
                if (chars[startIndex + i] == '1')
                {
                    symbol += pow2;
                }
                pow2 *= 2;
            }
            return Encoding.UTF8.GetString(new byte[] { symbol });
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
