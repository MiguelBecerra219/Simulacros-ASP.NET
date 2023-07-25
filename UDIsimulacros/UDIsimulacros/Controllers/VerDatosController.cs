using Microsoft.AspNetCore.Mvc;
using UDIsimulacros.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


using Microsoft.AspNetCore.Authorization;

namespace UDIsimulacros.Controllers
{
    public class VerDatosController : Controller
    {

        private readonly DbsimulacrosudiContext _context;
        public VerDatosController(DbsimulacrosudiContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Ver()
        {

            /*using(var context = _context)
            {
                var info = context.Usuarios.Single(b => b.Idusuario == 1);
            }*/
            var idUsuario = AccesoController.Global.idUsuario;

            var usuario = await _context.Usuarios
                .Include(u => u.IdCarreraNavigation)
                .FirstOrDefaultAsync(m => m.Idusuario == idUsuario);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

    }
}
