using ApplicationService.DTOs;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Website.Utils;
using Website.ViewModels;

namespace Website.Controllers
{
    public class CharacterController : Controller
    {
        private readonly Uri url = new Uri("https://localhost:44377/api/character/");
        private static async Task<string> GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44377");

                // We want the response to be JSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Build up the data to POST.
                List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "pavel"),
                    new KeyValuePair<string, string>("password", "123123")
                };

                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);

                // Post to the Server and parse the response.
                HttpResponseMessage response = await client.PostAsync("Token", content);
                string jsonString = await response.Content.ReadAsStringAsync();
                object responseData = JsonConvert.DeserializeObject(jsonString);

                // return the Access Token.
                return ((dynamic)responseData).access_token;
            }
        }

        // GET: Genres
        public async Task<ActionResult> Index(string searchString, string currentFilter, int? page)
        {
            //string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Add the Authorization header with the AccessToken.+ accessToken
                client.DefaultRequestHeaders.Add("Authorization", "Bearer ");

                // make the request
                HttpResponseMessage response = await client.GetAsync("");

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<CharacterVM>>(jsonString);

                page = 1;
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                responseData = responseData.ToList();

                return View(responseData.ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: Genres/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Add the Authorization header with the AccessToken.+ accessToken
                client.DefaultRequestHeaders.Add("Authorization", "Bearer ");

                // make the request
                HttpResponseMessage response = await client.GetAsync("getcharacterbyid?id=" + id);

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<CharacterVM>(jsonString);
                return View(responseData);
            }
        }

        private static Uri url2 = new Uri("https://localhost:44377/api/comics/");
        private static Uri url3 = new Uri("https://localhost:44377/api/series/");

        // GET: Genres/Create
        public ActionResult Create()
        {
            ViewBag.Series = LoadDataUtil.LoadSeriesData();
            ViewBag.Comics = LoadDataUtil.LoadComicsData();
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        public async Task<ActionResult> Create(CharacterVM characterVM)
        {
            if (!ModelState.IsValid)
                return View(characterVM);
            try
            {
                //string accessToken = await GetAccessToken();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Add the Authorization header with the AccessToken. + accessToken
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer ");


                    var clientloc = new WebClient();
                    var body = "";

                    body = clientloc.DownloadString(url2 + "getcomicsbyid?id=" + characterVM.ComicsId);
                    var responseDataloc = JsonConvert.DeserializeObject<ComicsDTO>(body);

                    characterVM.Comics = new ComicsVM(responseDataloc);

                    var clientser = new WebClient();
                    var bodys = "";

                    bodys = clientloc.DownloadString(url3 + "getseriesbyid?id=" + characterVM.SeriesId);
                    var responseDataser = JsonConvert.DeserializeObject<SeriesDTO>(bodys);

                    characterVM.Series = new SeriesVM(responseDataser);
                    characterVM.Series.Comics = new ComicsVM(responseDataloc);

                    var content = JsonConvert.SerializeObject(characterVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    

                    // make the request
                    HttpResponseMessage response = await client.PostAsync("save", byteContent);

                    // parse the response and return the data.
                    //string jsonString = await response.Content.ReadAsStringAsync();
                    //var responseData = JsonConvert.DeserializeObject<GenreVM>(jsonString);
                    //return View(responseData);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Series = LoadDataUtil.LoadSeriesData();
                ViewBag.Comics = LoadDataUtil.LoadComicsData();
                return View();
            }
        }

        // GET: Genres/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Add the Authorization header with the AccessToken. + accessToken
                client.DefaultRequestHeaders.Add("Authorization", "Bearer ");

                // make the request
                HttpResponseMessage response = await client.GetAsync("getcharacterbyid?id=" + id);

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<CharacterVM>(jsonString);

                ViewBag.Series = LoadDataUtil.LoadSeriesData();
                ViewBag.Comics = LoadDataUtil.LoadComicsData();

                return View(responseData);
            }
        }

        // POST: Genres/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(CharacterVM characterVM)
        {
            if (!ModelState.IsValid)
                return View(characterVM);
            try
            {
                //string accessToken = await GetAccessToken();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Add the Authorization header with the AccessToken.+ accessToken
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer ");

                    var clientloc = new WebClient();
                    var body = "";

                    body = clientloc.DownloadString(url2 + "getcomicsbyid?id=" + characterVM.ComicsId);
                    var responseDataloc = JsonConvert.DeserializeObject<ComicsDTO>(body);

                    characterVM.Comics = new ComicsVM(responseDataloc);

                    var clientser = new WebClient();
                    var bodys = "";

                    bodys = clientloc.DownloadString(url3 + "getseriesbyid?id=" + characterVM.SeriesId);
                    var responseDataser = JsonConvert.DeserializeObject<SeriesDTO>(bodys);

                    characterVM.Series = new SeriesVM(responseDataser);
                    characterVM.Series.Comics = new ComicsVM(responseDataloc);

                    var content = JsonConvert.SerializeObject(characterVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // make the request
                    HttpResponseMessage response = await client.PostAsync("save", byteContent);

                    // parse the response and return the data.
                    //string jsonString = await response.Content.ReadAsStringAsync();
                    //var responseData = JsonConvert.DeserializeObject<GenreVM>(jsonString);
                    //return View(responseData);
                }

                ViewBag.Series = LoadDataUtil.LoadSeriesData();
                ViewBag.Comics = LoadDataUtil.LoadComicsData();

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Series = LoadDataUtil.LoadSeriesData();
                ViewBag.Comics = LoadDataUtil.LoadComicsData();
                return View();
            }
        }

        // GET: Genres/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            //string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Add the Authorization header with the AccessToken.+ accessToken
                client.DefaultRequestHeaders.Add("Authorization", "Bearer ");

                // make the request
                HttpResponseMessage response = await client.GetAsync("getcharacterbyid?id=" + id);

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<CharacterVM>(jsonString);
                return View(responseData);
            }
        }

        // POST: Genres/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                // string accessToken = await GetAccessToken();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Add the Authorization header with the AccessToken. + accessToken
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer ");

                    // make the request
                    HttpResponseMessage response = await client.DeleteAsync("delete?id=" + id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}