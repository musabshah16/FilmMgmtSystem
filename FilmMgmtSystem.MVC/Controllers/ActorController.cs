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
    public class ActorController : Controller
    {
        // GET: Actor
        public ActorController()
        {

        }

        // display all Actor types
        public async Task<IActionResult> Index()
        {
            List<Actor> objActor = await GetActors();

            return View(objActor);
        }

        // display perticular Actor type details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var film = await GetActors(id);

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

        // enter new Actor type data
        public IActionResult Create()
        {
            return View();
        }

        // bind enter data to create new record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Actorid,FirstName,LastName")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await AddActor(actor);
                }
                catch (Exception)
                {
                    if (ActorExists(actor.Actorid) == null)
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

            return View(actor);
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
                var film = await GetActors(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Actorid,FirstName,LastName")] Actor actor)
        {
            if (id != actor.Actorid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await UpdateActor(actor);

                }
                catch (Exception)
                {
                    if (ActorExists(actor.Actorid) == null)
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

            return View(actor);
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
                Actor objActor = await GetActors(id);

                if (objActor == null)
                {
                    return NotFound();
                }

                return View(objActor);
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

            await DeleteActor(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<Actor> ActorExists(int id)
        {

            Actor objactor = await GetActors(id);
            return objactor;

        }




        //----------Separate function used by actions methods of our controller

        // get all car type data from server side
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
            catch (Exception)
            {
                throw;
            }
        }

        // get perticular Actor type from server side
        public async Task<Actor> GetActors(int? id)
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

                HttpResponseMessage response = await client.GetAsync("/api/ActorAPI/" + id);
                string stringData = response.Content.ReadAsStringAsync().Result;
                Actor data = JsonConvert.DeserializeObject<Actor>(stringData);

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
        public async Task<Actor> AddActor(Actor actor)
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

                string stringData = JsonConvert.SerializeObject(actor);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/ActorAPI/", contentData);

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
                return actor;
            }
            catch (Exception)
            {
                throw;
            }

        }

        // send edited data to server side
        public async Task<Actor> UpdateActor(Actor actor)
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

                string stringData = JsonConvert.SerializeObject(actor);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("api/ActorAPI/" + actor.Actorid, contentData);

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
                return actor;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // send request to server side to delete record
        public async Task<Actor> DeleteActor(int? id)
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

                HttpResponseMessage response = await client.DeleteAsync("/api/ActorAPI/" + id);
                string stringData = response.Content.
            ReadAsStringAsync().Result;
                Actor data = JsonConvert.DeserializeObject
            <Actor>(stringData);

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



