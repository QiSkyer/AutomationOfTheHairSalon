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
            _Phone.Text = Reg1Data.ClientPhone;
            _Date.Text = Reg2Data.Date;
            _Cost.Text = Reg1Data.ServiceCost;
            _Time.Text = Reg2Data.Time;
            Reg();
        }

        private void Reg()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                if (Reg1Data.Clientid < 0)
                {
                    string CmdString00 = "Select COUNT([idClient]) As Kol FROM [Kurs].[dbo].[Clients]";
                    SqlCommand cmd00 = new SqlCommand(CmdString00, con);
                    SqlDataReader reader00 = cmd00.ExecuteReader();

                    reader00.Read();
                    int Kol0 = Convert.ToInt32(reader00[0]);
                    reader00.Close();

                    string CmdString = "INSERT INTO [dbo].[Clients] ([idClient],[Surname] ,[Name]   ,[MiddleName] ,[Phone])" +
                        " VALUES ( @a, @Family , @SurName , @MiddleName , @Phone) ";

                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.Parameters.AddWithValue("@a", Kol0 + 1);
                    cmd.Parameters.AddWithValue("@Family", Reg1Data.ClientFamily);
                    cmd.Parameters.AddWithValue("@SurName", Reg1Data.ClientName);
                    cmd.Parameters.AddWithValue("@MiddleName", Reg1Data.ClientOtchstvo);
                    cmd.Parameters.AddWithValue("@Phone", Reg1Data.ClientPhone);
                    cmd.ExecuteNonQuery();

                    string CmdString2 = "SELECT [idClient],[Surname] ,[Name] ,[MiddleName] ,[Phone] FROM [Kurs].[dbo].[Clients]";

                    SqlCommand cmd2 = new SqlCommand(CmdString2, con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(CmdString2, connectionString);
                    //con.Open();
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        Reg1Data.Clientid = Convert.ToInt32(reader2[0]);
                    }
                    reader2.Close();
                }

                string CmdString0 = " SELECT COUNT([idPriem]) As Kol FROM [Kurs].[dbo].[Priem]";
                SqlCommand cmd0 = new SqlCommand(CmdString0, con);
                SqlDataReader reader0 = cmd0.ExecuteReader();

                reader0.Read();
               int Kol = Convert.ToInt32(reader0[0]);
                reader0.Close();

                string CmdString1 = " INSERT INTO[dbo].[Priem] ([idPriem],[idMaster],[idService],[idClient],[idRaspisaniya],[TimePriem], [DatePriem])" +
                  $" VALUES ( @a , @masterid ,@ServiceId, @Clientid, 0 , @Time, '{Reg2Data.Date}')";
                SqlCommand cmd1 = new SqlCommand(CmdString1, con);
                cmd1.Parameters.AddWithValue("@a", Kol + 1);
                cmd1.Parameters.AddWithValue("@masterid", Reg2Data.masterid);
                cmd1.Parameters.AddWithValue("@ServiceId", Reg1Data.ServiceId);
                cmd1.Parameters.AddWithValue("@Clientid", Reg1Data.Clientid);
                cmd1.Parameters.AddWithValue("@Time", Reg2Data.Time);
               
                cmd1.ExecuteNonQuery();

            }
        }

        private void ExitBu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration0());
            NavigationService.RemoveBackEntry();
        }

        private void ResBu_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
