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
using MySql.Data.MySqlClient;

namespace AAA_HAG_Aplication
{
    /// <summary>
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Window
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            Session.conn.Open();
            string LogInSQL = "SELECT Email FROM Accounts WHERE Email = @Email AND Password = SHA(@Password)";
            MySqlCommand cmd = new MySqlCommand(LogInSQL, Session.conn);
            cmd.Parameters.AddWithValue("@Email", txtbEmail.Text);    //These parameters prevent SQL injections
            cmd.Parameters.AddWithValue("@Password", pswPassword.Password);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                AccountEmail.address = rdr.GetString(0);
                MessageBox.Show("Log In Successful.");
                txtbEmail.Text = "";               
                pswPassword.Password = "";
                Session.conn.Close();
                return;
            }
            Session.conn.Close();    
            MessageBox.Show("Log In Unsuccessful");
            txtbEmail.Text = "";
            pswPassword.Password = "";
        }

        private void btnBackArrow_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage profilepage = new ProfilePage();
            profilepage.Show();
            Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();
            registerPage.Show();
            Close();
        }
    }
}
