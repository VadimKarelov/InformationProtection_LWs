#pragma warning disable SYSLIB0011

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace LW6
{
    public static class WordsStatistic
    {
        public static Dictionary<char, PointF> Statistic
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

        private static bool _isInit = false;

        private static Dictionary<char, PointF> _stat;

        private static readonly string pathToExample = @"..\..\Resources\text_example.txt";
        private static readonly string pathToStat = @"..\..\Resources\statistics.dat";

        private static void CountStatistic()
        {
            string file;

            using (StreamReader sr = new StreamReader(pathToExample))
            {
                file = sr.ReadToEnd();
            }

            List<SymbStat> charNumber = new();

            // count number of each symbol
            foreach (char c in file)
            {
                SymbStat s = charNumber.First(x => x.Symbol == c);
                if (s is not null)
                {
                    s.Number++;
                }
                else
                {
                    charNumber.Add(new SymbStat(c, 0));
                }
            }

            int total = file.Length;

            // find chance for each symbol
            charNumber = charNumber.Select(x => new SymbStat(x.Symbol, x.Number / total)).OrderBy(x => x.Number).ToList();

            _stat = new();
            // add data to dictionary
            float previosBorder = 0; 
            foreach (SymbStat pair in charNumber)
            {
                // symbol, (left, right)
                _stat.Add(pair.Symbol, new PointF(previosBorder, previosBorder + pair.Number));
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
                _stat = (Dictionary<char, PointF>)binaryFormatter.Deserialize(fs);
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
}

#pragma warning restore SYSLIB0011
