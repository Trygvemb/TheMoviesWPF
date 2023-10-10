using System;
using System.Collections.Generic;
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
            InitializeComponent();
            DataContext = moviesViewModel;

            SetMovieOverviewtb();
        }

        private void AddMovie_Click(object sender, RoutedEventArgs e)
        {
            moviesViewModel.AddMovie();
            Title.Text = "";
            Genre.Text = "";
            Length.Text = "";

            
        }

        public void SetMovieOverviewtb()
        {
            moviesViewModel.SetMovieOverview();
        }


    }
}
