using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COVID19.Tracker.Core.Models.COVIDStats;
using COVID19.Tracker.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace COVID19.Tracker.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(IStatsService service, ILogger<IndexModel> logger)
        {
            this._service = service;
            _logger = logger;
        }
        private readonly ILogger<IndexModel> _logger;
        private readonly IStatsService _service;
        public DashboardViewModel ViewModel { get; set; }
        public class DashboardViewModel
        {
            public Stats Stats { get; set; }
            public Summary Summary { get; set; }
         
        }

        public async Task OnGet()
        {
            this.ViewModel = new DashboardViewModel();
            //ViewModel.Stats = await _service.GetStatsAsync("TT");
            ViewModel.Summary = await _service.GetStatsSummaryAsync(DateTime.Now.ToString("yyyy-MM-dd")+ "T00:00:00Z");
            var list = await _service.GetStatsAllSummaryAsync();
            ViewData["labelz"] = from i in list
                                    group i by i.DateVal.ToString("MMM") into grp
                                    select new { Month = grp.Key, Count = grp.Max(i => Convert.ToInt32(i.Active)).ToString()};
            ViewData["label"] = list.GroupBy(g => g.DateVal.ToString("MMM")).Select(grp => grp.Key).ToArray();
            ViewData["value"] = list.GroupBy(g => g.DateVal.ToString("MMM")).Select(grp => grp.Max(i => Convert.ToInt32(i.Active)).ToString()).ToArray();
            // var x = ViewModel.SummaryList.FirstOrDefault();


        }
    }
}
