using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog f = new();
            
            if (f.ShowDialog() == true)
            {
                _catalog = f.FileName.Remove(f.FileName.LastIndexOf("\\"));
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

            WriteFileAsync(bits);

            string compressed = (bits.Count * 100 / sourceTetxSize).ToString();

            MessageBox.Show($"Сжатый файл составляет {compressed}% от исходного.");
        }

        private async void WriteFileAsync(BitArray bits)
        {
            await Task.Run(() =>
            {
                string text = "";

                foreach (bool bit in bits)
                {
                    text += bit ? "1" : "0";
                }

                using (StreamWriter writer = new(_catalog + "output.txt"))
                {
                    writer.Write(text);
                }
            });
        }
    }
}
