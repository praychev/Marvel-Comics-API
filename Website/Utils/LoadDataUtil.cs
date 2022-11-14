using ApplicationService.Implementations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Website.ViewModels;

namespace Website.Utils
{
    public class LoadDataUtil
    {
        private static Uri url = new Uri("https://localhost:44377/api/comics/");
        public static SelectList LoadComicsData()
        {
            var client = new WebClient();
            var body = "";

            body = client.DownloadString(url);
            var responseData = JsonConvert.DeserializeObject<List<ComicsVM>>(body);
            return new SelectList(responseData, "idC", "Title");
        }
        private static Uri url2 = new Uri("https://localhost:44377/api/series/");
        public static SelectList LoadSeriesData()
        {
            var client = new WebClient();
            var body = "";

            body = client.DownloadString(url2);
            var responseData = JsonConvert.DeserializeObject<List<SeriesVM>>(body);
            return new SelectList(responseData, "id", "Title");
        }
    }
}