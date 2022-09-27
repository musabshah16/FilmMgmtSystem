using FilmMgmtSystem.DAL;
using FilmMgmtSystem.Entities;
using System.Collections.Generic;

namespace FilmMgmtSystem.BL
{
    public class ActorBL
    {
        ActorDAL objActorDAL = new ActorDAL();

        public void CreateActor(Actor objActor)
        {
            objActorDAL.CreateActor(objActor);
        }

        public void UpdateActor(Actor objActor)
        {
            objActorDAL.UpdateActor(objActor);
        }

        public void DeleteActor(int id)
        {
            objActorDAL.DeleteActor(id);
        }

        public Actor GetActor(int id)
        {
            Actor objActor = objActorDAL.GetActor(id);
            return objActor;
        }

        //public Actor SearchActor(string model)
        //{
        //    Actor objActor = objActorDAL.SearchActor(model);
        //    return objActor;
        //}

        //public IEnumerable<GetManufacturerTransmissionType> GetManufacturerTransmissionType()
        //{
        //    return objCarDAL.GetManufacturerTransmissionType();
        //}
        public IEnumerable<Actor> GetActor()
        {

            return objActorDAL.GetActor();
        }


    }
}