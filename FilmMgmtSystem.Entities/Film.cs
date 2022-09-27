using System;
using System.Collections.Generic;

#nullable disable

namespace FilmMgmtSystem.Entities
{
    public partial class Film
    {
       // public int Id { get; set; }
        public int Filmid { get; set; }
        public string FilmDescription { get; set; }
        public string Title { get; set; }
        public DateTime? Releaseyear { get; set; }
        public int? Languageid { get; set; }
        public DateTime? Rentalduration { get; set; }
        public int? FilmLength { get; set; }
        public int? Replacementcost { get; set; }
        public decimal? Rating { get; set; }
        public string Specialfeatures { get; set; }
        public int? Actorid { get; set; }
        public int? Categoryid { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual Category Category { get; set; }
        public virtual Language Language { get; set; }   
    }
}
