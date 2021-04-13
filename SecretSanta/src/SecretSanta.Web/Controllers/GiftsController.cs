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
    public class GiftsController : Controller
    {
        // GET: GiftsController
        public IActionResult Index()
        {
            return View(MockData.Gifts);
        }
        // GET: GiftsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GiftsController/Create
        [HttpPost]
        public IActionResult Create(GiftViewModel gift)
        {
            if (ModelState.IsValid && gift is not null)
            {
                MockData.Gifts.Add(gift);
                return RedirectToAction(nameof(Index));
            }
            return View(gift);
        }

        // GET: GiftsController/Edit/5
        public IActionResult Edit(int id)
        {
            GiftViewModel gift = MockData.Gifts[id];
            if (gift is not null)
                return View(gift);
            return RedirectToAction(nameof(Index));
        }

        // POST: GiftsController/Edit/5
        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", 
            "CA1062:Validate arguments of public methods", 
            Justification = "GiftViewModel argument is validated with ModelState.IsValid and null check")]
        public IActionResult Edit(GiftViewModel gift)
        {
            if (ModelState.IsValid && gift is not null)
            {
                MockData.Gifts[gift.Id] = gift;
                return RedirectToAction(nameof(Index));
            }
            return View(gift);
        }
        // POST: GiftsController/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            GiftViewModel gift = MockData.Gifts[id];
            if (gift is not null)
                MockData.Gifts.Remove(gift);
            return RedirectToAction(nameof(Index));
        }
    }
}
