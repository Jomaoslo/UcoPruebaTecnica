using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UcoPruebaTecnica.Models;
using UcoPruebaTecnica.Repository;

namespace UcoPruebaTecnica.Business
{
    public class ArtistBusiness
    {
        private readonly ArtistRepository _artistRepository;
        
        public ArtistBusiness(ArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        /// Método agregar un nuevo artista
        public Response AddArtist(Artist artist)
        {
            Response response;
            try
            {
                artist.IdArtista = 0;
                response = _artistRepository.Artist(artist, false);
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
                response = new Response { State = false, Code = 400, Messsage = result };              
            }
            return response;
        }

        /// Método actualizar un artista
        public Response UpdArtist(Artist artist)
        {
            Response response;
            try
            {
                response = _artistRepository.Artist(artist, false);
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
                response = new Response { State = false, Code = 400, Messsage = result };
            }
            return response;
        }

        /// Método eliminar un artista
        public Response DelArtist(Artist artist)
        {
            Response response;
            try
            {
                response = _artistRepository.Artist(artist, true);
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
                response = new Response { State = false, Code = 400, Messsage = result };
            }
            return response;
        }

        /// Método consultar un artista
        public (Response, List<Artist>) GetArtist(string nombre)
        {
            Response response;
            List<Artist> artist = new();
            try
            {
                var ds = _artistRepository.GetArtist(nombre);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Artist objArtist = new()
                    {
                        IdArtista = Convert.ToInt64(row["IdArtista"]),
                        Nombre = row["Nombre"].ToString(),
                        Pais = row["Pais"].ToString().ToString(),
                        CasaDisquera = row["CasaDisquera"].ToString()
                    };
                    artist.Add(objArtist);
                }
                response = new Response { State = true, Code = 200, Messsage = "Consulta existosa" };
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
                response = new Response { State = false, Code = 400, Messsage = result };
            }
            return (response, artist);
        }

        /// Método consultar un artista por Id
        public Artist GetArtistbyIdArtista(long idArtista)
        {
            Artist artist = new();
            try
            {
                var response = GetArtist(string.Empty);
                artist = response.Item2.Where(x => x.IdArtista == idArtista).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
            }
            return artist;
        }
    } 
}