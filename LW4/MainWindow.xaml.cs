using System;
using System.Collections.Generic;
using System.Linq;
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

        private void Decrypt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_inputDecrypt.Text.Length % 2 == 0 && !String.IsNullOrEmpty(tb_inputDecrypt.Text))
                ShowDecryption(Festel.Decrypt(tb_inputDecrypt.Text));
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

        private void ShowDecryption(string decryptedText)
        {
            // headers
            string res = $"Раунд/Ключ/Число\n";

            List<TBNumber> backKeys = CloneList(Festel.Keys);
            backKeys.Reverse();

            for (int i = 0; i < Festel.Keys.Count; i++)
            {
                res += $"{i + 1} / {backKeys[i].Hex()} / {CombineNumber(Festel.History[i])}\n";
            }

            res += $"Результат: {CombineNumber(Festel.LastValue)}\n";

            tb_decrypt.Text = res;

            tb_outputDecrypt.Text = decryptedText;
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

        private static List<TBNumber> CloneList(List<TBNumber> list)
        {
            return list.Select(x => x.Clone()).ToList();
        }
    }
}
