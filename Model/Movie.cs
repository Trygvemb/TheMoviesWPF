using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMoviesWPF.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Length { get; set; }

        public Movie(string title, string genre, int length)
        {
            Id = 0;
            Title = title;
            Genre = genre;
            Length = length;
        }
    }
}
