using System;
using System.Collections.Generic;

#nullable disable

namespace FilmMgmtSystem.Entities
{
    public partial class Language
    {
        public Language()
        {
            Films = new HashSet<Film>();
        }

       // public int Id { get; set; }
        public int Languageid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Film> Films { get; set; }
    }
}
