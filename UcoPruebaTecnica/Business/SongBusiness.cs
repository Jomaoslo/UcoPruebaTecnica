using System;
using UcoPruebaTecnica.Models;
using UcoPruebaTecnica.Repository;

namespace UcoPruebaTecnica.Business
{  
    public class SongBusiness
    {
        private readonly SongRepository _songRepository;
        
        public SongBusiness(SongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        /// Método agregar una nueva canción
        public Response AddSong(Song song)
        {
            Response response;
            try
            {
                song.IdCancion = 0;
                response = _songRepository.Song(song, false);
                return response;
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
                response = new Response { State = false, Code = 400, Messsage = result };
            }
            return response;
        }

        /// Método actualizar una canción
        public Response UpdSong(Song song)
        {
            Response response;
            try
            {
                response = _songRepository.Song(song, false);
                return response;
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
                response = new Response { State = false, Code = 400, Messsage = result };
            }
            return response;
        }

        /// Método eliminar una canción
        public Response DelSong(Song song)
        {
            Response response;
            try
            {
                response = _songRepository.Song(song, true);
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