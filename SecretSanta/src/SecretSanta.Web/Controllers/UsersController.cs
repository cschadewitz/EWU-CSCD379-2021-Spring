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

        // GET: UsersController/Details/5
        public IActionResult Details(int id)
        {
            return View();
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
            if (ModelState.IsValid)
            {
                user.Id = MockData.UsersNextId;
                MockData.Users.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        // GET: UsersController/Edit/5
        public IActionResult Edit(int id)
        {
            return View(MockData.Users.Where(u => u.Id == id).FirstOrDefault());
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        public IActionResult Edit(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                UserViewModel updatedUser = MockData.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                updatedUser.FirstName = user.FirstName;
                updatedUser.LastName = user.LastName;
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(MockData.Users.Where(u => u.Id == id).FirstOrDefault() is not null)
                MockData.Users.Remove(MockData.Users.Where(u => u.Id == id).FirstOrDefault());
            return RedirectToAction(nameof(Index));
        }

        //// POST: UsersController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
