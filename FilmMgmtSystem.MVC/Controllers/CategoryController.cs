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
    public class CategoryController : Controller
    {
        // GET: CarTypesController
        public CategoryController()
        {

        }

        // display all car types
        public async Task<IActionResult> Index()
        {
            List<Category> objCategory = await GetCategory();

            return View(objCategory);
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
                var film = await GetCategory(id);

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
        public async Task<IActionResult> Create([Bind("Categoryid,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await AddCategory(category);
                }
                catch (Exception)
                {
                    if (CategoryExists(category.Categoryid) == null)
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

            return View(category);
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
                var film = await GetCategory(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Categoryid,Name")] Category category)
        {
            if (id != category.Categoryid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await UpdateCategory(category);

                }
                catch (Exception)
                {
                    if (CategoryExists(category.Categoryid) == null)
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

            return View(category);
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
                Category objCategory = await GetCategory(id);

                if (objCategory == null)
                {
                    return NotFound();
                }

                return View(objCategory);
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

            await DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<Category> CategoryExists(int id)
        {

            Category objcategory = await GetCategory(id);
            return objcategory;

        }




        //----------Separate function used by actions methods of our controller

        // get all car type data from server side
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
            catch (Exception)
            {
                throw;
            }
        }

        // get perticular car type from server side
        public async Task<Category> GetCategory(int? id)
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

                HttpResponseMessage response = await client.GetAsync("/api/CategoryAPI/" + id);
                string stringData = response.Content.ReadAsStringAsync().Result;
                Category data = JsonConvert.DeserializeObject<Category>(stringData);

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
        public async Task<Category> AddCategory(Category category)
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

                string stringData = JsonConvert.SerializeObject(category);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/CategoryAPI/", contentData);

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
                return category;
            }
            catch (Exception)
            {
                throw;
            }

        }

        // send edited data to server side
        public async Task<Category> UpdateCategory(Category category)
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

                string stringData = JsonConvert.SerializeObject(category);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("api/CategoryAPI/" + category.Categoryid, contentData);

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
                return category;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // send request to server side to delete record
        public async Task<Category> DeleteCategory(int? id)
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

                HttpResponseMessage response = await client.DeleteAsync("/api/CategoryAPI/" + id);
                string stringData = response.Content.
            ReadAsStringAsync().Result;
                Category data = JsonConvert.DeserializeObject
            <Category>(stringData);

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
