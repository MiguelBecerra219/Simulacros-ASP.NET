using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Specialized;
using UDIsimulacros.models;
using UDIsimulacros.Models;
namespace UDIsimulacros.Controllers
{
    [Authorize]
    public class CuestionarioController : Controller
    {

        private readonly DbsimulacrosudiContext _context;

        public CuestionarioController(DbsimulacrosudiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public async Task<IActionResult> IniciarPrueba(string seleccion)
        {
            ViewData["Avance"] = Global.cont; 
            ViewData["porcentaje"] = porcentaje(Global.cont);
            Global.correctas = 0;
            Global.categoria = seleccion;
            Global.cont = 0;


            var pregunta = _context.Pregunta
                        .Where(m => m.Categoria.Equals(seleccion))
                        .ToList();


            return View(pregunta[Global.cont]);
        }

        [HttpPost] 
        public async Task<IActionResult> seguirPrueba()
        {  
            Global.cont = Global.cont + 1;
            ViewData["Avance"] = Global.cont;
            ViewData["porcentaje"] = porcentaje(Global.cont);

            var pregunta = _context.Pregunta
                .Where(m => m.Categoria.Equals(Global.categoria))
                .ToList();

            if (Global.cont > 9)
            {
                return RedirectToAction("Resultados", "Cuestionario");
            }
            else
            {
                return View(pregunta[Global.cont]);
            }

        }


        [HttpPost]
        public async Task<IActionResult> Respuesta()
        {
            

            var collection = Request.Form;
            var seleccion = collection["exampleRadios"];
            if (seleccion == "option1")
            {
                Global.correctas = Global.correctas + 1;
                return RedirectToAction("RespuestaCorrecta", "Cuestionario");
            }
            else
            {
                return RedirectToAction("RespuestaIncorrecta", "Cuestionario");
            }
        }

        public async Task<IActionResult> RespuestaCorrecta()
        {
            return View();
        }

        public async Task<IActionResult> RespuestaIncorrecta()
        {
            return View();
        }

        public async Task<IActionResult> Resultados()
        {
            var Informe = new Informepueba { Calificacion = Global.correctas, IdUsuario = AccesoController.Global.idUsuario, FechaHora = DateTime.Now, Categoria = Global.categoria };
            _context.Add(Informe);
            await _context.SaveChangesAsync();
            ViewData["Puntaje"] = Global.correctas;
            return View();
        }

        public static class Global
        {
            public static int correctas;
            public static int cont;
            public static String categoria = "";
        }

        public static string porcentaje(int cont)
        {
            string Porcentaje="";
            int numero = 0;
            numero = (cont + 1) * 10;
            Porcentaje = "width:" + numero + "%";
          
            
            return Porcentaje;
        }

    }
}
