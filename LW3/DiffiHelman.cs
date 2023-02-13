using System;
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
            _g = GetRandomSimpleFromFile();
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
    }
}
