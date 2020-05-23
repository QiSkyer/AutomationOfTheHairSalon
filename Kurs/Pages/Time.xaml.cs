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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Kurs
{
    /// <summary>
    /// Логика взаимодействия для Time.xaml
    /// </summary>
    /// 

    
        
    public partial class Time : Window
    {
        List<Button> buttons = new List<Button>();
       private string connectionString;
        public Time()
        {
            int i = 0;
            int top =  00;
            int left = 10;
            int beginRaspHour;
            int Rasp;
            int RaspMin;
            int endRasp;

            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
               
                string CmdString = "SELECT [idRaspisaniya],[idMaster],[TimeBegin],[TimeEnd],[Date] FROM [Kurs].[dbo].[RaspisanieMastera]";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(CmdString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                    beginRaspHour = Convert.ToInt32(reader[2]);
                    endRasp = Convert.ToInt32(reader[3]);
                reader.Close();
              
            }
            

            Rasp = beginRaspHour;
            RaspMin =0;
            for (i = 0; i<11; i++)
            {
                left =left + 40;
                if (RaspMin == 60)
                {
                    RaspMin = 0;
                    Rasp += 1;
                }
                RaspMin += 15;
                Button Buu = new Button();
                if (i == 0)
                {
                    Buu.Content = beginRaspHour + ":" + RaspMin ;
                }
                else
                {
                    Buu.Content =  Rasp+ ":" + RaspMin;
                }
                Canvas.SetTop(Buu,  top);
                Canvas.SetLeft(Buu,  left);
                buttons.Add(Buu);
                Canvas1.Children.Add(Buu);

            }
            
        }

      
    }
}
