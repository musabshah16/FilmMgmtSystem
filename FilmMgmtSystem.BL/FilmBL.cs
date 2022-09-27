using FilmMgmtSystem.DAL;
using FilmMgmtSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmMgmtSystem.BL
{
    public class FilmBL
    {
        FilmDAL objFilmDAL = new FilmDAL();



        public void CreateFilm(Film objFilm)
        {
            objFilmDAL.CreateFilm(objFilm);
        }



        public void UpdateFilm(Film objFilm)
        {
            objFilmDAL.UpdateFilm(objFilm);
        }



        public void DeleteFilm(int id)
        {
            objFilmDAL.DeleteFilm(id);
        }



        public Film GetFilm(int id)
        {
            Film objFilm = objFilmDAL.GetFilm(id);
            return objFilm;
        }



        public Film SearchFilm(string Title)
        {
            Film objFilm = objFilmDAL.SearchFilm(Title);
            return objFilm;
        }



        //public IEnumerable<GetManufacturerTransmissionType> GetManufacturerTransmissionType()
        //{
        //    return objCarDAL.GetManufacturerTransmissionType();
        //}
        public IEnumerable<Film> GetFilm()
        {
            return objFilmDAL.GetFilm();
        }        
    }
}
