using Newtonsoft.Json;

namespace weatherDesktop.Entity
{
    public class AirInfo
    {
        public AInfo[] Result;
    }

    public class AInfo
    {
        //城市
        public string City { get; set; }
        //PM2.5
        [JsonProperty("PM2.5")]
        public string Pm25 { get; set; }
        //AQI
        public string Aqi { get; set; }
        //PM10
        public string Pm10 { get; set; }
        //CO
        public string Co{ get; set; }
        //NO2
        public string No2{ get; set; }
        //O3
        public string O3{ get; set; }
        //SO2
        public string So2 { get; set; }
        //发布时间
        public string Time { get; set; }

    }
}
