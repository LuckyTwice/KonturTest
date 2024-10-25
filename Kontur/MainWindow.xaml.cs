﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kontur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //отключаю кнопку с заполнением основных данных, чтобы нельзя было это выполнить до заполнения вспомогательных таблиц
        //при заполнении вспомогательных становится активной, при очищении таблиц не активной
        public MainWindow()
        {
            InitializeComponent();
            table.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Class1 class1 = new Class1();
            class1.Read();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Class1 class1 = new Class1();
            class1.Write();
            table.IsEnabled = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Class1 class1 = new Class1();
            class1.Remove();
            table.IsEnabled = false;
        }
    }
}