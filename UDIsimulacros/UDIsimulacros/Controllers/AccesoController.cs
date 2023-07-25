using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UDIsimulacros.models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


namespace UDIsimulacros.Controllers
{
    public class AccesoController : Controller
    {
        private readonly DbsimulacrosudiContext _context;
        public AccesoController(DbsimulacrosudiContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> Index(Usuario _usuario)
        {
            if(_usuario.Correo == null && _usuario.Contraseña == null)
            {
                return View();
            }
            //var usuario = await _context.Usuarios.FirstOrDefaultAsync(u=> u.Contraseña == _usuario.Contraseña &&  u.Correo == _usuario.Correo);

            //Hacer consulta dentro de la 
            //var usuario2 = _context.Usuarios.FromSql($"SELECT * FROM usuario where contraseña= {_usuario.Contraseña} and correo = {_usuario.Correo}").ToList(); ;
            //var usuario2 = _context.Usuarios.Where(u => u.Contraseña.Equals(_usuario.Contraseña) && u.Correo.Equals(_usuario.Correo)).ToList();
            
            var usuario = _context.Usuarios.SingleOrDefault(u => u.Contraseña.Equals(_usuario.Contraseña) && u.Correo.Equals(_usuario.Correo));


            



            if (usuario != null)
            {
                var Claims = new List<Claim>
                {
                    new Claim("Correo", usuario.Correo),
                    new Claim(ClaimTypes.Role, usuario.Rol)
                    
                };
                var claimsIdentity = new ClaimsIdentity(Claims,CookieAuthenticationDefaults.AuthenticationScheme);
                Global.idUsuario = usuario.Idusuario;
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home");
            }
            else{
                
                return View();

            }

            
            
        }

        public static class Global
        {
            public static int idUsuario ;
            
        }
        public async Task<IActionResult> Salir()
        {
            
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Acceso");
        }

        public async Task<IActionResult> Details()
        {
            return View();
        }


    }
}
