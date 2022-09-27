using FilmMgmtSystem.DAL;
using FilmMgmtSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmMgmtSystem.BL
{
   
    public class CategoryBL
    {
        CategoryDAL objCategoryDAL = new CategoryDAL();



        public void CreateCategory(Category objCategory)
        {
            objCategoryDAL.CreateCategory(objCategory);
        }



        public void UpdateCategory(Category objCategory)
        {
            objCategoryDAL.UpdateCategory(objCategory);
        }



        public void DeleteCategory(int id)
        {
            objCategoryDAL.DeleteCategory(id);
        }



        public Category GetCategory(int id)
        {
            Category objCategory = objCategoryDAL.GetCategory(id);
            return objCategory;
        }



        //public Category SearchCategory(string model)
        //{
        //    Category objCategory = objCategoryDAL.SearchCategory(model);
        //    return objCategory;
        //}



        //public IEnumerable<GetManufacturerTransmissionType> GetManufacturerTransmissionType()
        //{
        //    return objCarDAL.GetManufacturerTransmissionType();
        //}
        public IEnumerable<Category> GetCategory()
        {
            return objCategoryDAL.GetCategory();
        }
    }
}