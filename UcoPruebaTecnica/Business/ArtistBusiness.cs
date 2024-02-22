using System;
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
                return response;
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
                return response;
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
                return response;
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
                response = new Response { State = false, Code = 400, Messsage = result };
            }
            return response;
        }
    } 
}