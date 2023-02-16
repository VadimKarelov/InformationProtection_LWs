using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace LW3
{
    public static class DiffiHelman
    {
        #region Variables
        public static BigInteger p => _p;
        public static BigInteger g => _g;

        public static BigInteger a => _a;
        public static BigInteger b => _b;

        public static BigInteger A => _A;
        public static BigInteger B => _B;

        public static BigInteger K => _K;
        public static BigInteger Ka => _Ka;
        public static BigInteger Kb => _Kb;

        private static BigInteger _p;
        private static BigInteger _g;

        private static BigInteger _a;
        private static BigInteger _b;

        private static BigInteger _A;
        private static BigInteger _B;

        private static BigInteger _K;
        private static BigInteger _Ka;
        private static BigInteger _Kb;

        private static Random _rn = new();
        #endregion

        public static void GenerateKeys()
        {
            _p = GetRandomSimpleFromFile();
            _g = Generator(_p);
            _a = GetRandomBigInteger();
            _b = GetRandomBigInteger();
            _A = BigInteger.ModPow(_g, _a, _p);
            _B = BigInteger.ModPow(_g, _b, _p);
            _Ka = BigInteger.ModPow(_B, _a, _p);
            _Kb = BigInteger.ModPow(_A, _b, _p);
            _K = _Ka;
                
            if (_Ka != _Kb)
                throw new Exception("Keys not equal");
        }

        private static BigInteger GetRandomSimpleFromFile()
        {
            string path = "../../../Resources/simple12.txt";
            BigInteger res;
            int linesNumber;
            using (StreamReader stream = new(path))
            {
                linesNumber = stream.ReadToEnd().Split("\n").Length;
            }
            int rowNumber = _rn.Next(linesNumber);
            using (StreamReader stream = new(path))
            {
                for (int i = 0; i < rowNumber; i++)
                {
                    stream.ReadLine();
                }
                res = BigInteger.Parse(stream.ReadLine());
            }
            return res;
        }

        private static BigInteger GetRandomBigInteger()
        {
            return _rn.Next(1000000) * (BigInteger)1000000 + _rn.Next(1000000);
        }

        private static BigInteger Generator(BigInteger p)
        {
            List<BigInteger> fact = new();
            BigInteger phi = p - 1, n = phi;
            for (int i = 2; i * i <= n; ++i)
            {
                if (n % i == 0)
                {
                    fact.Add(i);
                    while (n % i == 0)
                        n /= i;
                }
            }
            if (n > 1)
                fact.Add(n);
            for (int res = 2; res <= p; ++res)
            {
                bool ok = true;
                for (int i = 0; i < fact.Count && ok; ++i)
                    ok &= BigInteger.ModPow(res, phi / fact[i], p) != 1;
                if (ok) return res;
            }
            return -1;
        }
    }
}
