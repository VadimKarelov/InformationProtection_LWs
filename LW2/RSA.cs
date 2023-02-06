using System;
using System.Numerics;

namespace LW2
{
    public class RSA
    {
        public BigInteger E => e;
        public BigInteger N => n;

        private BigInteger p;
        private BigInteger q;
        private BigInteger e;
        private BigInteger n;
        private BigInteger d;

        /// <summary>
        /// Return private key d
        /// </summary>
        public BigInteger CreateKeys(BigInteger p, BigInteger q)
        {
            n = p * q;
            d = GetD(p, q);
            e = GetE(p, q, d);
            return d;
        }

        public string Encrypt(string s)
        {
            return TransferNumberArrayToString(EncryptNumberArray(TransferStringToNumberArray(s)));
        }

        private BigInteger GetD(BigInteger p, BigInteger q)
        {
            BigInteger t = (p - 1) * (q - 1);
            Random rn = new();
            BigInteger res;
            do
            {
                res = rn.Next();
            } while (NOD(t, res) != 1);
            return res;
        }

        private BigInteger NOD(BigInteger x, BigInteger y)
        {
            while (x != y)
            {
                if (x > y)
                    x = x - y;
                else
                    y = y - x;
            }
            return x;
        }

        private BigInteger GetE(BigInteger p, BigInteger q, BigInteger d)
        {
            BigInteger t = (p - 1) * (q - 1);
            Random rn = new();
            BigInteger e;
            do
            {
                e = rn.Next();
            } while ((e * d) % t != 1);
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
