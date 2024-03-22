using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using KDSB_JMRH_NNNN_NDMM.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;

namespace KDSB_JMRH_NNNN_NDMM.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;


        public LoginController(ApplicationDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Login(string ReturnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        // POST: User/Login
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password")] users user, string ReturnUrl)
        {
            user.Password = CalcularHashMD5(user.Password);
            var usuarioAut = await _context.users.FirstOrDefaultAsync(s => s.Email == user.Email && s.Password == user.Password && s.Status == 1);
            if (usuarioAut?.Id > 0 && usuarioAut.Email == user.Email)
            {
                var claims = new[] {
                    new Claim(ClaimTypes.Name, usuarioAut.Email),
                     //new Claim(ClaimTypes.Role, usuarioAut.Rol),
                    new Claim("Id", usuarioAut.Id.ToString())
                    };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true }); ;
                var result = User.Identity.IsAuthenticated;
                if (!string.IsNullOrWhiteSpace(ReturnUrl))
                    return Redirect(ReturnUrl);
                else
                    return RedirectToAction("Index", "users");
            }
            else
                ViewBag.Error = "Credenciales incorrectas";
            ViewBag.pReturnUrl = ReturnUrl;
            return View(user);

        }


        public async Task<IActionResult> SalirU()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
        private string CalcularHashMD5(string texto)
        {
            using (MD5 md5 = MD5.Create())
            {
                // Convierte la cadena de texto a bytes
                byte[] inputBytes = Encoding.UTF8.GetBytes(texto);

                // Calcula el hash MD5 de los bytes
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convierte el hash a una cadena hexadecimal
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
