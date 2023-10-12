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
        private readonly MovieRepository _movieRepository;
        private ObservableCollection<Movie> movies;

        public ObservableCollection<Movie> Movies
        {
            get { return movies; }
            set { movies = value; }
        }

        public string Title { get; set; }
        public string Genre { get; set; }
        public int Length { get; set; }

        public ICommand AddMovieCommand { get; }

        public MoviesViewModel(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            movies = movieRepository.movies;
            movieRepository.GetAll();
        }

        public void AddMovie()
        {
            var newMovie = new Movie(Title, Genre, Length);

            _movieRepository.Add(newMovie);

            Title = string.Empty;
            Genre = string.Empty;
            Length = 0;
        }
        public void RemoveMovie(Movie movie)
        {
            _movieRepository.Remove(movie);
        }

    }
}
