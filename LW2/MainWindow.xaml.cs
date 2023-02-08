using System;
using System.IO;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace LW2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RSA rsa = new();

        public MainWindow()
        {
            InitializeComponent();
            GeneratePQ();
        }

        private void Encrypt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Encrypt();
        }

        private void Encrypt()
        {
            if (GetPQ(out BigInteger p, out BigInteger q))
            {
                tb_out.Text = rsa.Encrypt(tb_in.Text);
            }
        }

        private void ResetKeys_Click(object sender, RoutedEventArgs e)
        {
            GeneratePQ();
            Encrypt();
        }

        private void SetKeys()
        {
            tb_q.Text = rsa.q.ToString();
            tb_p.Text = rsa.p.ToString();
            tb_e.Text = rsa.e.ToString();
            tb_n.Text = rsa.n.ToString();
            tb_d.Text = rsa.d.ToString();
        }

        private bool GetPQ(out BigInteger p, out BigInteger q)
        {
            p = 0;
            q = 0;
            bool f = BigInteger.TryParse(tb_p.Text, out p);
            f = f && BigInteger.TryParse(tb_q.Text, out q);
            return f;
        }

        private void GeneratePQ()
        {
            Random rn = new();
            int rows = NumberOfRowsInFile();

            BigInteger p = GetSimpleFromFile(rn.Next(rows));
            BigInteger q = GetSimpleFromFile(rn.Next(rows));

            tb_q.Text = q.ToString();
            tb_p.Text = p.ToString();

            rsa.CreateKeys(p, q);
            SetKeys();
        }

        private BigInteger GetSimpleFromFile(int rowNumber)
        {
            string path = "../../../Resources/simple12.txt";
            BigInteger res;
            using (StreamReader stream = new(path))
            {
                for (int i = 0; i < rowNumber; i++)
                {
                    stream.ReadLine();
                }
                res = BigInteger.Parse(stream.ReadLine());
            }
            return res;
        }

        private int NumberOfRowsInFile()
        {
            string path = "../../../Resources/simple12.txt";
            using (StreamReader stream = new(path))
            {
                return stream.ReadToEnd().Split("\n").Length;
            }
        }
    }
}
