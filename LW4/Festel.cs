using System;
using System.Collections.Generic;
using System.Linq;

namespace LW4
{
    public static class Festel
    {
        private static int _roundsNumber = 10;

        private static List<TBNumber> _keys = new();
        private static List<Pair> _history = new();

        public static void GenerateKeys()
        {
            _keys = new();

            Random rn = new Random();
            _keys[0] = new TBNumber(rn.Next(30000));

            for (int i = 1; i < _roundsNumber; i++)
            {
                _keys[i] = _keys[i - 1].LeftShift;
            }
        }

        public static string Encrypt(string text)
        {
            // non encrypted
            List<TBNumber> toEncrypt = TransferStringToArray(text);

            DoManyRounds(toEncrypt, _keys);
            // array encrypted

            return TransferArrayToString(toEncrypt);
        }

        private static List<TBNumber> TransferStringToArray(string text)
        {
            List<TBNumber> resNumber = new();

            foreach (char c in text)
            {
                resNumber.Add(new TBNumber(Convert.ToInt16(c)));
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

        private static TBNumber GetNextKey(TBNumber key)
        {
            return key.LeftShift;
        }

        /// <summary>
        /// left & right values changing!
        /// </summary>
        private static void Round(TBNumber left, TBNumber right, TBNumber key)
        {
            TBNumber newLeft = right.XOR(key);
            TBNumber newRight = left;

            // save values for showing it on ui
            _history.Add(new Pair(left.Clone(), right.Clone()));

            // change values
            left = newLeft;
            right = newRight;
        }

        // keys have computed before encryption/decryption
        private static void DoManyRounds(List<TBNumber> array, List<TBNumber> keys)
        {
            for (int i = 0; i < _roundsNumber; i++)
            {
                // exception might be in case: arrays has not even number of elements
                for (int j = 0; j < array.Count; j += 2)
                {
                    Round(array[j], array[j + 1], keys[i]);                    
                }
            }
        }
    }

    internal class Pair
    {
        public TBNumber Left;
        public TBNumber Right;

        public Pair(TBNumber left, TBNumber right)
        {
            Left = left;
            Right = right;
        }
    }
}
