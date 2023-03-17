using System.Windows;
using System.Windows.Controls;

namespace LW8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tb_gamma.Text = Gammirovanie.Gamma;
        }

        private void Encrypt()
        {
            tb_encrypted.Text = Gammirovanie.Encrypt(tb_input.Text);
        }

        private void Decrypt()
        {
            tb_decrypted.Text = Gammirovanie.Decrypt(tb_encrypted.Text);
        }

        private void ChangeGamma()
        {
            if (tb_gamma.Text.Length > 0)
            {
                Gammirovanie.Gamma = tb_gamma.Text;
                Encrypt(); // will cause event to decrypt
            }
        }

        private void TextBoxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            Encrypt();
        }

        private void TextBoxGamma_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeGamma();
        }

        private void TextBoxEncrypted_TextChanged(object sender, TextChangedEventArgs e)
        {
            Decrypt();
        }
    }
}
