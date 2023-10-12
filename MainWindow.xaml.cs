using System;
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


        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            moviesViewModel.AddMovie();
            tbTitle.Text = "";
            tbGenre.Text = "";
            tbLength.Text = "";
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
