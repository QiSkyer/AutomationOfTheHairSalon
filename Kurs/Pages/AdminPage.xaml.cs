using System;
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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    /// 
    
        


    public partial class AdminPage : Page
    {

        public AdminPage()
        {
            InitializeComponent();
            

             
            
        }

        private void MasterBu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KlientBu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ServiceBu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitBu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }
    }
}
