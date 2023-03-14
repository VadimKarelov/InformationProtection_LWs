using Microsoft.Win32;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace LW6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _catalog;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenCompressedFile_Click(object sender, RoutedEventArgs e)
        {
            string path = _catalog + "\\output.txt";
            Process.Start("C:\\Windows\\System32\\notepad.exe", path);
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog f = new();
            
            if (f.ShowDialog() == true)
            {
                _catalog = f.FileName.Remove(f.FileName.LastIndexOf("\\"));   
                tb_inputFile.Text = f.FileName;

                string text;

                using (StreamReader reader = new(f.FileName))
                {
                    text = reader.ReadToEnd();
                }

                Encode(text);
            }
        }

        private void Encode(string text)
        {
            // init huffman
            HuffmanTree tree = new();
            tree.Build(WordsStatistic.ExampleFile);

            // in bits
            int sourceTetxSize = text.Length * 32;

            string ariphmetic = AriphmeticEncoding.Compression(text, out double t);

            BitArray bits = tree.Encode(ariphmetic);

            WriteFile(bits);

            string compressed = (bits.Count * 100 / sourceTetxSize).ToString();

            MessageBox.Show($"Сжатый файл составляет {compressed}% от исходного.");
        }

        private void WriteFile(BitArray bits)
        {
            tb_outputFile.Text = _catalog + "\\output.txt";

            string text = "";

            foreach (bool bit in bits)
            {
                text += bit ? "1" : "0";
            }

            using (StreamWriter writer = new(_catalog + "\\output.txt"))
            {
                writer.Write(text);
            }
        }
    }
}
