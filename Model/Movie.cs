using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMoviesWPF.Model
{
    internal class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Length { get; set; }

        public Movie(int id, string title, string genre, int length)
        {
            Id = id;
            Title = title;
            Genre = genre;
            Length = length;
        }
    }
}
