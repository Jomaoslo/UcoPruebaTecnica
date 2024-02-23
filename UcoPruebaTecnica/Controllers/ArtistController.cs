﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UcoPruebaTecnica.Models;
using System.Diagnostics;
using UcoPruebaTecnica.Business;
using System.Linq;

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
            ViewBag.alerta = "info";

            Artist artist = new()
            {
                IdArtista = 0,
                Nombre = nombre.ToUpper(),
                Pais = pais.ToUpper(),
                CasaDisquera = casaDisquera.ToUpper()
            };
            var response = _artistBusiness.AddArtist(artist);
            ViewBag.res = response.Messsage;
            if (!response.State)            
                ViewBag.alerta = "danger";                
            
            return View();
        }

        public IActionResult Actualizar(long idArtista)
        {
            ViewBag.alerta = "info";
            ViewBag.res = "Actualizar Artista";

            var response = _artistBusiness.GetArtist(string.Empty);
            if (!response.Item1.State)
            {
                ViewBag.alerta = "danger";
                ViewBag.res = response.Item1.Messsage;
            }            
            return View(response.Item2.Where(x => x.IdArtista == idArtista).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Actualizar(long idArtista, string nombre, string pais, string casaDisquera)
        {
            ViewBag.alerta = "info";

            Artist artist = new()
            {
                IdArtista = idArtista,
                Nombre = nombre.ToUpper(),
                Pais = pais.ToUpper(),
                CasaDisquera = casaDisquera.ToUpper()
            };
            var response = _artistBusiness.UpdArtist(artist);
            ViewBag.res = response.Messsage;
            if (!response.State)
                ViewBag.alerta = "danger";

            return View();
        }

        public IActionResult Eliminar(long idArtista)
        {
            ViewBag.alerta = "info";
            ViewBag.res = "Actualizar Artista";

            Artist artist = new()
            {
                IdArtista = idArtista,
                Nombre = string.Empty,
                Pais = string.Empty,
                CasaDisquera = string.Empty
            };
            var response = _artistBusiness.DelArtist(artist);
            ViewBag.res = response.Messsage;
            if (!response.State)
                ViewBag.alerta = "danger";
            return View();
        }
    }
}
