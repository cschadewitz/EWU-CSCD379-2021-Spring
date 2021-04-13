using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

        // GET: GroupsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GroupsController/Create
        [HttpPost]
        public IActionResult Create(GroupViewModel group)
        {
            if(ModelState.IsValid && group is not null)
            {
                MockData.Groups.Add(group);
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        // GET: GroupsController/Edit/5
        public IActionResult Edit(int id)
        {
            GroupViewModel group = MockData.Groups[id];
            if (group is not null)
                return View(group);
            return RedirectToAction(nameof(Index));
        }


        // POST: GroupsController/Edit/5
        [HttpPost]
        public IActionResult Edit(GroupViewModel group)
        {
            if (ModelState.IsValid && group is not null)
            {
                MockData.Groups[group.Id] = group;
                return RedirectToAction(nameof(Index));
            }

            return View(group);
        }
        // POST: GroupsController/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            GroupViewModel group = MockData.Groups[id];
            if (group is not null)
                MockData.Groups.Remove(group);
            return RedirectToAction(nameof(Index));
        }
    }
}
