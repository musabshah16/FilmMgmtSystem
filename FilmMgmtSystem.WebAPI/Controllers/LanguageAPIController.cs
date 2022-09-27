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

namespace LanguageMgmtSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageAPIController : ControllerBase
    {
        // GET: api/<LanguageWebApiController>
        private readonly LanguageBL languageBL = new LanguageBL();
        private readonly FilmMgmtSystemContext _context = new FilmMgmtSystemContext();

        public LanguageAPIController()
        {

        }

        // Get All Film details

        [HttpGet]
        public ActionResult<IEnumerable<Language>> GetLanguages()
        {
            return new ActionResult<IEnumerable<Language>>(languageBL.GetLanguage());

        }



        // Get Perticular Language details by Using id
        [HttpGet("{id}")]
        public ActionResult<Language> GetLanguage(int id)
        {
            try
            {
                var language = languageBL.GetLanguage(id);


                if (language == null)
                {
                    return NotFound();
                }

                return language;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Search Perticular Film Details by Using Model
        //[HttpGet]
        //[Route("Model")]
        //public ActionResult<Language> SearchLanguage(string model)
        //{
        //    var language = languageBL.SearchLanguage(model);


        //    if (language == null)
        //    {
        //        return NotFound();
        //    }

        //    return language;
        //}


        //Edit Perticular Language details
        [HttpPut("{id}")]
        //[Authorize(Roles ="Admin")]
        public IActionResult PutLanguage(int id, Language language)
        {
            if (id != language.Languageid)
            {
                return BadRequest();
            }

            try
            {

                languageBL.UpdateLanguage(language);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageExists(id))
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

        //Create New Language Details
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public ActionResult<Language> PostLanguage(Language language)
        {


            try
            {

                languageBL.CreateLanguage(language);

            }
            catch (DbUpdateException)
            {
                if (LanguageExists(language.Languageid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getfilm", new { id = language.Languageid }, language);
        }

        // Delete Perticular Film details 
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult<Language> Deletelanguage(int id)
        {

            var language = languageBL.GetLanguage(id);



            try
            {
                languageBL.DeleteLanguage(language.Languageid);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (language == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return language;
        }

        // Check Language Id Exists or Not
        private bool LanguageExists(int id)
        {


            if (languageBL.GetLanguage(id) != null)
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