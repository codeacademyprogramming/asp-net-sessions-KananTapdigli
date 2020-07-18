using ASPSession.Contexts;
using ASPSession.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ASPSession.Controllers
{
    public class AuthController : Controller
    {
        private readonly EducationContext db = new EducationContext();


        // GET: Auth

        public ActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Index(User user)
        {
            ModelState.Clear();

            if (db.users.Any(u => u.Username == user.Username))
            {
                ModelState.AddModelError("Username", "This Username has already been registered!");
            }

            if (db.users.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "This Email has already been registered!");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    string hashedPassword = Crypto.HashPassword(user.Password);
                    user.Password = hashedPassword;
                    user.confirmPassword = hashedPassword;

                    db.users.Add(user);

                    db.SaveChanges();

                    Session["SignUp"] = true;

                    return RedirectToAction("SignIn");
                }
            }

            return View(user);

        }


        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn([Bind(Include = "Username,Password")] User user)
        {
            User activeUser = db.users.FirstOrDefault(u => u.Username.ToLower() == user.Username.ToLower());

            if (activeUser != null)
            {
                string hashedPassword = activeUser.Password;

                if (Crypto.VerifyHashedPassword(hashedPassword, user.Password))
                {
                    Session["Login"] = true;
                    Session["UserID"] = activeUser.ID;
                    Session["Username"] = activeUser.Username;

                    return RedirectToAction("Index","Home");
                }
            }

            ModelState.AddModelError("UserPass", "Username or Password is not Correct!");

            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}