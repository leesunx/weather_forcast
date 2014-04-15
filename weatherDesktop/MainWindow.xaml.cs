using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using Newtonsoft.Json;
using weatherDesktop.Entity;

namespace weatherDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string nowTempUrl = "http://www.weather.com.cn/data/sk/101020100.html";
        const string forecastTempUrl = "http://www.weather.com.cn/data/cityinfo/101020100.html";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //updateNowInfo();
            //updateForcastInfo();
            loadJSON();
        }

        private WeatherInfo getinfo(string url)
        {
            using (var webClient = new System.Net.WebClient())
            {
                webClient.Encoding = System.Text.Encoding.UTF8;
                var json = webClient.DownloadString(url);                
                var weatherinfo = JsonConvert.DeserializeObject<WeatherInfo>(json);
                return weatherinfo;
            }            
        }

        private void updateNowInfo()
        {
            WeatherInfo nowinfo = getinfo(nowTempUrl);
            //prase to the page
            location.Content = nowinfo.weatherinfo.city;
            tempNow.Content = nowinfo.weatherinfo.temp;
            wd.Content = nowinfo.weatherinfo.wd;
            ws.Content = nowinfo.weatherinfo.ws;
            sd.Content = nowinfo.weatherinfo.sd;
            pubTime.Content = nowinfo.weatherinfo.time;
        }

        private void updateForcastInfo()
        {
            WeatherInfo forcastinfo = getinfo(forecastTempUrl);
            //prase to the page            
            tempHi.Content = forcastinfo.weatherinfo.temp2;
            tempLow.Content = forcastinfo.weatherinfo.temp1;
            weather.Content = forcastinfo.weatherinfo.weather;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            updateNowInfo();
            updateTime.Content = DateTime.Now.ToShortTimeString();
        }        

        //downloadstringasync
        public void loadJSON()
        {
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            // Specify that the DownloadStringCallback2 method gets called
            // when the download completes.
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(loadJSONCallback);
            client.DownloadStringAsync(new Uri(nowTempUrl));
            return;
        }

        public void loadJSONCallback(Object sender, DownloadStringCompletedEventArgs e)
        {
            // If the request was not canceled and did not throw
            // an exception, display the resource.
            if (!e.Cancelled && e.Error == null)
            {
                string result = (string)e.Result;
                var weatherinfo = JsonConvert.DeserializeObject<WeatherInfo>(result);
                location.Content = weatherinfo.weatherinfo.city;
                tempNow.Content = weatherinfo.weatherinfo.temp;
                wd.Content = weatherinfo.weatherinfo.wd;
                ws.Content = weatherinfo.weatherinfo.ws;
                sd.Content = weatherinfo.weatherinfo.sd;
                pubTime.Content = weatherinfo.weatherinfo.time;

            }
        }
    }
}
