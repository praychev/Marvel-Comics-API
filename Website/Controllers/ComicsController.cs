using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Website.Utils;
using Website.ViewModels;
using PagedList;

namespace Website.Controllers
{
    public class ComicsController : Controller
    {
        // GET: Comics
        private readonly Uri url = new Uri("https://localhost:44377/api/comics/");

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
                    new KeyValuePair<string, string>("username", "pepi"),
                    new KeyValuePair<string, string>("password", "181218")
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

        // GET: Comics
        public async Task<ActionResult> Index(string searchString, string currentFilter, int? page)
        {
          // string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Add the Authorization header with the AccessToken.  + accessToken
                client.DefaultRequestHeaders.Add("Authorization", "Bearer ");

                // make the request
                HttpResponseMessage response = await client.GetAsync("");

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<ComicsVM>>(jsonString);

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                if (!String.IsNullOrEmpty(searchString))
                {
                    responseData = responseData.Where( c => c.Title.Contains(searchString)).ToList();
                }

                int pageSize = 3;
                int pageNumber = (page ?? 1);
                responseData = responseData.ToList();

                return View(responseData.ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: Comics/Details/5
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
                HttpResponseMessage response = await client.GetAsync("getcomicsbyid?id=" + id);

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ComicsVM>(jsonString);
                return View(responseData);
            }
        }

        // GET: Comics/Create
        public ActionResult Create()
        {
            ViewBag.Comics = LoadDataUtil.LoadComicsData();
            return View();
        }

        // POST: Comics/Create
        [HttpPost]
        public async Task<ActionResult> Create(ComicsVM comicsVM)
        {

            if (!ModelState.IsValid)
                return View(comicsVM);
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

                    var content = JsonConvert.SerializeObject(comicsVM);
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
                ViewBag.Comics = LoadDataUtil.LoadComicsData();
                return View();
            }
        }

        // GET: Comics/Edit/5
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
                HttpResponseMessage response = await client.GetAsync("getcomicsbyid?id=" + id);

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ComicsVM>(jsonString);
                return View(responseData);
            }
        }

        // POST: Comics/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(ComicsVM comicsVM)
        {

            if (!ModelState.IsValid)
                return View(comicsVM);
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

                    var content = JsonConvert.SerializeObject(comicsVM);
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
                return View();
            }
        }

        // GET: Comics/Delete/5
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
                HttpResponseMessage response = await client.GetAsync("getcomicsbyid?id=" + id);

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ComicsVM>(jsonString);
                return View(responseData);
            }
        }

        // POST: Comics/Delete/5
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