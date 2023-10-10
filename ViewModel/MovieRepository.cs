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
using System.Collections.ObjectModel;

namespace TheMoviesWPF.ViewModel
{
    internal class MovieRepository : IMovieRepository
    {
        private readonly string ConnectionString;

        public ObservableCollection<Movie> movies = new ObservableCollection<Movie>();
        public MovieRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            ConnectionString = config.GetConnectionString("MyDBConnection");

        }
        public void Add(Movie movie)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tm_Movies(Title, Genre, Length)" +
                                                "VALUES(@Title, @Genre, @Length)" +
                                                "SELECT @@IDENTITY", con);
                
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = movie.Title;
                cmd.Parameters.Add("@Genre", SqlDbType.NVarChar).Value = movie.Genre;
                cmd.Parameters.Add("@Length", SqlDbType.Int).Value = movie.Length;
                movie.Id = Convert.ToInt32(cmd.ExecuteScalar());

                movies.Add(movie);
            }
        }

        public IEnumerable<Movie> GetAll()
        {
            
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Title, Genre, Length FROM tm_Movies", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Movie movie = new Movie(

                            dr["Title"].ToString(),
                            dr["Genre"].ToString(),
                            int.Parse(dr["Length"].ToString())
                            );
                        movies.Add(movie);
                    }
                }
            }
            return movies;

        }
        


    }
}

