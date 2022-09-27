using FilmMgmtSystem.EFCore;
using FilmMgmtSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmMgmtSystem.DAL
{
    public class FilmDAL
    {
        FilmMgmtSystemContext objFilmMgmtSystemContext = new FilmMgmtSystemContext();



        public void CreateFilm(Film objFilm)
        {
            objFilmMgmtSystemContext.Add(objFilm);
            objFilmMgmtSystemContext.SaveChanges();
        }



        public void UpdateFilm(Film objFilm)
        {
            objFilmMgmtSystemContext.Entry(objFilm).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            objFilmMgmtSystemContext.SaveChanges();
        }



        public void DeleteFilm(int id)
        {
            Film objFilm = objFilmMgmtSystemContext.Films.Find(id);
            objFilmMgmtSystemContext.Remove(objFilm);
            objFilmMgmtSystemContext.SaveChanges();
        }



        public Film GetFilm(int id)
        {
            Film objFilm = objFilmMgmtSystemContext.Films.Find(id);
            return objFilm;
        }



        public Film SearchFilm(string Title)
        {
           IEnumerable<Film> objFilm = objFilmMgmtSystemContext.Films.Where(a => a.Title.Contains(Title));            
           return objFilm.FirstOrDefault();
        }




        public IEnumerable<Film> GetFilm()
        {
            return objFilmMgmtSystemContext.Films;
        }
    }
}



        