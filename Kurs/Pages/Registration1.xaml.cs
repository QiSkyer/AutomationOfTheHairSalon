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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration1 : Page
    {
        List<string> Serviceid = new List<string>();
        List<string> Service = new List<string>();
        List<string> ServiceCost = new List<string>();
        private string connectionString;

        public Registration1()
        {
            InitializeComponent();
            LoadGrid();
        }


        private void LoadGrid()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT [idService],[ServiceName],[ServiceLength],[ServiceCost] FROM [Kurs].[dbo].[Service]";

                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(CmdString, connectionString);
                DataTable dt = new DataTable("Gr1");
                sda.Fill(dt);
                Gr1.ItemsSource = dt.DefaultView;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Serviceid.Add(reader[0].ToString());
                    Service.Add(reader[1].ToString());
                    ServiceCost.Add(reader[3].ToString());
                } 
                reader.Close();
            }
        }

        //Проверка существующей записи
        private void Clientid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string CmdString = $"SELECT [idClient],[Surname],[Name],[MiddleName],[Phone] FROM [Kurs].[dbo].[Clients] Where Surname = '{FamilyBox.Text}' AND Name = '{NameBox.Text}' AND MiddleName= '{OtchestvoBox.Text}' AND Phone = '{PhoneBox.Text}'";
              
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(CmdString, connectionString);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Reg1Data.Clientid = Convert.ToInt32(reader[0]);
                    return;
                }
                reader.Close();
               // MessageBox.Show(Reg1Data.Clientid.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int sc2 = Gr1.SelectedIndex;
            Reg2Data.Time = "";
            if (NameBox.Text == "")
            {
                MessageBox.Show("Вы не ввели Имя");
            }
            else
            {
                if (sc2 == -1)
                {
                    MessageBox.Show("Вы не выбрали услугу");
                }
                else
                {
                    Clientid();
                    Reg1Data.ClientName = NameBox.Text;
                    Reg1Data.ClientFamily = FamilyBox.Text;
                    Reg1Data.ClientOtchstvo = OtchestvoBox.Text;
                    Reg1Data.ClientPhone = PhoneBox.Text;
                    Reg1Data.ServiceId = sc2.ToString();
                    Reg1Data.ServiceCost = ServiceCost[sc2];
                    Reg1Data.Service = Service[sc2];
                    NavigationService.Navigate(new Registration2());
                }
            }
            
        }
    }
}
