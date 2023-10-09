using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheMoviesWPF.Model;
using TheMoviesWPF.Model.Interfaces;

namespace TheMoviesWPF.ViewModel
{
    internal class MoviesViewModel
    {
        private readonly IMovieRepository _movieRepository;

        public ObservableCollection<Movie> movies { get; set; } = new ObservableCollection<Movie>();

        public string Title { get; set; }
        public string Genre { get; set; }
        public int Length { get; set; }

        public ICommand AddMovieCommand { get; }

        public MoviesViewModel(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;

        }

        public void AddMovie()
        {
            var newMovie = new Movie(0, Title, Genre, Length);

            _movieRepository.Add(newMovie);
            movies.Add(newMovie);

            Title = string.Empty;
            Genre = string.Empty;
            Length = 0;
        }
    }
}
