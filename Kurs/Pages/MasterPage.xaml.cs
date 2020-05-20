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
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT [idPriem],[Surname],[Name],[MiddleName],[ServiceName] FROM[Kurs].[dbo].[ForMasters]";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(CmdString, connectionString);
                DataTable dt = new DataTable("Emplo");
                sda.Fill(dt);
                Dg1.ItemsSource = dt.DefaultView;


            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
