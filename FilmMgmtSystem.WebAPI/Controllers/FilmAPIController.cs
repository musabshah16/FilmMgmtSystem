using FilmMgmtSystem.BL;
using FilmMgmtSystem.EFCore;
using FilmMgmtSystem.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FilmMgmtSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmAPIController : ControllerBase
    {
        // GET: api/<FilmWebApiController>
        private readonly FilmBL filmBL = new FilmBL();
        private readonly FilmMgmtSystemContext _context = new FilmMgmtSystemContext();

        public FilmAPIController()
        {

        }

        // Get All Film details

        [HttpGet]
        public ActionResult<IEnumerable<Film>> GetFilms()
        {
            return new ActionResult<IEnumerable<Film>>(filmBL.GetFilm());

        }



        // Get Perticular Film details by Using id
        [HttpGet("{id}")]
        public ActionResult<Film> GetFilm(int id)
        {
            try
            {
                var film = filmBL.GetFilm(id);


                if (film == null)
                {
                    return NotFound();
                }

                return film;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Search Perticular Film Details by Using Model
        [HttpGet]
        [Route("title")]
        public ActionResult<Film> SearchFilm(string Title)
        {
            var film = filmBL.SearchFilm(Title);


            if (film == null)
            {
                return NotFound();
            }

            return film;
        }


        //Edit Perticular Film details
        [HttpPut("{id}")]
        //[Authorize(Roles ="Admin")]
        public IActionResult PutFilm(int id, Film film)
        {
            if (id != film.Filmid)
            {
                return BadRequest();
            }

            try
            {

                filmBL.UpdateFilm(film);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //Create New Film Details
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public ActionResult<Film> PostFilm(Film film)
        {


            try
            {

                filmBL.CreateFilm(film);

            }
            catch (DbUpdateException)
            {
                if (FilmExists(film.Filmid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getfilm", new { id = film.Filmid }, film);
        }

        // Delete Perticular Film details 
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult<Film> Deletefilm(int id)
        {

            var film = filmBL.GetFilm(id);



            try
            {
                filmBL.DeleteFilm(film.Filmid);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (film == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return film;
        }

        // Check Film Id Exists or Not
        private bool FilmExists(int id)
        {


            if (filmBL.GetFilm(id) != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}