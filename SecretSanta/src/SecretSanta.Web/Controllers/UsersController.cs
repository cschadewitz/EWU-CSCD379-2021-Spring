using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Data;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        // GET: UsersController
        public IActionResult Index()
        {
            return View(MockData.Users);
        }
        // GET: UsersController/Create
        public IActionResult Create()
        {
            return View();
        }
        //POST: UsersController/Create
        [HttpPost]
        public IActionResult Create(UserViewModel user)
        {
            if (ModelState.IsValid && user is not null)
            {
                MockData.Users.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        // GET: UsersController/Edit/5
        public IActionResult Edit(int id)
        {
            UserViewModel user = MockData.Users[id];
            if(user is not null)
                return View(user);
            return RedirectToAction(nameof(Index));
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        public IActionResult Edit(UserViewModel user)
        {
            if (ModelState.IsValid && user is not null)
            {
                MockData.Users[user.Id] = user;
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            UserViewModel user = MockData.Users[id];
            if(user is not null)
                MockData.Users.Remove(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
