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
    internal class CinemaRepository : ICinemaRepository
    {
        private readonly string ConnectionString;

        public ObservableCollection<Cinema> cinemas = new ObservableCollection<Cinema>();
        public CinemaRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            ConnectionString = config.GetConnectionString("MyDBConnection");

        }
        public void Add(Cinema entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cinema> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(Cinema entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Cinema entity)
        {
            throw new NotImplementedException();
        }
    }
}
