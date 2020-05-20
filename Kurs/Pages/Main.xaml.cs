﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kurs.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration0.xaml
    /// </summary>
    public partial class Registration0 : Page
    {
        public Registration0()
        {
            InitializeComponent();
        }
        //Кнопка записаться
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration1());
        }

        //Кнопка персонал
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }
    }
}
