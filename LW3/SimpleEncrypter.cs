using System.Numerics;

namespace LW3
{
    public static class SimpleEncrypter
    {
        public static string Encrypt(string line, BigInteger key)
        {
            return TransferNumberArrayToString(EncryptNumberArray(TransferStringToNumberArray(line), key));
        }

        private static BigInteger[] TransferStringToNumberArray(string s)
        {
            BigInteger[] result = new BigInteger[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                result[i] = (int)(s[i]);
            }
            return result;
        }

        private static string TransferNumberArrayToString(BigInteger[] array)
        {
            // простой вывод чисел в столбик
            string result = "";
            for (int i = 0; i < array.Length; i++)
            {
                result += array[i].ToString() + "\n";
            }
            return result;
        }

        private static BigInteger[] EncryptNumberArray(BigInteger[] array, BigInteger key)
        {
            BigInteger[] result = new BigInteger[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = Pow(array[i], key);
            }
            return result;
        }

        private static BigInteger Pow(BigInteger value, BigInteger exponent)
        {
            BigInteger res = value;
            BigInteger pow = 1;
            int increase = 8;
            BigInteger part = exponent / increase;            
            // pow 2
            for (; pow < part; pow *= increase)
            {
                res = BigInteger.Pow(res, increase);
            }
            // remaining pows
            for (; pow <= exponent; pow++)
            {
                res *= value;
            }
            return res;
        }
    }
}
