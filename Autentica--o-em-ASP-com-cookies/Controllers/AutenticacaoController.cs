using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppAutenticacao.Models;
using AppAutenticacao.ViewModel;
using AppAutenticacao.Utils;
using System.Security.Claims;

namespace AppAutenticacao.Controllers
{
    public class AutenticacaoController : Controller
    {
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(UsuCadViewModel usuarioVM)
        {
            if (!ModelState.IsValid)
            {
                return View(usuarioVM);
            }
            Usuario newUsuario = new Usuario
            {
                UsuNome = usuarioVM.UsuNome,
                Login = usuarioVM.Login,
                Senha = Hash.GerarHash(usuarioVM.Senha)
            };
            newUsuario.InsertUsuario(newUsuario);
            return RedirectToAction("Login","Autenticacao");
        }

        public ActionResult SelectLogin(string Login)
        {
            bool loginExists;
            string login = new Usuario().SelectUsuLogin(Login);
            if(login.Length == 0)
            {
                loginExists = false;
            }
            else
            {
                loginExists = true;
            }
            return Json(!loginExists, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identity = User.Identity as ClaimsIdentity;
            var login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;

            Usuario usuario = new Usuario();
            usuario = usuario.SelectUsuario(login);

            if (Hash.GerarHash(viewmodel.SenhaAtual) != usuario.Senha)
            {
                ModelState.AddModelError("SenhaAtual", "Senha incorreta");
                return View();
            }

            if (Hash.GerarHash(viewmodel.NovaSenha) == usuario.Senha)
            {
                ModelState.AddModelError("NovaSenha", "A senha nova é igual a antiga");
                return View();
            }

            usuario.Senha = Hash.GerarHash(viewmodel.NovaSenha);

            usuario.UpdateSenha(usuario);

            return RedirectToAction("Index", "Home");
        }
        public ActionResult CheckSenha(string Login, string OldPass)
        {
            Usuario TempUsuario = new Usuario();
            string checkPass = TempUsuario.SelectUsuario(Login).ToString();
            bool passSame;

            if(OldPass == checkPass)
            {
                passSame = true;
            }
            else
            {
                passSame = false;
            }
            return Json(passSame, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Login(string ReturnUrl)
        {
            var viewmodel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };
            return View(viewmodel); 
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }

            Usuario usuario = new Usuario();
            usuario = usuario.SelectUsuario(viewmodel.Login);

            if (usuario == null | usuario.Login != viewmodel.Login)
            {
                ModelState.AddModelError("Login", "Login incorreto");
                return View(viewmodel);
            }
            if (usuario.Senha != Hash.GerarHash(viewmodel.Senha))
            {
                ModelState.AddModelError("Senha", "Senha incorreta");
                return View(viewmodel);
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Login),
                new Claim("Login", usuario.Login)
            }, "AppAplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identity);


            if (!String.IsNullOrWhiteSpace(viewmodel.UrlRetorno) || Url.IsLocalUrl(viewmodel.UrlRetorno))
                return Redirect(viewmodel.UrlRetorno);
            else
                return RedirectToAction("Index", "Administrativo");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("AppAplicationCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}