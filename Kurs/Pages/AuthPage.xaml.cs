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
using System.Configuration;
using System.Data.SqlClient;

namespace Kurs.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    /// 

    public partial class AuthPage : Page
    {
        private string connectionString;
      
        public AuthPage()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                //   MessageBox.Show ($"Сервер: {connection.DataSource}; База данных: {connection.State.ToString()};Состояние: подключено.");
            }

            catch (SqlException)

            {

                MessageBox.Show("Подключение не выполнено!");

            }
        }



        private void Login(object sender, RoutedEventArgs e)
        {

            

            string query = "SELECT [MasterSurName],[MasterName],[MasterMName], loginMaster, PasswordMaster FROM dbo.Masters Where loginMaster = @login AND PasswordMaster = @password";


            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@login", LoginBox.Text);
            DataStore.Userlogin = LoginBox.Text;
            command.Parameters.AddWithValue("@password", PassBox.Password);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();


            if (LoginBox.Text == "admin" && PassBox.Password == "admin")
            {
                NavigationService.Navigate(new AdminPage());
            }
            else
            {
                while (reader.Read())
                {
                    DataStore.MasterFam = reader[0].ToString();
                    DataStore.MasterName = reader[1].ToString();
                    DataStore.MasterOtch = reader[2].ToString();
                    
                    NavigationService.Navigate(new MasterPage());
                     return;

                }
                MessageBox.Show("Ошибка в логине или пароле");
            }
            
            

        }
    }


}
