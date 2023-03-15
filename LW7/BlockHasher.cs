using LW4;

namespace LW7
{
    public static class BlockHasher
    {
        private static readonly int HASH_SIZE = 16;

        static BlockHasher()
        {
            Festel.GenerateKeys();
        }

        public static string GetHash(string password)
        {
            // remove extra symbols by sum of their codes
            while (password.Length > HASH_SIZE)
            {
                string newPassword = "";

                for (int i = 0; i < password.Length; i += 2)
                {
                    if (i + 1 < password.Length)
                    {
                        newPassword += (char)((int)password[i] + (int)password[i + 1]);
                    }
                    else
                    {
                        newPassword += password[i];
                    }
                }

                password = newPassword;
            }

            // adding symbols
            for (int i = password.Length; i < HASH_SIZE; i++)
                password += (i % 10).ToString();

            return Festel.Encrypt(password);
        }
    }
}
