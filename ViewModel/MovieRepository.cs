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
                SqlCommand cmd = new SqlCommand("exec sp_AddMovie @Title, @Genre, @Length", con);

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
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateMovie @Id, @Title, @Genre, @Length");

                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = movie.Id;
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = movie.Title;
                cmd.Parameters.Add("@Genre", SqlDbType.NVarChar).Value = movie.Genre;
                cmd.Parameters.Add("@Length", SqlDbType.Int).Value = movie.Length;
                cmd.ExecuteNonQuery();
            }

            Movie existingMovie = movies.FirstOrDefault(m => m.Id == movie.Id);
            if (existingMovie != null)
            {
                existingMovie.Title = movie.Title;
                existingMovie.Genre = movie.Genre;
                existingMovie.Length = movie.Length;
            }
        }

        /*
create proc sp_UpdateMovie
@Id INT,
@Title NVARCHAR(200),
@Genre NVARCHAR(200),
@Length INT
as
BEGIN
    UPDATE tm_Movies
    Set Title = @Title, Genre = @Genre, Length = @Length
    WHERE Id = @Id;
END

CREATE proc sp_DeleteMovie
@Id int
AS
BEGIN
    DELETE from tm_Movies WHERE Id = @Id;
END
        */

    }
}

