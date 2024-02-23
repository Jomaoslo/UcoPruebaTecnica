using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UcoPruebaTecnica.Models;
using UcoPruebaTecnica.Business;
using System.Linq;

namespace UcoPruebaTecnica.Controllers
{
    public class SongController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SongBusiness _songBusiness;
        private readonly ArtistBusiness _artistBusiness;

        public SongController(ILogger<HomeController> logger, SongBusiness songBusiness, ArtistBusiness artistBusiness)
        {
            _logger = logger;
            _songBusiness = songBusiness;
            _artistBusiness = artistBusiness;
        }

        public IActionResult Index()
        {
            ViewBag.alerta = "info";
            ViewBag.res = "Listado de Canciones";

            var response = _songBusiness.GetSong(string.Empty);
            if (!response.Item1.State)
            {
                ViewBag.alerta = "danger";
                ViewBag.res = response.Item1.Messsage;
            }
            return View(response.Item2);
        }

        public IActionResult Nuevo()
        {
            ViewBag.alerta = "info";
            ViewBag.res = "Registrar Nueva Canción";
            var artist = _artistBusiness.GetArtist(string.Empty).Item2.OrderBy(x => x.Nombre).ToList();
            return View(artist);
        }

        [HttpPost]
        public IActionResult Nuevo(long idArtista, string nombre, string duracion)
        {
            Song song = new()
            {
                IdCancion = 0,
                IdArtista = idArtista,
                Nombre = nombre.ToUpper(),
                Duracion = duracion
            };
            var response = _songBusiness.AddSong(song);

            ViewBag.alerta = "success";
            ViewBag.res = response.Messsage;

            if (!response.State)         
                ViewBag.alerta = "danger";

            var artist = _artistBusiness.GetArtist(string.Empty).Item2.OrderBy(x => x.Nombre).ToList();
            return View(artist);
        }

        public IActionResult Actualizar(long idCancion)
        {
            ViewBag.alerta = "info";
            ViewBag.res = "Actualizar Canción";

            var song = _songBusiness.GetSongbyIdCancion(idCancion);
            song.Artist = _artistBusiness.GetArtist(string.Empty).Item2.OrderBy(x => x.Nombre).ToList();
            return View(song);
        }

        [HttpPost]
        public IActionResult Actualizar(long idCancion, long idArtista, string nombre, string duracion)
        {
            Song song = new()
            {
                IdCancion = idCancion,
                IdArtista = idArtista,
                Nombre = nombre.ToUpper(),
                Duracion = duracion
            };
            var response = _songBusiness.UpdSong(song);

            ViewBag.alerta = "success";
            ViewBag.res = response.Messsage;

            if (!response.State)
                ViewBag.alerta = "danger";

            song.Artist = _artistBusiness.GetArtist(string.Empty).Item2.OrderBy(x => x.Nombre).ToList();
            return View(song);
        }

        public IActionResult Eliminar(long idCancion)
        {
            ViewBag.alerta = "warning";
            ViewBag.res = "¿Está seguro que desea eliminar la canción?";

            var artist = _songBusiness.GetSongbyIdCancion(idCancion);
            return View(artist);
        }

        [HttpPost]
        public IActionResult Eliminar(long idCancion, long idArtista, string nombre, string duracion)
        {
            Song song = new()
            {
                IdCancion = idCancion,
                IdArtista = idArtista,
                Nombre = nombre.ToUpper(),
                Duracion = duracion
            };
            var response = _songBusiness.DelSong(song);

            ViewBag.alerta = "success";
            ViewBag.res = response.Messsage;

            if (!response.State)
                ViewBag.alerta = "danger";

            return RedirectToAction("Index", "Song");
        }
    }
}
