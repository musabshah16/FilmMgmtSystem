using FilmMgmtSystem.Entities;
using FilmMgmtSystem.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FilmMgmtSystem.MVC.Controllers
{
    public class LanguageController : Controller
    {
        // GET: CarTypesController
        public LanguageController()
        {

        }

        // display all car types
        public async Task<IActionResult> Index()
        {
            List<Language> objLanguage = await GetLanguage();

            return View(objLanguage);
        }

        // display perticular car type details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var film = await GetLanguage(id);

                if (film == null)
                {
                    return NotFound();
                }

                return View(film);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // enter new car type data
        public IActionResult Create()
        {
            return View();
        }

        // bind enter data to create new record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Languageid,Name")] Language language)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await AddLanguage(language);
                }
                catch (Exception)
                {
                    if (LanguageExists(language.Languageid) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(language);
        }

        // edit perticular data
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            try
            {
                var film = await GetLanguage(id);
                if (film == null)
                {
                    return NotFound();
                }

                return View(film);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //  bind enter data 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Languageid,Name")] Language language)
        {
            if (id != language.Languageid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await UpdateLanguage(language);

                }
                catch (Exception)
                {
                    if (LanguageExists(language.Languageid) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(language);
        }

        // delete perticular data
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                Language objLanguage = await GetLanguage(id);

                if (objLanguage == null)
                {
                    return NotFound();
                }

                return View(objLanguage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // this function give confirmation 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await DeleteLanguage(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<Language> LanguageExists(int id)
        {

            Language objlanguage = await GetLanguage(id);
            return objlanguage;

        }




        //----------Separate function used by actions methods of our controller

        // get all car type data from server side
        public async Task<List<Language>> GetLanguage()
        {
            try
            {
                string baseUrl = "https://localhost:44386";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync("/api/LanguageAPI");
                string stringData = response.Content.ReadAsStringAsync().Result;
                List<Language> data = JsonConvert.DeserializeObject<List<Language>>(stringData);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                else
                {
                    return data;
                }

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // get perticular car type from server side
        public async Task<Language> GetLanguage(int? id)
        {
            try
            {
                string baseUrl = "https://localhost:44386";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync("/api/LanguageAPI/" + id);
                string stringData = response.Content.ReadAsStringAsync().Result;
                Language data = JsonConvert.DeserializeObject<Language>(stringData);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                else
                {

                    return data;
                }

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // send enter data to server side
        public async Task<Language> AddLanguage(Language language)
        {
            try
            {
                string baseUrl = "https://localhost:44386";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
             //   var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
              //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string stringData = JsonConvert.SerializeObject(language);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/LanguageAPI/", contentData);

                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    //UrireturnUrl = response.Headers.Location;
                    //Console.WriteLine(returnUrl);
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                return language;
            }
            catch (Exception)
            {
                throw;
            }

        }

        // send edited data to server side
        public async Task<Language> UpdateLanguage(Language language)
        {
            try
            {
                string baseUrl = "https://localhost:44386";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue
            ("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
            //    var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string stringData = JsonConvert.SerializeObject(language);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("api/LanguageAPI/" + language.Languageid, contentData);

                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    //UrireturnUrl = response.Headers.Location;
                    //Console.WriteLine(returnUrl);
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                return language;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // send request to server side to delete record
        public async Task<Language> DeleteLanguage(int? id)
        {
            try
            {
                string baseUrl = "https://localhost:44386";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue
            ("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
            //    var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.DeleteAsync("/api/LanguageApi/" + id);
                string stringData = response.Content.
            ReadAsStringAsync().Result;
                Language data = JsonConvert.DeserializeObject
            <Language>(stringData);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                else
                {

                    return data;
                }

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
