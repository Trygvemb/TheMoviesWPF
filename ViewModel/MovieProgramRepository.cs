using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMoviesWPF.Model;
using TheMoviesWPF.Model.Interfaces;

namespace TheMoviesWPF.ViewModel
{
    internal class MovieProgramRepository : IMovieProgramRepository
    {
        private readonly string ConnectionString;

        public ObservableCollection<MovieProgram> moviePrograms = new ObservableCollection<MovieProgram>();
        public MovieProgramRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            ConnectionString = config.GetConnectionString("MyDBConnection");

        }
        public void Add(MovieProgram entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovieProgram> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(MovieProgram entity)
        {
            throw new NotImplementedException();
        }

        public void Update(MovieProgram entity)
        {
            throw new NotImplementedException();
        }
    }
}
