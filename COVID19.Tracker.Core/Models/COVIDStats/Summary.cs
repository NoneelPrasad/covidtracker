using System;
using System.Collections.Generic;
using System.Text;

namespace COVID19.Tracker.Core.Models.COVIDStats
{
    public class Summary
    {
        public string ID { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Confirmed { get; set; }
        public string Deaths { get; set; }
        public string Recovered { get; set; }
        public string Active { get; set; }
        public string Date { get; set; }
        public DateTime DateVal
        {
            get
            {
                return DateTime.ParseExact(Date.Substring(0, Date.Length - 10), "yyyy-MM-dd", null); 
            }
            
        }
    }
}
