using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MHRAApp.Models
{
    public class Horse
    {
        public int HorseId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public string HorseDiscipline { get; set; }
        public string Notes { get; set; }
    }

    /*public class DisciplineCategory
    {
        public int DisciplineCategoryId { get; set; }
        public string Discipline { get; set; }
        public List<Horse> Horses { get; set; }
    }*/

    public class HorseContext : DbContext
    {
        public DbSet<Horse> Horses { get; set; }
        //public DbSet<DisciplineCategory> Disciplines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=AcademyData.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Horse>()
                .Property(p => p.HorseId)
                .IsRequired();
            
            modelBuilder.Entity<Horse>()
                .Property(p => p.Name)
                .IsRequired();

            /*modelBuilder.Entity<DisciplineCategory>()
                .Property(p => p.DisciplineCategoryId)
                .IsRequired();*/
        }
    }
}
