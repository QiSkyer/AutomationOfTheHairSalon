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
    /// Логика взаимодействия для AdminMaster.xaml
    /// </summary>
    public partial class AdminMaster : Page
    {
        List<int> Masterid = new List<int>();
        int sc2;
        private string connectionString;

        public AdminMaster()
        {
            InitializeComponent();
            GridLoad();
        }


        private void GridLoad()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string CmdString = $"SELECT [idMaster],[MasterSurName],[MasterName],[MasterMName] FROM[Kurs].[dbo].[ForReg2]";

                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(CmdString, connectionString);
                DataTable dt = new DataTable("Gr1");
                sda.Fill(dt);
                Gr1.ItemsSource = dt.DefaultView;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    Masterid.Add(i);
                    i++;
                }
                reader.Close();
            }
        }



    }
}
