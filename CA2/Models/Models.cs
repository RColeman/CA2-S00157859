using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CA2.Models
{
    class MovieDbInitialiser : DropCreateDatabaseAlways<MovieDb>
    {
        protected override void Seed(MovieDb context)
        {
            // seed the database with new Movies and Actors
            Movie c = new Movie()
            {
                Title = "127 Hours",
                StartDate = DateTime.Parse("4/6/2010")
            };
            context.Movies.Add(c);

            Actor AlPacino = new Actor() { Name = "James Franco", Screename = "Aron Ralston" };
            c.Actors = new List<Actor>();
            c.Actors.Add(AlPacino);

            context.Movies.Add(new Movie()
            {
                Title = "The Dark Knight",
                StartDate = DateTime.Parse("12/1/2014"),
                Actors = new List<Actor>()
                {
                    new Actor() {Name = "Christian Bale", Screename = "Bruce Wayne"},
                    new Actor() {Name = "Maggie Gylenhaal", Screename = "Rachel Dawes"},
                    new Actor() {Name = "Heath Ledger", Screename = "The Joker"}
                }
            });

            context.Movies.Add(new Movie()
            {
                Title = "The Departed",
                StartDate = DateTime.Parse("15/1/2012"),
                Actors = new List<Actor>()
                {
                    new Actor() {Name = "Matt Damon", Screename = "Colin Sullivan"},
                    new Actor() {Name = "Leonardo Di Caprio", Screename = "Billy Costigan"},
                    new Actor() {Name = "Jack Nicolson", Screename = "Frank Costello"},
                    new Actor() {Name = "Mark Wahlberg", Screename = "Sgt Dignam"}
                }
            });

            context.Movies.Add(new Movie()
            {
                Title = "Wolf of Wall Street",
                StartDate = DateTime.Parse("5/1/2009"),
                Actors = new List<Actor>()
                {
                    new Actor() {Name = "Leonardo Di Caprio", Screename = "Jordan Belfort"},
                    new Actor() {Name = "Jonah Hill", Screename = "Donnie Azoff"},
                    new Actor() {Name = "Matthew McConaughey", Screename = "Mark Hanna"}
                }
            });

            context.Movies.Add(new Movie()
            {
                Title = "Reign of Fire",
                StartDate = DateTime.Parse("5/1/2009"),
                Actors = new List<Actor>()
                {
                    new Actor() {Name = "Christian Bale", Screename = "Quinn Abercromby"},
                    new Actor() {Name = "Matthew McConaughey", Screename = "Denton Van Zan"}
                }
            });

        }
    }

    public class MovieDb : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public MovieDb()
            : base("MovieDb")
        { }
    }
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [Display(Name = "Movie")]
        public string Title { get; set; }
        [DisplayName("Opening Date"), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Actors")]
        public virtual List<Actor> Actors { get; set; }
    }

    public class Actor
    {
        [Key]
        public int ActorId { get; set; }
        public string Name { get; set; }
        public string Screename { get; set; }
        public Movie Movie { get; set; }
    }

  

}
