using System;
using System.Windows;
using System.Windows.Input;
using System.Net;
using Newtonsoft.Json;
using weatherDesktop.Entity;

namespace weatherDesktop
{
    /// <summary>
    /// Interaction logic for Air.xaml
    /// </summary>
    public partial class Air : Window
    {
        //Air quality API
        private const string AirQualityUrl = "http://web.juhe.cn:8080/environment/air/pm?city=shanghai&key=f7dd7f09028e8ac3b5efcbb78654e79c";

        public Air()
        {
            InitializeComponent();
            LoadJson(AirQualityUrl);
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
            var airinfo = JsonConvert.DeserializeObject<AirInfo>(result);
            pm25.Content = airinfo.Result[0].Pm25;
            aqi.Content = airinfo.Result[0].Aqi;
        }
    
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }
    }
}
