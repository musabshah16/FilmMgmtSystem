using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FilmMgmtSystem.Entities;

namespace FilmMgmtSystem.WebAPI.Data
{
    public class FilmMgmtSystemWebAPIContext : DbContext
    {
        public FilmMgmtSystemWebAPIContext (DbContextOptions<FilmMgmtSystemWebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<FilmMgmtSystem.Entities.Actor> Actor { get; set; }

        public DbSet<FilmMgmtSystem.Entities.Category> Category { get; set; }

        public DbSet<FilmMgmtSystem.Entities.Film> Film { get; set; }

        public DbSet<FilmMgmtSystem.Entities.Language> Language { get; set; }
    }
}
