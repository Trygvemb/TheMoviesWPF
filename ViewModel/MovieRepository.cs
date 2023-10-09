using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMoviesWPF.Model;
using TheMoviesWPF.Model.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using System.Runtime.ConstrainedExecution;

namespace TheMoviesWPF.ViewModel
{
    internal class MovieRepository : IMovieRepository
    {
        private readonly string ConnectionString;

        public ICollection<Movie> movies;
        public MovieRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            ConnectionString = config.GetConnectionString("MyDBConnection");

        }
        public void Add(Movie entity)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Movie(Title, Genre, Lenth)" +
                                                "VALUES(@Title, @Genre, @Lenth)" +
                                                "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Titel", SqlDbType.NVarChar).Value = entity.Title;
                cmd.Parameters.Add("@Genre", SqlDbType.NVarChar).Value = entity.Genre;
                cmd.Parameters.Add("@Length", SqlDbType.Int).Value = entity.Length;
                entity.Id = Convert.ToInt32(cmd.ExecuteScalar());

                movies.Add(entity);
            }
        }

        public IEnumerable<Movie> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

