using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMoviesWPF.Model
{
    internal class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Theater { get; set; }

        public Cinema(string name, string theater)
        {
            Id = 0;
            Name = name;
            Theater = theater;
        }
    }
}
