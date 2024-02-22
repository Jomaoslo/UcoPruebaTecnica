using Microsoft.AspNetCore.Mvc;
using UcoPruebaTecnica.Models;
using UcoPruebaTecnica.Business;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UcoPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ArtistBusiness _artistBusiness;

        public ArtistController(ArtistBusiness artistBusiness)
        {
            _artistBusiness = artistBusiness;
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
