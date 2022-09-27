//using FilmMgmtSystem.BAL;
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
    public class ActorAPIController : ControllerBase
    {
        // GET: api/<CarWebApiController>
        private readonly ActorBL actorBL = new ActorBL();
        private readonly FilmMgmtSystemContext _context = new FilmMgmtSystemContext();

        public ActorAPIController()
        {

        }

        // Get All Cars details

        [HttpGet]
        public ActionResult<IEnumerable<Actor>> GetActors()
        {
            return new ActionResult<IEnumerable<Actor>>(actorBL.GetActor());

        }



        // Get Perticular Car details by Using id
        [HttpGet("{id}")]
        public ActionResult<Actor> GeActor(int id)
        {
            try
            {
                var actor = actorBL.GetActor(id);


                if (actor == null)
                {
                    return NotFound();
                }

                return actor;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Search Perticular Car Details by Using Model
        //[HttpGet]
        //[Route("Model")]
        //public ActionResult<Actor> SearchActor(string model)
        //{
        //    var actor = actorBL.SearchActor(model);


        //    if (actor == null)
        //    {
        //        return NotFound();
        //    }

        //    return actor;
        //}


        //Edit Perticular Film details
        [HttpPut("{id}")]
        //[Authorize(Roles ="Admin")]
        public IActionResult PutActor(int id, Actor actor)
        {
            if (id != actor.Actorid)
            {
                return BadRequest();
            }

            try
            {

                actorBL.UpdateActor(actor);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
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

        //Create New Car Details
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public ActionResult<Actor> PostActor(Actor actor)
        {


            try
            {

                actorBL.CreateActor(actor);

            }
            catch (DbUpdateException)
            {
                if (ActorExists(actor.Actorid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getactor", new { id = actor.Actorid }, actor);
        }

        // Delete Perticular Car details 
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult<Actor> Deleteactor(int id)
        {

            var actor = actorBL.GetActor(id);



            try
            {
                actorBL.DeleteActor(actor.Actorid);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (actor == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return actor;
        }

        // Check Car Id Exists or Not
        private bool ActorExists(int id)
        {


            if (actorBL.GetActor(id) != null)
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

