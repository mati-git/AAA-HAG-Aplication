using MySql.Data.MySqlClient;
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

namespace AAA_HAG_Aplication
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Window
    {
        public ProfilePage()
        {
            InitializeComponent();
            DefineAccountInfo();
        }
        private void DefineAccountInfo()
        {
            string AccountInfoSQL = $"SELECT Firstname, Lastname, Email FROM Accounts WHERE Email = {AccountEmail.address}";
            MySqlCommand cmd = new MySqlCommand(AccountInfoSQL, Session.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            
            txtb
        }
        private void btnLogInPage_Click(object sender, RoutedEventArgs e)
        {
            LogInPage logInPage = new LogInPage();
            logInPage.Show();
            Close();
        }
    }
}
