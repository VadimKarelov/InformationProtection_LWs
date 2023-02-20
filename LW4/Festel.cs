using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LW4
{
    public static class Festel
    {
        private static List<TBNumber> _keys = new();
        private static List<List<TBNumber>> _history = new();

        public static string Encrypt(string text)
        {

        }

        private static List<TBNumber> TransferStringToArray(string text)
        {
            List<TBNumber> resNumber = new();

            foreach (char c in text)
            {
                resNumber.Add(new TBNumber((Int16)c));
            }

            return resNumber;
        }

        private static string TransferArrayToString(List<TBNumber> number)
        {
            return number.Select(x => (char)x.Value).ToString();
        }

        private static TBNumber F(TBNumber number, TBNumber key)
        {
            return new TBNumber(number.Value * key.Value % Int16.MaxValue);
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
