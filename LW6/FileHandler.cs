using System.IO;

namespace LW6
{
    public static class FileHandler
    {
        private static readonly string pathInput = @"../../../Resources/input.txt";
        private static readonly string pathOutput = @"../../../Resources/output.txt";

        public static string ReadInput()
        {
            CreateInputFileIfNotExist();

            using (StreamReader reader = new StreamReader(pathInput))
            {
                return reader.ReadToEnd();
            }
        }

        public static void WriteOutput(string text)
        {
            using (StreamWriter writer = new StreamWriter(pathOutput))
            {
                writer.WriteLine(text);
            }
        }

        private static void CreateInputFileIfNotExist()
        {
            if (!File.Exists(pathInput))
            {
                using (StreamWriter writer = new StreamWriter(pathInput))
                {
                    writer.WriteLine("Пример какого-то сообщения для кодирования.");
                }
            }
        }
    }
}
