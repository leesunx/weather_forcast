﻿using System;
using System.Net;
using System.Windows;
using Newtonsoft.Json;
using weatherDesktop.Entity;

namespace weatherDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const string NowTempUrl = "http://www.weather.com.cn/data/sk/101020100.html";
        private const string ForecastTempUrl = "http://www.weather.com.cn/data/cityinfo/101020100.html";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadJson(NowTempUrl);
            LoadJson(ForecastTempUrl);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            LoadJson(NowTempUrl);
            updateTime.Content = DateTime.Now.ToShortTimeString();
        }

        //download json async
        private void LoadJson(string url)
        {
            var client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            // Specify that the DownloadStringCallback2 method gets called
            // when the download completes.
            client.DownloadStringCompleted += LoadJsonCallback;
            client.DownloadStringAsync(new Uri(url));
        }

        //callback method when download complete
        private void LoadJsonCallback(Object sender, DownloadStringCompletedEventArgs e)
        {
            // If the request was not canceled and did not throw
            // an exception, display the resource.
            if (e.Cancelled || e.Error != null) return;
            var result = e.Result;
            var weatherinfo = JsonConvert.DeserializeObject<WeatherInfo>(result);
            //city
            location.Content = weatherinfo.weatherinfo.city ?? location.Content;
            //realtime                
            tempNow.Content = weatherinfo.weatherinfo.temp ?? tempNow.Content;
            wd.Content = weatherinfo.weatherinfo.wd ?? wd.Content;
            ws.Content = weatherinfo.weatherinfo.ws ?? ws.Content;
            sd.Content = weatherinfo.weatherinfo.sd ?? sd.Content;
            pubTime.Content = weatherinfo.weatherinfo.time ?? pubTime.Content;
            //forcast
            tempHi.Content = weatherinfo.weatherinfo.temp1 ?? tempHi.Content;
            tempLow.Content = weatherinfo.weatherinfo.temp2 ?? tempLow.Content;
            weather.Content = weatherinfo.weatherinfo.weather ?? weather.Content;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var option = new OptionWindow();
            option.ShowDialog();
        }
    }
}