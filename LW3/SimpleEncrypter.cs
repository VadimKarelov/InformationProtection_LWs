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
                result[i] = array[i] ^ key;
            }
            return result;
        }
    }
}
