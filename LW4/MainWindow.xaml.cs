using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LW4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Festel.GenerateKeys();

            tb_inputEncrypt.Text = "Some sentense for example. Некое предложение для примера.";
        }

        private void Encrypt_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowEncryptionHistory(Festel.Encrypt(tb_inputEncrypt.Text));
        }

        private void ShowEncryptionHistory(string encryptedText)
        {
            // headers
            string res = $"Раунд/Ключ/Число\n";

            for (int i = 0; i < Festel.Keys.Count; i++)
            {
                res += $"{i + 1} / {Festel.Keys[i].Hex()} / {CombineNumber(Festel.History[i])}\n";
            }

            res += $"Результат: {CombineNumber(Festel.LastValue)}\n";

            tb_encrypt.Text = res;

            tb_outputEncrypt.Text = encryptedText;
        }

        private string CombineNumber(List<TBNumber> numbers)
        {
            string res = "";

            foreach (TBNumber number in numbers)
            {
                res += number.Hex() + " ";
            }

            return res;
        }
    }
}
