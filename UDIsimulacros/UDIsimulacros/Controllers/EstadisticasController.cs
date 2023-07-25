using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UDIsimulacros.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using UDIsimulacros.models;

namespace UDIsimulacros.Controllers
{
    [Authorize]
    public class EstadisticasController : Controller
    {
        private readonly DbsimulacrosudiContext _context;
        public EstadisticasController(DbsimulacrosudiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> EstadisticasEstudiante()
        {
            double promedioLecura, promedioRazonamiento, promedioCompetencias, promedioIngles;

            var puntajesLectura = _context.Informepuebas
                .Where(m => m.IdUsuario.Equals(AccesoController.Global.idUsuario) && m.Categoria.Equals("Lectura critica"))
                .ToList();

            var puntajesRazonamiento = _context.Informepuebas
                .Where(m => m.IdUsuario.Equals(AccesoController.Global.idUsuario) && m.Categoria.Equals("Razonamiento cuantitativo"))
                .ToList();

            var puntajesCompetencias = _context.Informepuebas
                .Where(m => m.IdUsuario.Equals(AccesoController.Global.idUsuario) && m.Categoria.Equals("Competencias ciudadanas"))
                .ToList();


            var puntajesIngles = _context.Informepuebas
                .Where(m => m.IdUsuario.Equals(AccesoController.Global.idUsuario) && m.Categoria.Equals("Ingles"))
                .ToList();



            promedioLecura = promedio(puntajesLectura);
            promedioRazonamiento = promedio(puntajesRazonamiento);
            promedioCompetencias = promedio(puntajesCompetencias); 
            promedioIngles = promedio(puntajesIngles);


            ViewData["promedioLecura"] = promedioLecura;
            ViewData["promedioRazonamiento"] = promedioRazonamiento;
            ViewData["promedioCompetencias"] = promedioCompetencias;
            ViewData["promedioIngles"] = promedioIngles;


            ViewData["porcentajeLecura"] = porcentaje(promedioLecura);
            ViewData["porcentajeRazonamiento"] = porcentaje(promedioRazonamiento);
            ViewData["porcentajeCompetencias"] = porcentaje(promedioCompetencias);
            ViewData["porcentajeIngles"] = porcentaje(promedioIngles);

            return View();
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> seleccionEstudiante()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> estadisticasSeleccionadas(Correo _correo)  
        {
            var usuario = _context.Usuarios.SingleOrDefault(u => u.Correo.Equals(_correo.correo));

            double promedioLecura, promedioRazonamiento, promedioCompetencias, promedioIngles;

            var puntajesLectura = _context.Informepuebas
                .Where(m => m.IdUsuario.Equals(usuario.Idusuario) && m.Categoria.Equals("Lectura critica"))
                .ToList();

            var puntajesRazonamiento = _context.Informepuebas
                .Where(m => m.IdUsuario.Equals(usuario.Idusuario) && m.Categoria.Equals("Razonamiento cuantitativo"))
                .ToList();

            var puntajesCompetencias = _context.Informepuebas
                .Where(m => m.IdUsuario.Equals(usuario.Idusuario) && m.Categoria.Equals("Competencias ciudadanas"))
                .ToList();

            var puntajesIngles = _context.Informepuebas
                .Where(m => m.IdUsuario.Equals(usuario.Idusuario) && m.Categoria.Equals("Ingles"))
                .ToList();



            promedioLecura = promedio(puntajesLectura);
            promedioRazonamiento = promedio(puntajesRazonamiento);
            promedioCompetencias = promedio(puntajesCompetencias);
            promedioIngles = promedio(puntajesIngles);


            ViewData["promedioLecura"] = promedioLecura;
            ViewData["promedioRazonamiento"] = promedioRazonamiento;
            ViewData["promedioCompetencias"] = promedioCompetencias;
            ViewData["promedioIngles"] = promedioIngles;


            ViewData["porcentajeLecura"] = porcentaje(promedioLecura);
            ViewData["porcentajeRazonamiento"] = porcentaje(promedioRazonamiento);
            ViewData["porcentajeCompetencias"] = porcentaje(promedioCompetencias);
            ViewData["porcentajeIngles"] = porcentaje(promedioIngles);

            return View();
        }

        public static double promedio(List<Informepueba> informes)
        {
            int acomulador=0;
            double Promedio;
            for(int i=0; i<informes.Count; i++)
            {
                acomulador = acomulador + informes[i].Calificacion;
            }
            if(informes.Count!=0)
            {
                Promedio = acomulador / informes.Count;
            }
            else
            {
                Promedio = 0;
            }
            

            return Promedio;
        }

        public static string porcentaje(double promedio)
        {  
            promedio = (promedio * 10);
            var textoFinal = "width: " + promedio + "%";
            return textoFinal;

        }

    }
}
