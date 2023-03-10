#pragma warning disable SYSLIB0011

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace LW6
{
    public static class WordsStatistic
    {
        public static Dictionary<char, Pair> Statistic
        {
            get
            {
                if (_isInit)
                {
                    return _stat;
                }
                else
                {
                    if (File.Exists(pathToStat))
                    {
                        LoadStatistic();
                        _isInit = true;
                        return _stat;
                    }
                    else
                    {
                        CountStatistic();
                        SaveStatistic();
                        _isInit = true;
                        return _stat;
                    }
                }
            }
        }

        public static string ExampleFile
        {
            get
            {
                string file;

                using (StreamReader sr = new StreamReader(pathToExample))
                {
                    file = sr.ReadToEnd();
                }

                return file.ToLower();
            }
        }

        private static bool _isInit = false;

        private static Dictionary<char, Pair> _stat;

        private static readonly string pathToExample = @"..\..\..\Resources\text_example.txt";
        private static readonly string pathToStat = @"..\..\..\Resources\statistics.dat";

        static WordsStatistic()
        {
            CountStatistic();
            _isInit = true;
        }

        private static void CountStatistic()
        {
            string file = ExampleFile;

            List<SymbStat> charNumber = new();

            // count number of each symbol
            foreach (char c in file)
            {
                bool f = charNumber.Where(x => x.Symbol == c).Count() > 0;
                
                if (f)
                {
                    SymbStat s = charNumber.First(x => x.Symbol == c);
                    s.Number++;
                }
                else
                {
                    charNumber.Add(new SymbStat(c, 0));
                }
            }

            int total = file.Length;

            // find chance for each symbol
            charNumber = charNumber.Select(x => new SymbStat(x.Symbol, x.Number / total)).OrderByDescending(x => x.Number).ToList();

            _stat = new();
            // add data to dictionary
            float previosBorder = 0; 
            foreach (SymbStat pair in charNumber)
            {
                // symbol, (left, right)
                _stat.Add(pair.Symbol, new Pair(previosBorder, previosBorder + pair.Number));
                previosBorder = previosBorder + pair.Number;
            }
        }

        private static void SaveStatistic()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(pathToStat, FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fs, _stat);
            }
        }

        private static void LoadStatistic()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(pathToStat, FileMode.OpenOrCreate))
            {
                _stat = (Dictionary<char, Pair>)binaryFormatter.Deserialize(fs);
            }
        }

        private class SymbStat
        {
            public char Symbol;
            public float Number;

            public SymbStat(char symbol, float number)
            {
                Symbol = symbol;
                Number = number;
            }
        }
    }

    [Serializable]
    public class Pair
    {
        public double L;
        public double R;

        public Pair(double l, double r)
        {
            L = l;
            R = r;
        }
    }
}

#pragma warning restore SYSLIB0011
