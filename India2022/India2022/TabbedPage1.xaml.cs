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
            if(Battery.EnergySaverStatus == EnergySaverStatus.On) { energysave = "Battery Saver On"; }
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
            if (!flight) { flighttime.Start();  flight = true; UpdateFlyButton.Opacity = 1; StartFlyButton.Text = "Stop"; }
            else { flight = false; flighttime.Reset(); UpdateFlyButton.Opacity = 0;  StartFlyButton.Text = "Start"; }
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

            co2 = flighttime.ElapsedMilliseconds; co2 /= 1000; co2/= 60; co2 *= 4;

            countries = flighttime.ElapsedMilliseconds / 1000; countries /= 60;
            countries /= 38;
            countries = Convert.ToInt32(countries);

            if (countries >= 10)
            {
                countries = countries - countries;
                countries *= -1;
            }

            if (CrossConnectivity.Current.IsConnected) { BatteryPer.Text = "AirMode: Yes"; }
            else { Connection.Text = "AirMode: No"; }

            var indiatime = DateTime.UtcNow.AddMinutes(330);
            indiatime = Convert.ToDateTime(indiatime.ToString("HH:mm"));
            string temp1 = Convert.ToString(indiatime);
            temp1 = temp1.Substring(10);
            temp1 = temp1.Remove(temp1.Length - 3, 3);

            int uktime = DateTime.UtcNow.Hour + 1;
            string mins = Convert.ToString(DateTime.UtcNow.Minute);
            if(mins.Length == 1) { mins = "0" + mins; }
            if (uktime >= 24) { uktime -= 24; }

            Time.Text += workHours;
            Percentage.Text = percentage + "% Left";          
            CountriesFlew.Text = countries + " Countries";
            LocalTime.Text = DateTime.Now.ToString("HH:mm") + " Local";
            TimeIn.Text = "IN:" + temp1;
            TimeUK.Text = "UK: " + uktime + ":" + mins;

            if(co2 >= 100) { co2 /= 1000; co2 = Math.Round(co2, 2); CO2.Text = co2 + "T CO2"; }
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
            if(chargin == "AC") { chargin = "Wall Plug"; }
            else if(chargin == "Usb") { chargin = "USB"; }

            if(uktime >= 24) { uktime -= 24; }

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
            FLash2Async();
        }

        static async Task FLash2Async()
        {
            flash = !flash;
            if (flash) { await Xamarin.Essentials.Flashlight.TurnOffAsync(); }
            else { await Xamarin.Essentials.Flashlight.TurnOnAsync(); }       
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            input += "1";
            Box.Text = input;
        }

        private void Button2_Clicked(object sender, EventArgs e)
        {
            input += "2";
            Box.Text = input;
        }

        private void Button3_Clicked(object sender, EventArgs e)
        {
            input += "3";
            Box.Text = input;
        }

        private void Button4_Clicked(object sender, EventArgs e)
        {
            input += "4";
            Box.Text = input;
        }

        private void Button5_Clicked(object sender, EventArgs e)
        {
            input += "5";
            Box.Text = input;
        }

        private void Button6_Clicked(object sender, EventArgs e)
        {
            input += "6";
            Box.Text = input;
        }

        private void Button7_Clicked(object sender, EventArgs e)
        {
            input += "7";
            Box.Text = input;
        }

        private void Button8_Clicked(object sender, EventArgs e)
        {
            input += "8";
            Box.Text = input;
        }

        private void Button9_Clicked(object sender, EventArgs e)
        {
            input += "9";
            Box.Text = input;
        }

        private void Button0_Clicked(object sender, EventArgs e)
        {
            input += "0";
            Box.Text = input;
        }

        private void Buttondel_Clicked(object sender, EventArgs e)
        {
            string ostr = "";
            try { ostr = input.Remove(input.Length - 1, 1); } catch (Exception) { return; }
            input = ostr; Box.Text = input;
        }

        private void ButtonDot_Clicked(object sender, EventArgs e)
        {
            input += ".";
            Box.Text = input;
        }

        private void ButtonGBP_Clicked(object sender, EventArgs e)
        {
            try
            {
                double conversion = Convert.ToDouble(input) / 96.68;
                int temp = Convert.ToInt32(conversion);
                conversion = Math.Round(conversion, 2);
                Box.Text = "£" + conversion;
                input = "";
            }
            catch (Exception) { Box.Text = "Error"; input = ""; return;}
        }

        private void ButtonUSD_Clicked(object sender, EventArgs e)
        {
            try
            {
                double conversion = Convert.ToDouble(input) / 79.63;
                int temp = Convert.ToInt32(conversion);
                conversion = Math.Round(conversion, 2);
                Box.Text = "$" + conversion;
                input = "";
            }
            catch (Exception) { Box.Text = "Error"; input = ""; return; }
        }

        private void ButtonEUR_Clicked(object sender, EventArgs e)
        {
            try
            {
                double conversion = Convert.ToDouble(input) / 81.72;
                int temp = Convert.ToInt32(conversion);
                conversion = Math.Round(conversion, 2);
                Box.Text = "€" + conversion;
                input = "";
            }
            catch (Exception) { Box.Text = "Error"; input = ""; return; }
        }
    }
}