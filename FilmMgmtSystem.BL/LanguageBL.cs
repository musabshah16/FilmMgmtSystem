using FilmMgmtSystem.DAL.FilmMgmtSystem.DAL;
using FilmMgmtSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmMgmtSystem.BL
{
    public class LanguageBL
    {
        LanguageDAL objLanguageDAL = new LanguageDAL();



        public void CreateLanguage(Language objLanguage)
        {
            objLanguageDAL.CreateLanguage(objLanguage);
        }



        public void UpdateLanguage(Language objLanguage)
        {
            objLanguageDAL.UpdateLanguage(objLanguage);
        }



        public void DeleteLanguage(int id)
        {
            objLanguageDAL.DeleteLanguage(id);
        }



        public Language GetLanguage(int id)
        {
            Language objLanguage = objLanguageDAL.GetLanguage(id);
            return objLanguage;
        }



        //public Language SearchLanguage(string model)
        //{
        //    Language objLanguage = objLanguageDAL.SearchLanguage(model);
        //    return objLanguage;
        //}



        //public IEnumerable<GetManufacturerTransmissionType> GetManufacturerTransmissionType()
        //{
        //    return objCarDAL.GetManufacturerTransmissionType();
        //}
        public IEnumerable<Language> GetLanguage()
        {
            return objLanguageDAL.GetLanguage();
        }
    }
}
