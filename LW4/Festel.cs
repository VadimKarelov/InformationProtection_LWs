using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LW4
{
    public static class Festel
    {
        private static List<BitArray> _keys = new();
        private static List<List<BitArray>> _history = new();

        public static string Encrypt(string text)
        {

        }

        /// <summary>
        /// Returns text in array of BitArray[16] (16 bit size) with even number of elements
        /// </summary>
        private static List<BitArray> TransferStringToArray(string text)
        {
            // transfer to bytes array (8 bit)
            Encoding encoding = Encoding.UTF8;
            byte[] t = encoding.GetBytes(text);

            // transfer to bit array
            BitArray bitArray = new BitArray(t);
            List<BitArray> res = new();

            // counter for full array
            int i = 0;
            while (i < bitArray.Length)
            {
                // counter for one BitArray with 16 bits
                int j = 0;
                BitArray oneElement = new BitArray(16);
                while (i < bitArray.Length && j < 16)
                {
                    oneElement[j] = bitArray[i];
                    i++;
                    j++;
                }
                res.Add(oneElement);
            }

            // for even number of elements
            if (res.Count % 2 != 0)
            {
                res.Add(new BitArray(16));
            }

            return res;
        }

        /// <summary>
        /// Transfer array of BitArray (Count = 16) to string
        /// </summary>
        private static string TransferArrayToString(List<BitArray> arrays)
        {
            List<byte> bytes = new();

            // transfer bits to bytes
            for (int i = 0; i < arrays.Count; i++)
            {
                BitArray x1 = new BitArray(8);
                BitArray x2 = new BitArray(8);

                // copy first byte
                for (int j = 0; j < x1.Count; j++)
                {
                    x1[j] = arrays[i][j];
                }

                // copy second byte
                for (int j = 0; j < x2.Count; j++)
                {
                    x2[j] = arrays[i][j + x1.Count];
                }

                // create arrays of byte to copying
                byte[] y1 = new byte[1];
                byte[] y2 = new byte[1];

                // copy bits to bytes
                x1.CopyTo(y1, 0);
                x2.CopyTo(y2, 0);

                // get first and alone element of arrays
                bytes.Add(y1[0]);
                bytes.Add(y2[0]);
            }

            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(bytes.ToArray());
        }

        private static BitArray F(BitArray number, BitArray key)
        {
            return new BitArray(number.And(key));
        }

        private static void Round(ref BitArray left, ref BitArray right, ref BitArray key)
        {
            // save previous left branch value
            BitArray leftSave = new BitArray(left.Count);
            byte[] t = new byte[2];
            left.CopyTo(t, 0);
            leftSave = new BitArray(t);

            // coumpute new branches value
            left = F(left, key).Xor(right);
            right = leftSave;
        }

        private static void DoManyRounds(List<BitArray> arrays, BitArray initKey, int roundsNumber)
        {
            _keys = new();
            _keys.Add(initKey);

            BitArray key = initKey;

            for (int i = 0; i < roundsNumber; i++)
            {
                // exception might be in case: arrays has not even number of elements
                for (int j = 0; i < arrays.Count; j += 2)
                {
                    Round(ref arrays[j], ref arrays[j + 1], ref  key);
                    // change key for next round
                    key = key.LeftShift(1);
                    _keys.Add(key);
                }
            }
        }
    }
}
