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
        bool onlyhome = false; 

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

            onlyhome = true;
            UniversialUpdate();
        }

        private void StartFlyButton_Clicked(object sender, EventArgs e)
        {
            if (!flight) { flighttime.Start(); flight = true; UpdateFlyButton.Opacity = 1; StartFlyButton.Text = "Stop"; UniversialUpdate(); }
            else { flight = false; flighttime.Reset(); UpdateFlyButton.Opacity = 0; StartFlyButton.Text = "Start"; }
        }

        private void UpdateFlyButton_Clicked(object sender, EventArgs e)
        {
            UniversialUpdate();
        }

        private void UpdateButtonHome_Clicked(object sender, EventArgs e)
        {
            UniversialUpdate();
        }

        private void FlashButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                double conversion = Convert.ToDouble(CurrencyGBP.Text) / 82.42;
                int temp = Convert.ToInt32(conversion);
                conversion = Math.Round(conversion, 2);
                CurrencyUSD.Text = "$" + conversion;


                conversion = Convert.ToDouble(CurrencyGBP.Text) / 92.07;
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
            UniversialUpdate();         
        }

        private void Startsleep_Clicked(object sender, EventArgs e)
        {
            if (sleep.IsRunning) 
            {
                sleep.Stop();
                Startsleep.Text = "Sleep";
                Updatesleep.Opacity = 0;
                BarBackgroundColor = Color.Orange;
                FlightPage.IconImageSource = "IndiaPlaneIcon.jpg";
                IndiaHome.IconImageSource = "IndiaHouseIcon.jpg";
                IndiaSleep.IconImageSource = "IndiaSleepIcon.jpg";
            }
            else
            {
                sleep.Start();               
                Startsleep.Text = "End";
                Updatesleep.Opacity = 1;
                BarBackgroundColor = Color.MediumPurple;
                FlightPage.IconImageSource = "IndiaPlaneIconNight.jpg";
                IndiaHome.IconImageSource = "IndiaHouseIconNight.jpg";
                IndiaSleep.IconImageSource = "IndiaSleepIconNight.jpg";
                UniversialUpdate();
            }
        }




        public void UniversialUpdate()
        {
            //Home
            var indiatime = DateTime.UtcNow.AddMinutes(330).ToString("HH:mm");
            var uktime = DateTime.UtcNow.AddMinutes(60).ToString("HH:mm");

            var profiles = Connectivity.ConnectionProfiles;

            if ((CrossConnectivity.Current.IsConnected) && !profiles.Contains(ConnectionProfile.WiFi)) { Connection.Text = "Data On"; }
            else { Connection.Text = "Data Off"; }

            string chargin = "";

            chargin = Battery.PowerSource.ToString();
            if (chargin == "AC") { chargin = "Wall Plug"; }
            else if (chargin == "Usb") { chargin = "USB"; }


            double battcharge = Battery.ChargeLevel;
            battcharge *= 100;

            string energysave = "";
            if (Battery.EnergySaverStatus == EnergySaverStatus.On) { energysave = "Battery Saver On"; }
            else { energysave = "Battery Saver Off"; }

            HomeLocalTime.Text = "LOC: " + DateTime.Now.ToString("HH:mm");
            TimeHomeUK.Text = "LON: " + uktime;
            TimeHomeIN.Text = "DEL:" + indiatime;
                
            BatteryLevel.Text = "Batt: " + battcharge + "%";
            BatterySaver.Text = energysave;
            Appversion.Text = AppInfo.VersionString;
            Charging.Text = chargin;

            //Sleep
            if (sleep.IsRunning)
            {
                sleephours = sleep.ElapsedMilliseconds / 1000; sleephours /= 60;

                TimeSpan spWorkMin2 = TimeSpan.FromMinutes(sleephours);
                string workHours2 = spWorkMin2.ToString(@"hh\:mm");

                var indiatime2 = DateTime.UtcNow.AddMinutes(330).ToString("HH:mm");
                var uktime2 = DateTime.UtcNow.AddMinutes(60).ToString("HH:mm");

                double percentage2 = sleep.ElapsedMilliseconds / 2.88e+7;

                double battcharge2 = Battery.ChargeLevel;
                battcharge *= 100;

                string energysave2 = "";
                if (Battery.EnergySaverStatus == EnergySaverStatus.On) { energysave = "Battery Saver On"; }
                else { energysave = "Battery Saver Off"; }

                SleepPercentOfTarget.Text = Convert.ToInt32((1 - percentage2) * 100) + "%";
                SleepTime.Text = workHours2;
                SleepBreaths.Text = Convert.ToInt32(sleephours * 16) + " Breaths";
                BattPersleep.Text = "Batt: " + battcharge2 + "%";

                GBRLocal.Text = "LON: " + uktime2;
                IndiaLocal.Text = "DEL:" + indiatime2;

                BatterySaver1.Text = energysave;
                Appversion1.Text = AppInfo.VersionString;
            }

            //Planes
            if (flight)
            {
                Time.Text = "";
                int temp = Convert.ToInt32(3.15e+7 - flighttime.ElapsedMilliseconds);
                if (temp <= 0) { Time.Text = "Welcome"; flighttime.Reset(); return; }
                TimeSpan spWorkMin = TimeSpan.FromMilliseconds(temp);
                string workHours = spWorkMin.ToString(@"hh\:mm");

                double percentage = flighttime.ElapsedMilliseconds / 3.15e+7;

                co2 = flighttime.ElapsedMilliseconds * 0.0015;

                countries = flighttime.ElapsedMilliseconds / 1000; countries /= 60;
                countries /= 38;
                countries = Convert.ToInt32(countries);
                if (countries >= 10) { countries = countries - countries; countries *= -1; }


                if (Connectivity.NetworkAccess == NetworkAccess.None) { Airmode.Text = "AirMode: Yes"; }
                else { Airmode.Text = "AirMode: No"; }

                var indiatime3 = DateTime.UtcNow.AddMinutes(330).ToString("HH:mm");
                var uktime3 = DateTime.UtcNow.AddMinutes(60).ToString("HH:mm");

                Time.Text += workHours;

                Percentage.Text = Convert.ToInt32((1 - percentage) * 100) + "% Left";

                CO2.Text = Math.Round(co2 / 1000, 2) + "T CO2";

                CountriesFlew.Text = countries + " Countries";

                LocalTime.Text = DateTime.Now.ToString("HH:mm") + " Local";
                TimeIn.Text = "IN:" + indiatime3;
                TimeUK.Text = "UK: " + uktime3;

                Alt();
                Speed();
            }
        }
    }
}