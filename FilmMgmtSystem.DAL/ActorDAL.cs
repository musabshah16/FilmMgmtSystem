using FilmMgmtSystem.EFCore;
using FilmMgmtSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmMgmtSystem.DAL
{
    public class ActorDAL
    {
        FilmMgmtSystemContext objFilmMgmtSystemContext = new FilmMgmtSystemContext();

        public void CreateActor(Actor objActor)
        {
            objFilmMgmtSystemContext.Add(objActor);
            objFilmMgmtSystemContext.SaveChanges();
        }

        public void UpdateActor(Actor objActor)
        {
            objFilmMgmtSystemContext.Entry(objActor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            objFilmMgmtSystemContext.SaveChanges();
        }

        public void DeleteActor(int id)
        {
            Actor objActor = objFilmMgmtSystemContext.Actors.Find(id);
            objFilmMgmtSystemContext.Remove(objActor);
            objFilmMgmtSystemContext.SaveChanges();
        }

        public Actor GetActor(int id)
        {
            Actor objActor = objFilmMgmtSystemContext.Actors.Find(id);
            return objActor;
        }

        //public Actor SearchActor(string model)
        //{
        //    IEnumerable<Actor> objActor = objFilmMgmtSystemContext.Actors.Where(a => a.Model.Contains(model));
        //    //IEnumerable<Car> objCar = objCarInformationContext.Cars;
        //    //objCar = objCar.Where(a => a.Model.Contains(model)).ToList();

        //    return objActor.FirstOrDefault();
        //}


        public IEnumerable<Actor> GetActor()
        {
            return objFilmMgmtSystemContext.Actors;
        }


    }
}