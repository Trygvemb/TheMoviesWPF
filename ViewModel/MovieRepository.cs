using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMoviesWPF.Model;
using TheMoviesWPF.Model.Interfaces;

namespace TheMoviesWPF.ViewModel
{
    internal class MovieRepository : IMovieRepository
    {
        public MovieRepository()
        {
            
        }
        public void Add(Movie entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
}
