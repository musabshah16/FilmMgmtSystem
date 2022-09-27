using FilmMgmtSystem.EFCore;
using FilmMgmtSystem.Entities;
using FilmMgmtSystem.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FilmMgmtSystem.MVC.Controllers
{
    public class FilmController : Controller
    {
        private readonly FilmMgmtSystemContext _context = new FilmMgmtSystemContext();
        public FilmController()
        {

        }

        // Get all Film details and display

        public async Task<IActionResult> Index()
        {
            List<Film> objFilm = await GetFilms();

            return View(objFilm);
        }



        // Get perticular Car details and display
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await GetFilms(id);

            if (film == null)
            {
                return NotFound();
            }


            return View(film);
        }

        // Search perticular Film by using model and display
        public async Task<IActionResult> SearchFilm(string title)
        {
           if (title == null)
            {
                return NotFound();
            }

           var film = await SearchFilms(title);

           if (film == null)
            {
                return NotFound();
           }

            return View(film);
        }

        // open form for create new car details
        public IActionResult Create()
        {
            Task<List<Actor>> taskActor = GetActors();
            Task<List<Category>> taskCategory = GetCategory();
            Task<List<Language>> taskLanguage = GetLanguage();
            List<Category> category = taskCategory.Result;
            List<Actor> actor = taskActor.Result;
            List<Language> language = taskLanguage.Result;

            ViewData["ActorId"] = new SelectList(actor, "Actorid", "FirstName", "LastName");
            ViewData["CategoryId"] = new SelectList(category, "Categoryid", "Name");
            ViewData["LanguageId"] = new SelectList(language, "Languageid", "Name");
            return View();

        }

        // binding enter data in form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Filmid,FilmDescription,Title,Releaseyear,TypeId,Languageid,Rentalduration,FilmLength,Replacementcost,Rating,Specialfeatures,Actorid,Categoryid")] Film film)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await AddFilms(film);
                }
                catch (Exception)
                {
                    if (FilmExists(film.Filmid) == null)
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


            return View(film);
        }

        // Edit Perticular Car details by using id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var film = await GetFilms(id);
            if (film == null)
            {
                return NotFound();
            }
            Task<List<Actor>> taskActor = GetActors();
            Task<List<Category>> taskCategory = GetCategory();
            Task<List<Language>> taskLanguage = GetLanguage();
            List<Category> category = taskCategory.Result;
            List<Actor> actor = taskActor.Result;
            List<Language> language = taskLanguage.Result;

            ViewData["ActorId"] = new SelectList(actor, "Actorid", "FirstName", "LastName");
            ViewData["CategoryId"] = new SelectList(category, "Categoryid", "Name");
            ViewData["LanguageId"] = new SelectList(language, "Languageid", "Name");
            return View(film);
        }

        // Binding new Data of car
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Filmid,FilmDescription,Title,Releaseyear,TypeId,Languageid,Rentalduration,FilmLength,Replacementcost,Rating,Specialfeatures,Actorid,Categoryid")] Film film)
        {
            if (id != film.Filmid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await UpdateFilm(film);

                }
                catch (Exception)
                {
                    if (FilmExists(film.Filmid) == null)
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

            return View(film);
        }

        // Delete perticular car by using id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                Film objFilm = await GetFilms(id);

                if (objFilm == null)
                {
                    return NotFound();
                }

                return View(objFilm);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // this function give confirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await DeleteFilm(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<Film> FilmExists(int id)
        {

            Film objfilm = await GetFilms(id);
            return objfilm;

        }

        // get car details from server side
        public async Task<List<Film>> GetFilms()
        {
            string baseUrl = "https://localhost:44386";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            HttpResponseMessage response = await client.GetAsync("/api/FilmAPI");

            string stringData = response.Content.ReadAsStringAsync().Result;
            List<Film> data = JsonConvert.DeserializeObject<List<Film>>(stringData);

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

        // get perticular car details from server side
        public async Task<Film> GetFilms(int? id)
        {
            try
            {
                string baseUrl = "https://localhost:44386";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync("/api/FilmAPI/" + id);
                string stringData = response.Content.ReadAsStringAsync().Result;
                Film data = JsonConvert.DeserializeObject<Film>(stringData);
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

        // search perticular car details from server side
        public async Task<Film> SearchFilms(string title)
        {

            string baseUrl = "https://localhost:44386";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            HttpResponseMessage response = await client.GetAsync("/api/FilmAPI/title?Title=" + title);
            string stringData = response.Content.ReadAsStringAsync().Result;
            Film data = JsonConvert.DeserializeObject<Film>(stringData);

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

        // Add new car details 
        public async Task<Film> AddFilms(Film film)
        {
            try
            {
                string baseUrl = "https://localhost:44386";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue
            ("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string stringData = JsonConvert.SerializeObject(film);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/FilmAPI/", contentData);

                if (response.IsSuccessStatusCode)
                {

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";

                }

                return film;
            }
            catch (Exception)
            {
                return null;
            }

        }

        // Edit Perticular Film datails
        public async Task<Film> UpdateFilm(Film film)
        {
            try
            {
                string baseUrl = "https://localhost:44386";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue
            ("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string stringData = JsonConvert.SerializeObject(film);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("api/FilmAPI/" + film.Filmid, contentData);

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
                return film;
            }
            catch
            {
                return null;

            }
        }

        // delete perticular Car Details 
        public async Task<Film> DeleteFilm(int? id)
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

                HttpResponseMessage response = await client.DeleteAsync("/api/FilmAPI/" + id);
                string stringData = response.Content.
            ReadAsStringAsync().Result;
                Film data = JsonConvert.DeserializeObject<Film>(stringData);

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
            catch
            {
                return null;
            }
        }

        // get all car type details from server side
        public async Task<List<Actor>> GetActors()
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

                HttpResponseMessage response = await client.GetAsync("/api/ActorAPI");
                string stringData = response.Content.ReadAsStringAsync().Result;
                List<Actor> data = JsonConvert.DeserializeObject<List<Actor>>(stringData);

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
            catch
            {
                throw;
            }
        }

        // get all car transmission details from server side
        public async Task<List<Category>> GetCategory()
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

                HttpResponseMessage response = await client.GetAsync("/api/CategoryAPI");
                string stringData = response.Content.ReadAsStringAsync().Result;
                List<Category> data = JsonConvert.DeserializeObject<List<Category>>(stringData);

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
            catch
            {
                return null;
            }
        }

        // get all car manufacturer details from server side
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
            catch
            {
                return null;
            }
        }

    }
}