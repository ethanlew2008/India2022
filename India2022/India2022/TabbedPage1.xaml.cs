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
using static Xamarin.Essentials.Permissions;
using Battery = Xamarin.Essentials.Battery;

namespace India2022
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        bool flight = false;
        static bool flash = true;
        Stopwatch flighttime = new Stopwatch();
        Stopwatch sleep = new Stopwatch(); double sleephours = 0;
        double co2 = 0;
        double countries = 14;
        string input = "";




        public TabbedPage1()
        {
            InitializeComponent();
            CurrentPage = Children[1];
            

            var indiatime = DateTime.UtcNow.AddMinutes(330);
            indiatime = Convert.ToDateTime(indiatime.ToString("HH:mm"));
            string temp1 = Convert.ToString(indiatime);
            temp1 = temp1.Substring(10);
            temp1 = temp1.Remove(temp1.Length - 3, 3);

            int uktime = DateTime.UtcNow.Hour + 1;
            string mins = Convert.ToString(DateTime.UtcNow.Minute);
            if (mins.Length == 1) { mins = "0" + mins; }
            if (uktime >= 24) { uktime -= 24; }

            var profiles = Connectivity.ConnectionProfiles;
            if ((CrossConnectivity.Current.IsConnected) && !profiles.Contains(ConnectionProfile.WiFi)) { Connection.Text = "Data On"; }
            else { Connection.Text = "Data Off"; }

            double battcharge = Battery.ChargeLevel;
            battcharge *= 100;

            string chargin = "";
            chargin = Battery.PowerSource.ToString();
            if (chargin == "AC") { chargin = "Wall Plug"; }
            else if (chargin == "Usb") { chargin = "USB"; }

            string energysave = "";
            if (Battery.EnergySaverStatus == EnergySaverStatus.On) { energysave = "Battery Saver On"; }
            else { energysave = "Battery Saver Off"; }          

            HomeLocalTime.Text = "LOC: " + DateTime.Now.ToString("HH:mm");
            TimeHomeUK.Text = "LON: " + uktime + ":" + mins;
            TimeHomeIN.Text = "DEL:" + temp1;
            Charging.Text = chargin;
            BatterySaver.Text = energysave;
            Appversion.Text = AppInfo.VersionString;
            BatteryLevel.Text = "Batt: " + battcharge + "%";
        }

        private void StartFlyButton_Clicked(object sender, EventArgs e)
        {
            if (!flight) { flighttime.Start(); flight = true; UpdateFlyButton.Opacity = 1; StartFlyButton.Text = "Stop"; }
            else { flight = false; flighttime.Reset(); UpdateFlyButton.Opacity = 0; StartFlyButton.Text = "Start"; }
        }

        private void UpdateFlyButton_Clicked(object sender, EventArgs e)
        {;
            Time.Text = "";
            int temp = Convert.ToInt32(3.15e+7 - flighttime.ElapsedMilliseconds); temp /= 1000; temp /= 60;
            if (temp <= 0) { Time.Text = "Welcome"; flighttime.Reset(); return; }
            TimeSpan spWorkMin = TimeSpan.FromMinutes(temp);
            string workHours = spWorkMin.ToString(@"hh\:mm");

            double percentage = Convert.ToDouble(flighttime.ElapsedMilliseconds) / 3.15e+7; percentage *= 100; percentage = 100 - percentage;
            percentage = Convert.ToInt32(percentage);

            co2 = flighttime.ElapsedMilliseconds; co2 /= 1000; co2 /= 60; co2 *= 4;

            countries = flighttime.ElapsedMilliseconds / 1000; countries /= 60;
            countries /= 38;
            countries = Convert.ToInt32(countries);

            if (countries >= 10)
            {
                countries = countries - countries;
                countries *= -1;
            }

            if (CrossConnectivity.Current.IsConnected) { Airmode.Text = "AirMode: Yes"; }
            else { Connection.Text = "AirMode: No"; }

            

            var indiatime = DateTime.UtcNow.AddMinutes(330);
            indiatime = Convert.ToDateTime(indiatime.ToString("HH:mm"));
            string temp1 = Convert.ToString(indiatime);
            temp1 = temp1.Substring(10);
            temp1 = temp1.Remove(temp1.Length - 3, 3);

            int uktime = DateTime.UtcNow.Hour + 1;
            string mins = Convert.ToString(DateTime.UtcNow.Minute);
            if (mins.Length == 1) { mins = "0" + mins; }
            if (uktime >= 24) { uktime -= 24; }

            Time.Text += workHours;
            Percentage.Text = percentage + "% Left";
            CountriesFlew.Text = countries + " Countries";
            LocalTime.Text = DateTime.Now.ToString("HH:mm") + " Local";
            TimeIn.Text = "IN:" + temp1;
            TimeUK.Text = "UK: " + uktime + ":" + mins;
            Alt();
            Speed();

            if (co2 >= 100) { co2 /= 1000; co2 = Math.Round(co2, 2); CO2.Text = co2 + "T CO2"; }
            else { co2 = Math.Ceiling(co2); CO2.Text = co2 + "KG CO2"; }


        }

        private void UpdateButtonHome_Clicked(object sender, EventArgs e)
        {
            var indiatime = DateTime.UtcNow.AddMinutes(330);
            indiatime = Convert.ToDateTime(indiatime.ToString("HH:mm"));
            string temp1 = Convert.ToString(indiatime);
            temp1 = temp1.Substring(10);
            temp1 = temp1.Remove(temp1.Length - 3, 3);

            int uktime = DateTime.UtcNow.Hour + 1;
            string mins = Convert.ToString(DateTime.UtcNow.Minute);
            if (mins.Length == 1) { mins = "0" + mins; }


            var profiles = Connectivity.ConnectionProfiles;

            if ((CrossConnectivity.Current.IsConnected) && !profiles.Contains(ConnectionProfile.WiFi)) { Connection.Text = "Data On"; }
            else { Connection.Text = "Data Off"; }

            string chargin = "";

            chargin = Battery.PowerSource.ToString();
            if (chargin == "AC") { chargin = "Wall Plug"; }
            else if (chargin == "Usb") { chargin = "USB"; }

            if (uktime >= 24) { uktime -= 24; }

            double battcharge = Battery.ChargeLevel;
            battcharge *= 100;

            string energysave = "";
            if (Battery.EnergySaverStatus == EnergySaverStatus.On) { energysave = "Battery Saver On"; }
            else { energysave = "Battery Saver Off"; }

            HomeLocalTime.Text = "LOC: " + DateTime.Now.ToString("HH:mm");
            TimeHomeUK.Text = "LON: " + uktime + ":" + mins;
            TimeHomeIN.Text = "DEL:" + temp1;
            BatteryLevel.Text = "Batt: " + battcharge + "%";
            BatterySaver.Text = energysave;
            Appversion.Text = AppInfo.VersionString;
            Charging.Text = chargin;
        }

        private void FlashButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                double conversion = Convert.ToDouble(CurrencyGBP.Text) / 82.15;
                int temp = Convert.ToInt32(conversion);
                conversion = Math.Round(conversion, 2);
                CurrencyUSD.Text = "$" + conversion;


                conversion = Convert.ToDouble(CurrencyGBP.Text) / 92.94;
                temp = Convert.ToInt32(conversion);
                conversion = Math.Round(conversion, 2);
                CurrencyGBP.Text = "£" + conversion;

            }
            catch (Exception) { CurrencyGBP.Text = "Error"; CurrencyUSD.Text = "Error";  input = ""; return; }

        }

        public async void Alt()
        {
            Altbx.Text = "";
            try
            {
                
                var location = await Geolocation.GetLastKnownLocationAsync();
                double temp = Convert.ToDouble(location.Altitude);
                temp *= 3.2808399;
                int temp2 = Convert.ToInt32(temp);
                if (location != null)
                {
                    Altbx.Text += temp2 + "ft";
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public async void Speed()
        {
            SpdBx.Text = "";
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double temp = Convert.ToDouble(location.Speed);
                temp *= 2.23693629;
                int temp2 = Convert.ToInt32(temp);

                if (location != null)
                {
                    SpdBx.Text += temp2 + "MPH";
                }
            }
            catch (Exception)
            {

            }
        }

        private void Updatesleep_Clicked(object sender, EventArgs e)
        {

            if (sleep.IsRunning)
            {
                sleephours = sleep.ElapsedMilliseconds / 1000; sleephours /= 60;

                TimeSpan spWorkMin = TimeSpan.FromMinutes(sleephours);
                string workHours = spWorkMin.ToString(@"hh\:mm");

                var indiatime = DateTime.UtcNow.AddMinutes(330);
                indiatime = Convert.ToDateTime(indiatime.ToString("HH:mm"));
                string temp1 = Convert.ToString(indiatime);
                temp1 = temp1.Substring(10);
                temp1 = temp1.Remove(temp1.Length - 3, 3);

                int uktime = DateTime.UtcNow.Hour + 1;
                string mins = Convert.ToString(DateTime.UtcNow.Minute);
                if (mins.Length == 1) { mins = "0" + mins; }

                double elapsedtime = sleep.ElapsedMilliseconds; elapsedtime /= 1000; elapsedtime /= 60;
                double percentage = elapsedtime /= 480;
                percentage = Math.Ceiling(percentage);


                double battcharge = Battery.ChargeLevel;
                battcharge *= 100;

                string energysave = "";
                if (Battery.EnergySaverStatus == EnergySaverStatus.On) { energysave = "Battery Saver On"; }
                else { energysave = "Battery Saver Off"; }


                SleepPercentOfTarget.Text = percentage + "%";
                SleepTime.Text = workHours;
                SleepBreaths.Text = Convert.ToInt32(sleephours * 16) + " Breaths";
                BattPersleep.Text = "Batt: " + battcharge + "%";

                GBRLocal.Text = "LON: " + uktime + ":" + mins;
                IndiaLocal.Text = "DEL:" + temp1;     
                
                BatterySaver1.Text = energysave;
                Appversion1.Text = AppInfo.VersionString;

            }
            else
            {
                return;
            }
        }

        private void Startsleep_Clicked(object sender, EventArgs e)
        {
            if (sleep.IsRunning) { sleep.Stop(); Startsleep.Text = "Sleep"; Updatesleep.Opacity = 0; BarBackgroundColor = Color.Orange; }
            else { sleep.Start(); Startsleep.Text = "End"; Updatesleep.Opacity = 1; BarBackgroundColor = Color.MediumPurple; }
            
        }
    }
}