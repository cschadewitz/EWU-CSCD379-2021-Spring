﻿
using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }
    }
}
