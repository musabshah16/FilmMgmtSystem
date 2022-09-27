using FilmMgmtSystem.EFCore;
using FilmMgmtSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmMgmtSystem.DAL
{
    public class CategoryDAL
    {
        FilmMgmtSystemContext objFilmMgmtSystemContext = new FilmMgmtSystemContext();

        public void CreateCategory(Category objCategory)
        {
            objFilmMgmtSystemContext.Add(objCategory);
            objFilmMgmtSystemContext.SaveChanges();
        }

        public void UpdateCategory(Category objCategory)
        {
            objFilmMgmtSystemContext.Entry(objCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            objFilmMgmtSystemContext.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            Category objCategory = objFilmMgmtSystemContext.Categories.Find(id);
            objFilmMgmtSystemContext.Remove(objCategory);
            objFilmMgmtSystemContext.SaveChanges();
        }

        public Category GetCategory(int id)
        {
            Category objCategory = objFilmMgmtSystemContext.Categories.Find(id);
            return objCategory;
        }

        //public Category SearchCategory(string model)
        //{
        //    IEnumerable<Category> objCategory = objFilmMgmtSystemContext.Categories.Where(a => a.Model.Contains(model));
        //    //IEnumerable<Car> objCar = objCarInformationContext.Cars;
        //    //objCar = objCar.Where(a => a.Model.Contains(model)).ToList();

        //    return objCategory.FirstOrDefault();
        //}


        public IEnumerable<Category> GetCategory()
        {
            return objFilmMgmtSystemContext.Categories;
        }


    }
}