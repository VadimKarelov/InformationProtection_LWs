using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

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
            if (GetPQ(out BigInteger p, out BigInteger q))
            {
                tb_out.Text = rsa.Encrypt(tb_in.Text);
            }
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
            BigInteger p, q;
            do
            {
                p = Math.Abs(rn.Next(100000, 999999) * (int)Math.Pow(10, 6) + rn.Next(100000, 999999));
            } while (!IsSimple(p));
            do
            {
                q = Math.Abs(rn.Next(100000, 999999) * (int)Math.Pow(10, 6) + rn.Next(100000, 999999));
            } while (!IsSimple(q));
            tb_q.Text = q.ToString();
            tb_p.Text = p.ToString();
            rsa.CreateKeys(p, q);
            SetKeys();
        }

        private bool IsSimple(BigInteger value)
        {
            for (BigInteger i = 2; i < value / 2; i++)
            {
                if (value % i == 0)
                    return false;
            }
            return true;
        }
    }
}
