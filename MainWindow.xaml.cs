﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheMoviesWPF.Model;
using TheMoviesWPF.ViewModel;

namespace TheMoviesWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MoviesViewModel moviesViewModel = new MoviesViewModel(new MovieRepository());
        public MainWindow()
        {
            DataContext = moviesViewModel;
            //entries = new ObservableCollection<Movie>();
            InitializeComponent();
        }

        private void lvMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvMovies.SelectedItem != null)
            {
                moviesViewModel.SelectedMovie = (Movie)lvMovies.SelectedItem;
            }
        }


        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            
            string title = tbTitle.Text.ToString();
            string genre = tbGenre.Text.ToString();
            int length;
            int.TryParse(tbLength.Text, out length);
            
            moviesViewModel.AddMovie(title, genre, length);
            tbTitle.Text = "";
            tbGenre.Text = "";
            tbLength.Text = "";
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            Movie movieToRemove = (Movie)lvMovies.SelectedItem;
            moviesViewModel.RemoveMovie(movieToRemove);
            tbTitle.Text = "";
            tbGenre.Text = "";
            tbLength.Text = "";
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            Movie movieToUpdate = (Movie)lvMovies.SelectedItem;
            moviesViewModel.UpdateMovie(movieToUpdate);
            tbTitle.Text = "";
            tbGenre.Text = "";
            tbLength.Text = "";
        }

        private void ClearInput_Click(object sender, RoutedEventArgs e)
        {
            tbTitle.Text = "";
            tbGenre.Text = "";
            tbLength.Text = "";
        }
    }
}
