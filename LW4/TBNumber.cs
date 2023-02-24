using System;
using System.Linq;

namespace LW4
{
    /// <summary>
    /// Represent 2 bytes number (16 bit)
    /// </summary>
    public class TBNumber
    {
        public Int16 Value
        {
            get
            {
                // less zero
                if (_value[0])
                {
                    // minus 1
                    TBNumber t = new(Convert.ToInt16(this.Binary(), 2) - 1);
                    
                    // change 0 to 1 and 1 to 0
                    for (int i = 0; i < 16; i++)
                    {
                        t._value[i] = !t._value[i];
                    }

                    return Convert.ToInt16(new TBNumber(t._value).Value * -1);
                }
                else
                {
                    return Convert.ToInt16(this.Binary(), 2);
                }
            }
            set
            {
                bool[] t = Convert.ToString(value, 2).Select(x => x == '1').ToArray();

                // copy elements to array from the end
                for (int i = t.Length - 1, j = 15; i >= 0; i--, j--)
                {
                    _value[j] = t[i];
                }
            }
        }

        public TBNumber LeftShift
        {
            get
            {
                bool[] newNumber = new bool[16];
                Array.Copy(_value, newNumber, _value.Length);

                bool first = newNumber[0];
                for (int i = 0; i < newNumber.Length - 1; i++)
                {
                    newNumber[i] = newNumber[i + 1];
                }
                newNumber[newNumber.Length - 1] = first;

                return new TBNumber(newNumber);
            }
        }

        /// <summary>
        /// Standart order of bits - left most valuable
        /// </summary>
        private bool[] _value = new bool[16];

        #region constructor
        public TBNumber()
        {
        }

        public TBNumber(Int16 value)
        {
            Value = value;
        }

        public TBNumber(int value)
        {
            Value = Convert.ToInt16(value);
        }

        public TBNumber(bool[] array)
        {
            if (array.Length != 16)
                throw new ArgumentException("Wrong array size - must be 16.");

            _value = array;
        }
        #endregion

        public override string ToString()
        {
            return Value.ToString();
        }

        public string Hex()
        {
            string res = "";

            // 4 symbols in hexidecimal notation
            for (int i = 0; i < 16; i += 4)
            {
                res += HexChar(_value[i], _value[i + 1], _value[i + 2], _value[i + 3]);
            }

            return "0x" + res;
        }

        public string Binary()
        {
            string res = "";

            // 4 symbols in hexidecimal notation
            for (int i = 0; i < 16; i++)
            {
                res += _value[i] ? "1" : "0";
            }

            return res;
        }

        public TBNumber XOR(TBNumber value)
        {
            bool[] newNumber = new bool[16];

            for (int i = 0; i < 16; i++)
            {
                newNumber[i] = this._value[i] ^ value._value[i];
            }

            return new TBNumber(newNumber);
        }

        public TBNumber Clone()
        {
            return new TBNumber(this.Value);
        }

        private char HexChar(bool x1, bool x2, bool x3, bool x4)
        {
            int num = x4 ? 1 : 0;
            num += x3 ? 2 : 0;
            num += x2 ? 4 : 0;
            num += x1 ? 8 : 0;

            if (num < 10)
            {
                return num.ToString()[0];
            }
            else
            {
                switch (num)
                {
                    case 10: return 'A';
                    case 11: return 'B';
                    case 12: return 'C';
                    case 13: return 'D';
                    case 14: return 'E';
                    case 15: return 'F';
                    default: throw new Exception();
                }
            }
        }
    }
}
