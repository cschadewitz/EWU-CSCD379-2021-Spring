using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Data;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class GroupsController : Controller
    {
        // GET: GroupsController
        public IActionResult Index()
        {
            return View(MockData.Groups);
        }

        // GET: GroupsController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: GroupsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GroupsController/Create
        [HttpPost]
        public IActionResult Create(GroupViewModel group)
        {
            if(ModelState.IsValid)
            {
                group.Id = MockData.GroupsNextId();
                MockData.Groups.Add(group);
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        // GET: GroupsController/Edit/5
        public IActionResult Edit(int id)
        {
            return View(MockData.Groups.Where(g => g.Id == id).FirstOrDefault());
        }


        // POST: GroupsController/Edit/5
        [HttpPost]
        public IActionResult Edit(GroupViewModel group)
        {
            if (ModelState.IsValid)
            {
                GroupViewModel updatedGroup = MockData.Groups.Where(g => g.Id == group.Id).FirstOrDefault();
                updatedGroup.GroupName = group.GroupName;
                return RedirectToAction(nameof(Index));
            }

            return View(group);
        }
        // POST: GroupsController/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (MockData.Groups.Where(g => g.Id == id).FirstOrDefault() is not null)
                MockData.Groups.Remove(MockData.Groups.Where(g => g.Id == id).FirstOrDefault());
            return RedirectToAction(nameof(Index));
        }
    }
}
