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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Window
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            //---------------Validation---------------\\
            if (pswPassword.Password != pswPassword_Copy.Password) { MessageBox.Show("The password did nto match. Please try again."); 
                pswPassword.Password = ""; pswPassword_Copy.Password = ""; return; }  //Validates whether two password inputs are the same
            if (pswPassword.Password.Length <8){
                MessageBox.Show("The password should be longer than 7 characters");     //Validates the password length
                pswPassword.Password = ""; pswPassword_Copy.Password = ""; return;
            }
            string CheckEmailSQL = $"SELECT email FROM accounts WHERE email = '{txtbEmail.Text}'";  //This sql returns something if the inputted email is in the system.
            Session.conn.Open();
            MySqlCommand cmd = new MySqlCommand(CheckEmailSQL, Session.conn);
            MySqlDataReader CheckEMail = cmd.ExecuteReader();
            if (CheckEMail.Read()) { MessageBox.Show("This email is already registered to an account."); //If what is returned can be read from that means it exists and shouldnt be used.
                txtbEmail.Text = ""; return;}
            CheckEMail.Close();
            //---------------Inserting the Account---------------\\
            string AccountCreationSQL = "INSERT INTO Accounts (FirstName, LastName, Email, Password) VALUES (@FirstName, @LastName, @Email, SHA(@Password))";
            cmd = new MySqlCommand(AccountCreationSQL, Session.conn);
            cmd.Parameters.AddWithValue("@Email", txtbEmail.Text);    //These parameters prevent SQL injections
            cmd.Parameters.AddWithValue("@Password", pswPassword.Password);
            cmd.Parameters.AddWithValue("@FirstName", txtbFirstName.Text);  
            cmd.Parameters.AddWithValue("@LastName", txtbLastName.Text);
            cmd.ExecuteNonQuery();
            Session.conn.Close();
            MessageBox.Show("Account successfully created");
            txtbLastName.Text = "";
            txtbFirstName.Text = "";
            pswPassword.Password = "";
            txtbEmail.Text = "";
        }
        private void btnBackArrow_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage profilepage = new ProfilePage();
            profilepage.Show();
            Close();
        }
    }
}
