using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LW1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateTBs();
            SetRandomTable();
        }

        private void CreateTBs()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    TextBox tb = new()
                    {
                        Tag = i.ToString() + j.ToString(),
                    };

                    Grid.SetRow(tb, i);
                    Grid.SetColumn(tb, j);

                    grid_table.Children.Add(tb);
                }
            }
        }

        private bool MakeTable(out char[,] outTable)
        {
            outTable = new char[4, 7];
            foreach (var item in grid_table.Children)
            {
                if (item is TextBox tb)
                {
                    if (tb.Text.Length == 1)
                    {
                        if (int.TryParse(tb.Tag.ToString()[0].ToString(), out int i))
                        {
                            if (int.TryParse(tb.Tag.ToString()[1].ToString(), out int j))
                            {
                                outTable[i, j] = tb.Text[0];
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void Encrypt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MakeTable(out char[,] table) && Cipher.PolybianSquare(table, tb_in.Text, out string outLine))
            {
                tb_out.Text = outLine;
                tb_out.Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                tb_out.Background = new SolidColorBrush(Colors.LightCoral);
            }
        }

        private void SetRandomTable()
        {
            List<char> symbols = "qwertyuiopasdfghjklzxcvbnm".ToList();
            Random rn = new();

            foreach (var item in grid_table.Children)
            {
                if (item is TextBox tb && symbols.Any())
                {
                    int ind = rn.Next(0, symbols.Count);
                    tb.Text = symbols[ind].ToString();
                    symbols.RemoveAt(ind);
                }
            }
        }
    }
}
