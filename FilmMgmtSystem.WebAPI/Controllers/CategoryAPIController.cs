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

namespace CategoryMgmtSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        // GET: api/<CarWebApiController>
        private readonly CategoryBL categoryBL = new CategoryBL();
        private readonly FilmMgmtSystemContext _context = new FilmMgmtSystemContext();

        public CategoryAPIController()
        {

        }

        // Get All Cars details

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            return new ActionResult<IEnumerable<Category>>(categoryBL.GetCategory());

        }



        // Get Perticular Car details by Using id
        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            try
            {
                var category = categoryBL.GetCategory(id);


                if (category == null)
                {
                    return NotFound();
                }

                return category;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Search Perticular Car Details by Using Model
        //[HttpGet]
        //[Route("Model")]
        //public ActionResult<Category> SearchCategory(string model)
        //{
        //    var car = categoryBL.SearchCategory(model);


        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return category;
        //}


        //Edit Perticular Car details
        [HttpPut("{id}")]
        //[Authorize(Roles ="Admin")]
        public IActionResult PutCategory(int id, Category category)
        {
            if (id != category.Categoryid)
            {
                return BadRequest();
            }

            try
            {

                categoryBL.UpdateCategory(category);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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
        public ActionResult<Category> PostCategory(Category category)
        {


            try
            {

                categoryBL.CreateCategory(category);

            }
            catch (DbUpdateException)
            {
                if (CategoryExists(category.Categoryid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getcategory", new { id = category.Categoryid }, category);
        }

        // Delete Perticular Car details 
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult<Category> Deletecategory(int id)
        {

            var category = categoryBL.GetCategory(id);



            try
            {
                categoryBL.DeleteCategory(category.Categoryid);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return category;
        }

        // Check Car Id Exists or Not
        private bool CategoryExists(int id)
        {


            if (categoryBL.GetCategory(id) != null)
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