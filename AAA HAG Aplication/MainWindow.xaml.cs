using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
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
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crmf;

namespace AAA_HAG_Aplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IDictionary<string, string> TempForecasts = new Dictionary<string, string>(); // This being used to store the forecast
            IDictionary<string, string> HumidtyForecast = new Dictionary<string, string>(); // For the Tab Control (tbcForecastPanels)
            Session.conn = new MySqlConnection("server=ND-COMPSCI;" +
                         "user=TL_S2201087;" +
                         "database=TL_S2201087_hag;" +           // This is the connection string for the database.
                         "port=3306;" +                         // It defines conn in session
                         "password=Notre150905");
            string request = "http://api.weatherapi.com/v1/forecast.json?key=45bb9ec621fd4e68b2591219232311&q=Leeds&days=7&aqi=no&alerts=no";
            // ^ This the link to the API
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(request).Result;  //This is where the information in the API is received
            var jsonObject = JsonNode.Parse(response); //The information is intepreted as a JSON file for later access
            string Ctemp = jsonObject["current"]["temp_c"].ToString(); // Current temperature. This is displayed at the top of the menu
            for (int i = 0; i < 12; i++)
            {
                string Time = jsonObject["forecast"]["forecastday"][0]["hour"][i]["time"].ToString().Substring(10);
                string Temp = jsonObject["forecast"]["forecastday"][0]["hour"][i]["temp_c"].ToString();
                string Humidity = jsonObject["forecast"]["forecastday"][0]["hour"][i]["humidity"].ToString();
                TempForecasts.Add(Time, Temp);
                HumidtyForecast.Add(Time, Humidity);
            }
            txtMainCurrentTemp.Text = Ctemp + "c";
            foreach (string Times in TempForecasts.Keys) //Iterating through the keys to make a tab for each stored time
            {
                TabItem newTabItem = new TabItem //This is the actual tab being added to the tab control
                {
                    Header = Times, //This is the title of the tab (What written on the tab itself)
                    Content = "Temperature - " + TempForecasts[Times] + "\n Humidity - " + HumidtyForecast[Times]
                };
                tbcForecastPanels.Items.Add(newTabItem);
            }
            string GetAdviceSQL = $"SELECT ConditionDescription, Advice " +
                $"FROM accounts, customerconditions, conditions, advice, conditionadvice " +
                $"WHERE accounts.email = '{AccountEmail.address}' " +
                $"AND accounts.accountid = customerconditions.accountid " +  //This statement pulls the condition and the advice from the system
                $"AND customerconditions.conditionid = conditions.conditionid " +
                $"AND customerconditions.conditionid = conditionadvice.conditionid " +
                $"AND conditionadvice.adviceid = advice.adviceid ";
            MySqlCommand cmd = new MySqlCommand(GetAdviceSQL, Session.conn);
            Session.conn.Open();
            MySqlDataReader CheckAdvice = cmd.ExecuteReader(); 
            if (CheckAdvice.Read()) { txtConditionAdvice.Text = ""; CheckAdvice.Close(); }
            while (CheckAdvice.Read())
            {
                string ConditionName = CheckAdvice.GetString(0);
                string ConditionAdvice = CheckAdvice.GetString(1); // The advice is added in list format. 
                txtConditionAdvice.Text += "Advice\n " + ConditionName + " - " + ConditionAdvice;
            }
            CheckAdvice.Close();
            Session.conn.Close();

        }
        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage profilePage = new ProfilePage();
            profilePage.Show();
            Hide();
        }
    }
}
