using Microsoft.AspNetCore.Mvc;
using UcoPruebaTecnica.Models;
using UcoPruebaTecnica.Business;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UcoPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistApiController : ControllerBase
    {
        private readonly ArtistBusiness _artistBusiness;

        public ArtistApiController(ArtistBusiness artistBusiness)
        {
            _artistBusiness = artistBusiness;
        }

        [HttpGet]
        public dynamic Artist( string nombre)
        {
            var response = _artistBusiness.GetArtist(nombre);
            if (response.Item1.State)
                return response.Item2;
            else
                return response.Item1;
        }

        [HttpPost]
        public dynamic AddArtist([FromBody] Artist artist)
        {
            var response = _artistBusiness.AddArtist(artist);
            return response;
        }

        [HttpPut]
        public dynamic UpdArtist([FromBody] Artist artist)
        {
            var response = _artistBusiness.UpdArtist(artist);
            return response;
        }

        [HttpDelete]
        public dynamic DelArtist([FromBody] Artist artist)
        {
            var response = _artistBusiness.DelArtist(artist);
            return response;
        }
    }
}
