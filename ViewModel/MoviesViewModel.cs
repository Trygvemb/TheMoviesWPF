using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheMoviesWPF.Model;
using TheMoviesWPF.Model.Interfaces;
using System.ComponentModel;


namespace TheMoviesWPF.ViewModel
{
    internal class MoviesViewModel : INotifyPropertyChanged
    {
        private readonly MovieRepository _movieRepository;
        private ObservableCollection<Movie> movies;
        private Movie selectedMovie;

        public string Title { get; set; }
        public string Genre { get; set; }
        public int Length { get; set; }
        public ICommand AddMovieCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<Movie> Movies
        {
            get { return movies; }
            set { movies = value; }
        }

        public Movie SelectedMovie
        {
            get { return selectedMovie; }
            set
            {
                selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
            }
        }

        public MoviesViewModel(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            movies = movieRepository.movies;
            movieRepository.GetAll();
        }


        public void AddMovie(string title, string genre, int length)
        {
            var newMovie = new Movie(title, genre, length);

            _movieRepository.Add(newMovie);

            OnPropertyChanged("Title");
            OnPropertyChanged("Genre");
            OnPropertyChanged("Length");
        }

        public void RemoveMovie(Movie movie)
        {
            _movieRepository.Remove(movie);
        }

        public void UpdateMovie(Movie movie)
        {
            _movieRepository.Update(movie);
        }

    }
}
