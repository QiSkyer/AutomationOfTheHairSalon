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
    /// Логика взаимодействия для Final.xaml
    /// </summary>
    public partial class Final : Page
    {
        private string connectionString;

        public Final()
        {
            InitializeComponent();

            _FIO.Text = Reg1Data.ClientFamily + " " + Reg1Data.ClientName + " " + Reg1Data.ClientOtchstvo;
            _Master.Text = Reg2Data.master;
            _Service.Text = Reg1Data.Service;
        }

        private void Reg()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string CmdString = " DECLARE @a int  set @a = (Select COUNT([idClient]) FROM[Kurs].[dbo].[Clients])" +
                    "INSERT INTO [dbo].[Clients] ([idClient],[Surname] ,[Name]   ,[MiddleName] ,[Phone])" +
                    $"  VALUES   ( @a+1, '{Reg1Data.ClientFamily}'  , '{Reg1Data.ClientName}' , '{Reg1Data.ClientOtchstvo } , '{Reg1Data.ClientPhone}') ";

                SqlCommand cmd = new SqlCommand(CmdString, con);

                cmd.ExecuteNonQuery();
            }
        }

        private void ExitBu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration0());
            NavigationService.RemoveBackEntry();
        }
    }
}
