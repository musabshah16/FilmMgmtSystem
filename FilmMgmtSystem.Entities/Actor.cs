using System;
using System.Collections.Generic;

#nullable disable

namespace FilmMgmtSystem.Entities
{
    public partial class Actor
    {
        public Actor()
        {
            Films = new HashSet<Film>();
        }
        //public int Id { get; set; }
        public int Actorid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Film> Films { get; set; }
    }
}
