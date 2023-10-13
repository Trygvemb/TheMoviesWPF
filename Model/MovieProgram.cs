using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMoviesWPF.Model
{
    internal class MovieProgram
    {
        public int Id { get; set; }
        public string MovieDirector { get; set; }
        public Movie Movie { get; set; }
        public DateTime ShowTimes { get; set; }
        public DateTime PremierDate { get; set; }
        public Cinema Cinema { get; set; }

        public MovieProgram(Movie movie, DateTime showTimes, string movieDirector, DateTime premierDate, Cinema cinema)
        {
            Id = 0;
            Movie = movie;
            ShowTimes = showTimes;
            MovieDirector = movieDirector;
            PremierDate = premierDate;
            Cinema = cinema;
        }
    }
}
