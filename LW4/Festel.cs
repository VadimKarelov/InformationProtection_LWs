using System;
using System.Collections.Generic;
using System.Linq;

namespace LW4
{
    public static class Festel
    {
        public static List<TBNumber> Keys => _keys;
        public static List<List<TBNumber>> History => _history;
        public static List<TBNumber> LastValue => _lastValue;

        private static int _roundsNumber = 10;

        private static List<TBNumber> _keys = new();
        private static List<List<TBNumber>> _history = new();
        private static List<TBNumber> _lastValue = new();

        public static void GenerateKeys()
        {
            _keys = new();

            Random rn = new Random();
            _keys.Add(new TBNumber(rn.Next(30000)));

            for (int i = 1; i < _roundsNumber; i++)
            {
                _keys.Add(GetNextKey(_keys[i - 1]));
            }
        }

        public static string Encrypt(string text)
        {
            _history = new();

            // non encrypted
            List<TBNumber> toEncrypt = TransferStringToArray(text);

            MakeEvenElements(toEncrypt);

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

        private static void MakeEvenElements(List<TBNumber> number)
        {
            if (number.Count % 2 != 0)
            {
                number.Add(new TBNumber(0));
            }
        }

        private static string TransferArrayToString(List<TBNumber> number)
        {
            string res = "";
            foreach (TBNumber num in number)
            {
                res += (char)num.Value;
            }
            return res;
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
        private static void Round(ref TBNumber left, ref TBNumber right, TBNumber key)
        {
            TBNumber newLeft = right.XOR(F(left, key));
            TBNumber newRight = left;

            // change values
            left = newLeft;
            right = newRight;
        }

        // keys have computed before encryption/decryption
        private static void DoManyRounds(List<TBNumber> array, List<TBNumber> keys)
        {
            for (int i = 0; i < _roundsNumber; i++)
            {
                // save for showing on ui
                _history.Add(array.Select(x => x.Clone()).ToList());

                // exception might be in case: arrays has not even number of elements
                for (int j = 0; j < array.Count; j += 2)
                {
                    TBNumber l = array[j];
                    TBNumber r = array[j + 1];
                    Round(ref l, ref r, keys[i]);
                    array[j] = l;
                    array[j + 1] = r;
                }
            }
            _lastValue = array;
        }
    }
}
