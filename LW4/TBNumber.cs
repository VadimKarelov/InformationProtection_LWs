using System;
using System.Linq;

namespace LW4
{
    /// <summary>
    /// Represent 2 bytes number (16 bit)
    /// </summary>
    internal class TBNumber
    {
        public Int16 Value
        {
            get
            {
                Int16 pow2 = 1;
                Int16 res = 0;
                for (Int16 i = 15; i >= 0; i--)
                {
                    res += _value[i] ? pow2 : (Int16)0;
                    pow2 *= 2;
                }
                return res;
            }
            set
            {                
                _value = Convert.ToString(value, 2).ToArray().Select(x => x == '1').ToArray();
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
            return "0x" + Convert.ToString(Value, 16);
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
    }
}
