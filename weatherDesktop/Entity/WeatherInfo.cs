using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace weatherDesktop.Entity
{
    public class WeatherInfo
    {
        public Info weatherinfo;
    }

    public class Info
    {
        //城市
        public string city { get; set; }
        //当前气温
        public string temp { get; set; }
        //风向
        public string wd { get; set; }
        //风速
        public string ws { get; set; }
        //湿度
        public string sd { get; set; }
        //发布时间
        public string time { get; set; }
        //预报温度高
        public string temp1 { get; set; }
        //预报温度低
        public string temp2 { get; set; }
        //预报天气
        public string weather { get; set; }
        //预报发布时间
        public string ptime { get; set; }

    }
}
