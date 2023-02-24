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

        public static string Decrypt(string text)
        {
            _history = new();

            // non decrypted
            List<TBNumber> toDecrypt = TransferStringToArray(text);

            List<TBNumber> backKeys = CloneList(_keys);
            backKeys.Reverse();

            DoManyRounds(toDecrypt, backKeys);

            toDecrypt = DeleteExtraElement(toDecrypt);

            return TransferArrayToString(toDecrypt);
        }

        private static List<TBNumber> TransferStringToArray(string text)
        {
            List<TBNumber> resNumber = new();

            foreach (char c in text)
            {
                int n = Convert.ToInt32(c);

                if (n > Int16.MaxValue)
                {
                    // to make number negative back
                    n -= 65536;
                }

                resNumber.Add(new TBNumber(n));
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

        private static List<TBNumber> DeleteExtraElement(List<TBNumber> number)
        {
            if (number[number.Count - 1].Value == 0)
            {
                number.RemoveAt(number.Count - 1);
            }

            return number;
        }

        private static TBNumber F(TBNumber number, TBNumber key)
        {
            return new TBNumber((number.Value * key.Value) % Int16.MaxValue);
            //return new TBNumber(number.Value + key.Value);
        }

        private static TBNumber GetNextKey(TBNumber key)
        {
            return key.LeftShift;
        }

        private static void Round(ref TBNumber left, ref TBNumber right, TBNumber key)
        {
            // значения переменных нужно смотреть в википедии
            TBNumber x = F(right, key);
            x = x.XOR(left);

            left = x;
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

                    // на последнем раунде смена мест не нужна
                    if (i < _roundsNumber - 1)
                    {
                        // еще не последний раунд => меняем местами
                        array[j] = r;
                        array[j + 1] = l;
                    }
                    else
                    {
                        // последний раунд => оставляем все как есть
                        array[j] = l;
                        array[j + 1] = r;
                    }
                }
            }
            _lastValue = array;
        }

        private static List<TBNumber> CloneList(List<TBNumber> list)
        {
            return list.Select(x => x.Clone()).ToList();
        }
    }
}
