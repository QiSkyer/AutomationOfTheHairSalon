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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Kurs.Pages
{
    /// <summary>
    /// Логика взаимодействия для MasterPage.xaml
    /// </summary>
    public partial class MasterPage : Page
    {
        private string connectionString;
        public MasterPage()
        {
            InitializeComponent();
            MasterPageLo();
            MasterLabel.Content = DataStore.MasterFam + " " + DataStore.MasterName + " " + DataStore.MasterOtch;
        }

        private void MasterPageLo()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string CmdString = $"SELECT [idPriem],[Surname],[Name],[MiddleName],[ServiceName],[TimePriem] FROM [Kurs].[dbo].[ForMasters] WHERE [loginMaster] = '{DataStore.Userlogin}' AND [Date] = '{DateTime.Now.ToString("yyyy-MM-dd")}'";

                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(CmdString, connectionString);
                DataTable dt = new DataTable("Dg1");
                sda.Fill(dt);
                Dg1.ItemsSource = dt.DefaultView;
            }
        }
      

        private void ExitBu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }

        private void ResBu_Click(object sender, RoutedEventArgs e)
        {
            Dg1.ItemsSource = null;
            MasterPageLo();
        }
    }
}
