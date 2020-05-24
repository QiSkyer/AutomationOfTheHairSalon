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
        List<int> bans = new List<int>();
        List<Button> buttons = new List<Button>();
        List<string[]> CloseTime = new List<string[]>();
        List<int> ServiceLeng = new List<int>();
        int ServiceLengReg = 0;
        private string connectionString;

        public Time()
        {

            int i = 0;
            int ii = 0;
            int Top =  19;
            int left = 14;
            int beginRaspHour;
            int Rasp;
            int RaspMin;
            int endRasp;
            


            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string CmdString = $"SELECT [idRaspisaniya],[idMaster],[TimeBegin],[TimeEnd],[Date] FROM [Kurs].[dbo].[RaspisanieMastera] where idMaster = '{Reg2Data.masterid}'";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(CmdString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                beginRaspHour = Convert.ToInt32(reader[2]);
                endRasp = Convert.ToInt32(reader[3]);
                reader.Close();

                string CmdString1 = $"SELECT [idMaster],[idService],[TimePriem],[Date],[ServiceLength] FROM [Kurs].[dbo].[ForTimes] Where idmaster = '{Reg2Data.masterid}' AND [Date] = '{Reg2Data.Date}'";
                SqlConnection connection1 = new SqlConnection(connectionString);
                SqlCommand command1 = new SqlCommand(CmdString1, connection);
                SqlDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    CloseTime.Add((reader1[2]).ToString().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries));
                    ServiceLeng.Add(Convert.ToInt32(reader1[4]));
                }
                reader1.Close();

                string CmdString2 = $"SELECT [ServiceLength] FROM [Kurs].[dbo].[Service] Where  idService= {Reg1Data.ServiceId} ";
                SqlConnection connection2 = new SqlConnection(connectionString);
                SqlCommand command2 = new SqlCommand(CmdString2, connection);
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    ServiceLengReg = Convert.ToInt32(reader2[0]);
                  //  MessageBox.Show(ServiceLengReg.ToString());
                }
                reader2.Close();

              //  MessageBox.Show(_io.ToString());

            }



            //генерация кнопок времени

            Rasp = beginRaspHour;
            RaspMin =0;
            i = 0;

           

                while (Rasp < endRasp )
            {

                
                
                Button Buu = new Button();
                Buu.Tag = i;

                //MessageBox.Show(CloseTime.Count.ToString());
                for (ii=0; ii< CloseTime.Count; ii++)
                {
                    if (Rasp == Convert.ToInt32(CloseTime[ii][0]) && RaspMin == Convert.ToInt32(CloseTime[ii][1]))
                    {
                       
                                Buu.Background = Brushes.Gray;
                    }
                    else
                    {
                        Buu.Click += new RoutedEventHandler(Buu_Click);
                    }

                }
                if (CloseTime.Count == 0)
                {
                    Buu.Click += new RoutedEventHandler(Buu_Click);
                }

                if (i == 0)
                {
                    Buu.Content = beginRaspHour + ":" + RaspMin ;
                }
                else
                {
                    Buu.Content =  Rasp+ ":" + RaspMin;
                }

                if (left >= 710)
                {
                    Top += 23;
                    left = 54;
                }
                else
                {
                    left = left + 40;
                }
                RaspMin += ServiceLengReg;
                if (RaspMin >= 60)
                {
                    RaspMin = RaspMin % 60;
                    Rasp += 1;
                }

                Canvas.SetTop(Buu,  Top);
                Canvas.SetLeft(Buu,  left);
                buttons.Add(Buu);
                Canvas1.Children.Add(Buu);
               
                i++;
            }
        }

        private void Buu_Click(object sender, EventArgs e)
        {
           string sr = ((Button)sender).Content.ToString();
            Reg2Data.Time = sr;
            Result.Content = sr;
          
            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
