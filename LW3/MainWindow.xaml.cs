using System.Configuration;
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
    }
}
