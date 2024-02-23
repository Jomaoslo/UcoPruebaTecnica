using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using UcoPruebaTecnica.Models;

namespace UcoPruebaTecnica.Repository
{
    public class ArtistRepository
    {
        private readonly string _connectionString;

        public ArtistRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbUcoPrueba");
        }

        public Response Artist(Artist artist, bool delete)
        {
            Response response = new();
            DataSet ds = new();

            using SqlConnection sql = new(_connectionString);
            using SqlCommand cmd = new("uspArtista", sql);
            
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@IdArtista", artist.IdArtista));
            cmd.Parameters.Add(new SqlParameter("@Nombre", artist.Nombre));
            cmd.Parameters.Add(new SqlParameter("@Pais", artist.Pais));
            cmd.Parameters.Add(new SqlParameter("@CasaDisquera", artist.CasaDisquera));
            cmd.Parameters.Add(new SqlParameter("@Delete", delete));

            sql.Open();
            SqlDataAdapter da = new(cmd);
            da.Fill(ds);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                response.State = Convert.ToBoolean(row["Estado"]);
                response.Code = Convert.ToInt32(row["Codigo"]);
                response.Messsage = row["Mensaje"].ToString();
            }
            sql.Close();
            return response;
        }

        public DataSet GetArtist(string nombre)
        {
            DataSet ds = new();

            using SqlConnection sql = new(_connectionString);
            using SqlCommand cmd = new("uspArtistaFind", sql);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Nombre", nombre));
      
            sql.Open();
            SqlDataAdapter da = new(cmd);
            da.Fill(ds);

            sql.Close();
            return ds;
        }
    }   
}