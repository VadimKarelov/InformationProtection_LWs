using System.Windows;
using System.Windows.Controls;

namespace LW7
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

        private void Hash_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb_hash.Text = BlockHasher.GetHash(tb_password.Text);
        }
    }
}
