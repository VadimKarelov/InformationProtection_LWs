using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace LW3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DiffiHelman.GenerateKeys();
            SetParameters();

            Task();
        }

        private void SetParameters()
        {
            tb_p.Text = DiffiHelman.p.ToString();
            tb_g.Text = DiffiHelman.g.ToString();
            tb_a.Text = DiffiHelman.a.ToString();
            tb_b.Text = DiffiHelman.b.ToString();
            tb_A.Text = DiffiHelman.A.ToString();
            tb_B.Text = DiffiHelman.B.ToString();
            tb_Ka.Text = DiffiHelman.Ka.ToString();
            tb_Kb.Text = DiffiHelman.Kb.ToString();
        }

        private void Encrypt_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb_out.Text = SimpleEncrypter.Encrypt(tb_in.Text, DiffiHelman.K);
        }

        private void Task()
        {
            WriteTextToFile(SimpleEncrypter.Encrypt(ReadFile(), DiffiHelman.K));
        }

        private string ReadFile()
        {
            string text;
            using (StreamReader stream = new("input.txt", false))
            {
                text = stream.ReadToEnd();
            }
            tb_in.Text = text;
            return text;
        }

        private void WriteTextToFile(string text)
        {
            using (StreamWriter stream = new("output.txt", false))
            {
                stream.Write(text);
            }
        }
    }
}
