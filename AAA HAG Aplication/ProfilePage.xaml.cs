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
            Session.conn.Open();
            InitializeComponent();
            DefineAccountInfo();
            AfflictionTask();

        }
        private async void AfflictionTask()
        {
            await UpdateAfflicitons();
        }
        private void DefineAccountInfo()
        {
            if (AccountEmail.address == null) { return; } // Checks if the user is signed in by checking if
                                                          // the address variable contains anything

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
            rdr.Close();
        }

        private async Task UpdateAfflicitons()
        {
            if (AccountEmail.address == null) { return; } // Checks if the user is signed in by checking if
                                                          // the address variable contains anything

            await Task.Delay(500);
            string AfflictionsTheUserDoesNotHaveSQL = $"SELECT ConditionDescription " +
                $"FROM accounts, customerconditions, conditions " +
                $"WHERE email = '{AccountEmail.address}' " +                //Checks what afflictions the user does not have
                $"AND accounts.accountid = customerconditions.AccountID " +
                $"AND customerconditions.ConditionID != Conditions.ConditionID;";
            MySqlCommand cmd = new MySqlCommand(AfflictionsTheUserDoesNotHaveSQL, Session.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                cmbAddAfflicitons.Items.Add(rdr.GetString(0));
            }
            rdr.Close();

            await Task.Delay(500);
            string AfflictionsTheUserDoesHaveSQL = $"SELECT ConditionDescription " +
                $"FROM accounts, customerconditions, conditions " +
                $"WHERE email = '{AccountEmail.address}' " +                //Checks what afflictions the user does have
                $"AND accounts.accountid = customerconditions.AccountID " +
                $"AND customerconditions.ConditionID = Conditions.ConditionID;";
            cmd = new MySqlCommand(AfflictionsTheUserDoesHaveSQL, Session.conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                cmbRemoveAfflicitons.Items.Add(rdr.GetString(0));
            }
            rdr.Close();
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

        private void AddAffliciton_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            string GetAccountIDSQL = $"";


            string MakeChangeSQL = $"INSERT INTO customerconditions (conditionID, accountid) Values ( 2, 10009)";

        }

        private void RemoveAffliciton_SelectionChange(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
