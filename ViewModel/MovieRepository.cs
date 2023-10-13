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
using System.Linq.Expressions;

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
                SqlCommand cmd = new SqlCommand("sp_AddMovie @Title, @Genre, @Length", con);

                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = movie.Title;
                cmd.Parameters.Add("@Genre", SqlDbType.NVarChar).Value = movie.Genre;
                cmd.Parameters.Add("@Length", SqlDbType.Int).Value = movie.Length;
                movie.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Find the newly generated Id from database and set it to the movie so movies collection has the correct id
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                int movieId = -1;
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_FindMovieId", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 200) { Value = movie.Title });
                cmd.Parameters.Add(new SqlParameter("@Genre", SqlDbType.NVarChar, 200) { Value = movie.Genre });
                cmd.Parameters.Add(new SqlParameter("@Length", SqlDbType.Int) { Value = movie.Length });

                var result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    movieId = Convert.ToInt32(result);
                }
                movie.Id = movieId;

                movies.Add(movie);
            }
        }

        public IEnumerable<Movie> GetAll()
        {
            
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetMovies", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Movie movie = new Movie(
                            dr["Title"].ToString(),
                            dr["Genre"].ToString(),
                            int.Parse(dr["Length"].ToString())
                            );
                        movie.Id = int.Parse(dr["Id"].ToString());
                        movies.Add(movie);
                    }
                }
            }
            return movies;
        }

        public void Remove(Movie movie)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DeleteMovie @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = movie.Id;
                cmd.ExecuteNonQuery();
            }
            movies.Remove(movie);
        }

        public void Update(Movie movie)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open(); // Open the connection

                SqlCommand cmd = new SqlCommand("sp_UpdateMovie", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = movie.Id });
                cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar) { Value = movie.Title });
                cmd.Parameters.Add(new SqlParameter("@Genre", SqlDbType.NVarChar) { Value = movie.Genre });
                cmd.Parameters.Add(new SqlParameter("@Length", SqlDbType.Int) { Value = movie.Length });

                cmd.ExecuteNonQuery(); // Execute the stored procedure

                // Update the existing movie in the collection
                Movie existingMovie = movies.FirstOrDefault(m => m.Id == movie.Id);
                if (existingMovie != null)
                {
                    existingMovie.Title = movie.Title;
                    existingMovie.Genre = movie.Genre;
                    existingMovie.Length = movie.Length;
                }
            }
        }

    }
}

