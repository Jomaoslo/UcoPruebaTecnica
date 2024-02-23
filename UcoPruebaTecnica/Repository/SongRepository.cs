using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using UcoPruebaTecnica.Models;

namespace UcoPruebaTecnica.Repository
{
    public class SongRepository
    {
        private readonly string _connectionString;

        public SongRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbUcoPrueba");
        }

        public Response Song(Song song, bool delete)
        {
            Response response = new();
            DataSet ds = new();

            using SqlConnection sql = new(_connectionString);
            using SqlCommand cmd = new("uspCancion", sql);
            
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@IdCancion", song.IdCancion));
            cmd.Parameters.Add(new SqlParameter("@IdArtista", song.IdArtista));
            cmd.Parameters.Add(new SqlParameter("@Nombre", song.Nombre));
            cmd.Parameters.Add(new SqlParameter("@Duracion", song.Duracion));
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
            return response;
        }

        public DataSet GetSong(string nombre)
        {
            DataSet ds = new();

            using SqlConnection sql = new(_connectionString);
            using SqlCommand cmd = new("uspCancionFind", sql);

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