using System.Linq;

namespace LW8
{
    public static class Gammirovanie
    {
        public static string Gamma
        {
            get
            {
                return new string(_gamma.Select(x => ALPHABET[x]).ToArray());
            }
            set
            {
                _gamma = value.ToCharArray().Select(x => ALPHABET.IndexOf(x)).ToArray();
            }
        }

        private static readonly string ALPHABET = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNMйцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ1234567890!\"№;%:?*()@#$^&-=_+{}[]\\|/<>.,'`ёЁ~ ";

        private static int[] _gamma;

        static Gammirovanie()
        {
            Gamma = "gammaSentense";
        }

        public static string Encrypt(string text)
        {
            string result = "";

            for (int i = 0; i < text.Length; i++)
            {
                // Ci = (Pi + Ki) % N
                result += ALPHABET[(ALPHABET.IndexOf(text[i]) + _gamma[i % _gamma.Length]) % ALPHABET.Length];
            }

            return result;
        }

        public static string Decrypt(string text)
        {
            string result = "";

            for (int i = 0; i < text.Length; i++)
            {
                // Pi = (Ci + N - Ki) % N
                result += ALPHABET[(ALPHABET.IndexOf(text[i]) + ALPHABET.Length - _gamma[i % _gamma.Length]) % ALPHABET.Length];
            }

            return result;
        }
    }
}
