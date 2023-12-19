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
            if (AccountEmail.address == null) { return; }
            Session.conn.Open();
            string AccountInfoSQL = $"SELECT Firstname, Lastname, Email, ConditionDescription FROM Accounts, conditions, customerconditions " +
                $"WHERE Accounts.AccountID = customerconditions.AccountID " +
                $"AND conditions.conditionid = customerconditions.conditionID " +
                $"AND Email = '{AccountEmail.address}'";
            MySqlCommand cmd = new MySqlCommand(AccountInfoSQL, Session.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            txtbFullName.Text = rdr.GetString(0) + " " + rdr.GetString(1);
            txtbEmail.Text = rdr.GetString(2);
            txtbConditions.Text = rdr.GetString(3);

        }
        private void btnLogInPage_Click(object sender, RoutedEventArgs e)
        {
            LogInPage logInPage = new LogInPage();
            logInPage.Show();
            Session.conn.Close();
            Close();
        }
        private void btnBackArrow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Session.conn.Close();
            Close();
        }
    }
                         }
