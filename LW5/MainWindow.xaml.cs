using LW5.BackgroundModules;
using System.Windows;
using System.Windows.Controls;

namespace LW5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Convert_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb_bits.Text = Hamming.AddControlBits(Hamming.ConvertToBits(tb_input.Text));
        }

        private void Check_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool f = Hamming.IsMessageNotCorrupted(tb_bits.Text, out int corruptedBit);
            tb_checkResult.Text = f ? "Сообщение не повреждено" : $"Поврежденнй бит: {corruptedBit}";
            tb_correctedText.Text = Hamming.CorrectMessageAndConvertToString(tb_bits.Text, corruptedBit);
        }
    }
}
