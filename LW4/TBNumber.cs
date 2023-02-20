using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
