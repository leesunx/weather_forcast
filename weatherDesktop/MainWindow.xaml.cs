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
            updateNowInfo();
            updateForcastInfo();
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
            tempHi.Content = forcastinfo.weatherinfo.temp1;
            tempLow.Content = forcastinfo.weatherinfo.temp2;
            weather.Content = forcastinfo.weatherinfo.weather;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            updateNowInfo();
            updateTime.Content = DateTime.Now.ToShortTimeString();
        }        
    }
}
