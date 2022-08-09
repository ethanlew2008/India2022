using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using System.Diagnostics;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Connectivity;

namespace India2022
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        bool flight = false;
        Stopwatch flighttime = new Stopwatch();
        double co2 = 0;
        double countries = 0;

        public TabbedPage1()
        {
            InitializeComponent();
           
        }

        private void StartFlyButton_Clicked(object sender, EventArgs e)
        {
            if (!flight) { flighttime.Start();  flight = true; UpdateFlyButton.Opacity = 1; StartFlyButton.Text = "Stop"; }
            else { flight = false; flighttime.Reset(); UpdateFlyButton.Opacity = 0;  StartFlyButton.Text = "Start"; }                       
        }

        private void UpdateFlyButton_Clicked(object sender, EventArgs e)
        {
            Time.Text = "";
            int temp = Convert.ToInt32(3.15e+7 - flighttime.ElapsedMilliseconds); temp /= 1000; temp /= 60;
            if (temp <= 0) { Time.Text = "Welcome"; flighttime.Reset(); return; }
            TimeSpan spWorkMin = TimeSpan.FromMinutes(temp);
            string workHours = spWorkMin.ToString(@"hh\:mm");

            double percentage = Convert.ToDouble(flighttime.ElapsedMilliseconds) / 3.15e+7; percentage *= 100; percentage = 100 - percentage;
            percentage = Convert.ToInt32(percentage);

            co2 = flighttime.ElapsedMilliseconds; co2 /= 1000; co2/= 60; co2 *= 4;

            countries = temp / 38;
            countries = Math.Floor(countries);
            
            if (countries >= 10)
            {
                countries = countries - countries;
                countries *= -1;
            }

            if (CrossConnectivity.Current.IsConnected) { BatteryPer.Text = "Data: On"; }
            else { BatteryPer.Text = "Data: Off"; }

            var indiatime = DateTime.UtcNow.AddMinutes(330);
            indiatime = Convert.ToDateTime(indiatime.ToString("HH:mm"));
            string temp1 = Convert.ToString(indiatime);
            temp1 = temp1.Substring(10);
            temp1 = temp1.Remove(temp1.Length - 3, 3);

            int uktime = DateTime.UtcNow.Hour + 1;
            string mins = Convert.ToString(DateTime.UtcNow.Minute);
            if(mins.Length == 1) { mins = "0" + mins; }

            Time.Text += workHours;
            Percentage.Text = percentage + "% Left";          
            CountriesFlew.Text = countries + " Countries";
            LocalTime.Text = DateTime.Now.ToString("HH:mm") + " Local";
            TimeIn.Text = "IN:" + temp1;
            TimeUK.Text = "UK:" + uktime + ":" + mins;
            if(co2 >= 100) { co2 /= 1000; CO2.Text = co2 + "T CO2"; }
            else { CO2.Text = co2 + "KG CO2"; }


        }
    }
}