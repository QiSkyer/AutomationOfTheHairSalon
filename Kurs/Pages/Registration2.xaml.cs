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
    /// Логика взаимодействия для Registration2.xaml
    /// </summary>
    public partial class Registration2 : Page
    {
        List<string> Masterid = new List<string>();
        List<string> Master = new List<string>();
        int sc2;

        private string connectionString;

        public Registration2()
        {
            InitializeComponent();
            GridLoad();
        }

        private void GridLoad()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string CmdString = $"SELECT [idMaster],[MasterSurName],[MasterName],[MasterMName] FROM[Kurs].[dbo].[ForReg2] where [idService] = {Reg1Data.ServiceId} ";

                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(CmdString, connectionString);
                DataTable dt = new DataTable("Gr1");
                sda.Fill(dt);
                Gr1.ItemsSource = dt.DefaultView;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int i=0;
                while (reader.Read())
                {
                   Masterid.Add(reader[0].ToString());
                    Master.Add(reader[1].ToString());
                    Master[i] += " " + reader[2].ToString() + " " + reader[3].ToString();
                    i++;
                }
                reader.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ( Reg2Data.Time == "" || Reg2Data.Date == "" || sc2 == -1)
            {
                MessageBox.Show("Вы не выбрали Мастера/Дату/Время");
            }
            else
            {
                NavigationService.Navigate(new Final());
            }
        }

        private void Time_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.Now > Kalendar.SelectedDate)
            {
                Kalendar.SelectedDate = DateTime.Now;
                Reg2Data.Date = Kalendar.SelectedDate.ToString();
            }
            else
            {
                Reg2Data.Date = Kalendar.SelectedDate.ToString();
            }

            sc2 = Gr1.SelectedIndex;

            if (Reg2Data.Date == "" || sc2 == -1)
            {
                MessageBox.Show("Выберите дату и мастера");
            }
            else
            {
                   
                Reg2Data.masterid = Masterid[sc2].ToString();
                Reg2Data.master = Master[sc2];
                Time T = new Time();
                T.Show();
            }
        }

    }
}
