using FilmMgmtSystem.EFCore;
using FilmMgmtSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmMgmtSystem.DAL
{
    namespace FilmMgmtSystem.DAL
    {
        public class LanguageDAL
        {
            FilmMgmtSystemContext objFilmMgmtSystemContext = new FilmMgmtSystemContext();



            public void CreateLanguage(Language objLanguage)
            {
                objFilmMgmtSystemContext.Add(objLanguage);
                objFilmMgmtSystemContext.SaveChanges();
            }



            public void UpdateLanguage(Language objLanguage)
            {
                objFilmMgmtSystemContext.Entry(objLanguage).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                objFilmMgmtSystemContext.SaveChanges();
            }



            public void DeleteLanguage(int id)
            {
                Language objLanguage = objFilmMgmtSystemContext.Languages.Find(id);
                objFilmMgmtSystemContext.Remove(objLanguage);
                objFilmMgmtSystemContext.SaveChanges();
            }



            public Language GetLanguage(int id)
            {
                Language objLanguage = objFilmMgmtSystemContext.Languages.Find(id);
                return objLanguage;
            }



            //public Language SearchLanguage(string model)
            //{
            //    IEnumerable<Language> objLanguage = objFilmMgmtSystemContext.Films.Where(a => a.Model.Contains(model));
            //    return objLanguage.FirstOrDefault();
            //}




            public IEnumerable<Language> GetLanguage()
            {
                return objFilmMgmtSystemContext.Languages;
            }
        }
    }
}
