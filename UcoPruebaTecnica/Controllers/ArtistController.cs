using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UcoPruebaTecnica.Models;
using UcoPruebaTecnica.Business;

namespace UcoPruebaTecnica.Controllers
{
    public class ArtistController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ArtistBusiness _artistBusiness;

        public ArtistController(ILogger<HomeController> logger, ArtistBusiness artistBusiness)
        {
            _logger = logger;
            _artistBusiness = artistBusiness;
        }

        public IActionResult Index()
        {
            ViewBag.alerta = "info";
            ViewBag.res = "Listado de Artistas";

            var response = _artistBusiness.GetArtist(string.Empty);
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
            ViewBag.res = "Registrar Nuevo Artista";
            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(string nombre, string pais, string casaDisquera)
        {
            Artist artist = new()
            {
                IdArtista = 0,
                Nombre = nombre.ToUpper(),
                Pais = pais.ToUpper(),
                CasaDisquera = casaDisquera.ToUpper()
            };
            var response = _artistBusiness.AddArtist(artist);

            ViewBag.alerta = "success";
            ViewBag.res = response.Messsage;

            if (!response.State)            
                ViewBag.alerta = "danger";                
            
            return View();
        }

        public IActionResult Actualizar(long idArtista)
        {
            ViewBag.alerta = "info";
            ViewBag.res = "Actualizar Artista";

            var artist = _artistBusiness.GetArtistbyIdArtista(idArtista);
            return View(artist);
        }

        [HttpPost]
        public IActionResult Actualizar(long idArtista, string nombre, string pais, string casaDisquera)
        {
            Artist artist = new()
            {
                IdArtista = idArtista,
                Nombre = nombre.ToUpper(),
                Pais = pais.ToUpper(),
                CasaDisquera = casaDisquera.ToUpper()
            };
            var response = _artistBusiness.UpdArtist(artist);

            ViewBag.alerta = "success";
            ViewBag.res = response.Messsage;

            if (!response.State)
                ViewBag.alerta = "danger";

            return View(artist);
        }

        public IActionResult Eliminar(long idArtista)
        {
            ViewBag.alerta = "warning";
            ViewBag.res = "¿Está seguro que desea eliminar el artista?";

            var artist = _artistBusiness.GetArtistbyIdArtista(idArtista);
            return View(artist);
        }

        [HttpPost]
        public IActionResult Eliminar(long idArtista, string nombre, string pais, string casaDisquera)
        {
            Artist artist = new()
            {
                IdArtista = idArtista,
                Nombre = nombre.ToUpper(),
                Pais = pais.ToUpper(),
                CasaDisquera = casaDisquera.ToUpper()
            };
            var response = _artistBusiness.DelArtist(artist);

            ViewBag.alerta = "success";
            ViewBag.res = response.Messsage;

            if (!response.State)
                ViewBag.alerta = "danger";

            return RedirectToAction("Index");
        }
    }
}
