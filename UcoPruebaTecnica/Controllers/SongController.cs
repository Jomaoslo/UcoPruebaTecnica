using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UcoPruebaTecnica.Models;
using System.Diagnostics;
using UcoPruebaTecnica.Business;
using System.Linq;

namespace UcoPruebaTecnica.Controllers
{
    public class SongController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SongBusiness _songBusiness;

        public SongController(ILogger<HomeController> logger, SongBusiness songBusiness)
        {
            _logger = logger;
            _songBusiness = songBusiness;
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
            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(long idArtista, string nombre, string duracion)
        {
            ViewBag.alerta = "success";

            Song song = new()
            {
                IdCancion = 0,
                IdArtista = idArtista,
                Nombre = nombre,
                Duracion = duracion
            };
            var response = _songBusiness.AddSong(song);
            ViewBag.res = response.Messsage;
            if (!response.State)         
                ViewBag.alerta = "danger";                
            
            return View();
        }

        public IActionResult Actualizar(long idSong)
        {
            ViewBag.alerta = "info";
            ViewBag.res = "Actualizar Canción";

            var response = _songBusiness.GetSong(string.Empty);
            if (!response.Item1.State)
            {
                ViewBag.alerta = "danger";
                ViewBag.res = response.Item1.Messsage;
            }            
            return View(response.Item2.Where(x => x.IdArtista == idSong).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Actualizar(long idCancion, long idArtista, string nombre, string duracion)
        {
            Song song = new()
            {
                IdCancion = idCancion,
                IdArtista = idArtista,
                Nombre = nombre,
                Duracion = duracion
            };
            var response = _songBusiness.UpdSong(song);

            ViewBag.alerta = "success";
            ViewBag.res = response.Messsage;

            if (!response.State)
                ViewBag.alerta = "danger";

            return View();
        }

        public IActionResult Eliminar(long idCancion)
        {
            Song song = new()
            {
                IdCancion = idCancion,
                IdArtista = 0,
                Nombre = string.Empty,
                Duracion = string.Empty
            };
            var response = _songBusiness.DelSong(song);

            ViewBag.alerta = "success";
            ViewBag.res = response.Messsage;

            if (!response.State)
                ViewBag.alerta = "danger";

            return View();
        }
    }
}
