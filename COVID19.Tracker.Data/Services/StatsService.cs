using COVID19.Tracker.Core.Models.COVIDStats;
using COVID19.Tracker.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace COVID19.Tracker.Data.Services
{
    public class StatsService : IStatsService
    {
        public async Task<Stats> GetStatsAsync(string code)
        {
            var apiResult = new Dictionary<string, Stats>();
            var API_URL = Data.Constants.API.BaseURL;
            using (var client = new HttpClient())
            {
                string url = $"{API_URL}data.min.json";
                var response = client.GetAsync(url).Result;
                string responseAsString = await response.Content.ReadAsStringAsync();
                apiResult = JsonConvert.DeserializeObject<Dictionary<string, Stats>>(responseAsString);
            }
            var data = apiResult.Where(a=>a.Key == code).FirstOrDefault().Value;
            return data;
        }

        public async Task<Summary> GetStatsSummaryAsync(string code)
        {
            var apiResult = new List<Summary>();
            var API_URL = Data.Constants.API.BaseURLFj;
            using (var client = new HttpClient())
            {
                string url = $"{API_URL}country/fiji";
                var response = client.GetAsync(url).Result;
                string responseAsString = await response.Content.ReadAsStringAsync();
                apiResult = JsonConvert.DeserializeObject<List<Summary>>(responseAsString);
            }
           
            var data = apiResult.Where(a => a.Date == code).FirstOrDefault();
            if(data == null)
            {
                 data = apiResult.Where(a => a.Date == (DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd")+ "T00:00:00Z")).FirstOrDefault();
            }
            return data;
        }

        public async Task<List<Summary>> GetStatsAllSummaryAsync()
        {
            var apiResult = new List<Summary>();
            var API_URL = Data.Constants.API.BaseURLFj;
            using (var client = new HttpClient())
            {
                string url = $"{API_URL}country/fiji";
                var response = client.GetAsync(url).Result;
                string responseAsString = await response.Content.ReadAsStringAsync();
                apiResult = JsonConvert.DeserializeObject<List<Summary>>(responseAsString);
            }

            var data = apiResult.Where(a => a.DateVal.Year == DateTime.Now.Year).ToList();
            //if (data == null)
            //{
            //    data = apiResult.Where(a => a.Date == (DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + "T00:00:00Z")).FirstOrDefault();
            //}
            return data;
        }

        public async Task<Stats> GetStatsByDateAsync(string code, DateTime dateTime)
        {
            var apiResult = new Dictionary<string, Stats>();
            var API_URL = Data.Constants.API.BaseURL;
            using (var client = new HttpClient())
            {
                var dateTimeString = dateTime.ToString("yyyy-MM-dd");
                string url = $"{API_URL}data-{dateTimeString}.min.json";
                var response = client.GetAsync(url).Result;
                string responseAsString = await response.Content.ReadAsStringAsync();
                apiResult = JsonConvert.DeserializeObject<Dictionary<string, Stats>>(responseAsString);
            }
            var data = apiResult.Where(a => a.Key == code).FirstOrDefault().Value;
            return data;
        }
    }
}
