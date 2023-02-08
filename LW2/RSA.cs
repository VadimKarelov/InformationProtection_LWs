using System;
using System.Numerics;

namespace LW2
{
    public class RSA
    {
        public BigInteger p;
        public BigInteger q;
        public BigInteger e;
        public BigInteger n;
        public BigInteger d;

        private bool _isInitialized = false;

        /// <summary>
        /// Return private key d
        /// </summary>
        public BigInteger CreateKeys(BigInteger pp, BigInteger qq)
        {
            p = pp;
            q = qq;
            n = p * q;
            d = GetD(p, q);
            e = GetE(p, q, d);
            _isInitialized = true;
            return d;
        }

        public string Encrypt(string s)
        {
            if (!_isInitialized)
                throw new Exception("Keys are not initialized");

            return TransferNumberArrayToString(EncryptNumberArray(TransferStringToNumberArray(s)));
        }

        private BigInteger GetD(BigInteger p, BigInteger q)
        {
            BigInteger t = (p - 1) * (q - 1);
            BigInteger res;

            // по порядку ищем взаимо простые числа от 2 до t + 1
            for (res = 2; res < t + 1; res++)
            {
                if (t % res == 0)
                {
                    break;
                }
            }

            return res;
        }

        private BigInteger GetE(BigInteger p, BigInteger q, BigInteger d)
        {
            BigInteger t = (p - 1) * (q - 1);
            BigInteger n;

            for (n = 1; (n * t + 1) % d != 0; n++)
            { }

            BigInteger e = (t * n + 1) / d;

            if ((e * d) % t != 1)
                throw new Exception(((e * d) % t).ToString());

            return e;
        }

        private BigInteger[] TransferStringToNumberArray(string s)
        {
            BigInteger[] result = new BigInteger[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                result[i] = (int)s[i];
            }
            return result;
        }

        private string TransferNumberArrayToString(BigInteger[] array)
        {
            string result = "";
            for (int i = 0; i < array.Length; i++)
            {
                result += (char)array[i];
            }
            return result;
        }

        private BigInteger[] EncryptNumberArray(BigInteger[] array)
        {
            BigInteger[] result = new BigInteger[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = (array[i] ^ e) % n;
            }
            return result;
        }
    }
}
