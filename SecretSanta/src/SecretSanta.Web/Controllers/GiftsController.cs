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

        // GET: GiftsController/Details/5
        public IActionResult Details(int id)
        {
            return View();
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
            if (ModelState.IsValid)
            {
                gift.Id = MockData.GiftsNextId();
                MockData.Gifts.Add(gift);
                return RedirectToAction(nameof(Index));
            }
            return View(gift);
        }

        // GET: GiftsController/Edit/5
        public IActionResult Edit(int id)
        {
            return View(MockData.Gifts.Where(g => g.Id == id).FirstOrDefault());
        }

        // POST: GiftsController/Edit/5
        [HttpPost]
        public IActionResult Edit(GiftViewModel gift)
        {
            if (ModelState.IsValid)
            {
                GiftViewModel updatedGift = MockData.Gifts.Where(g => g.Id == gift.Id).FirstOrDefault();
                updatedGift.Title = gift.Title;
                updatedGift.Description = gift.Description;
                updatedGift.Priority = gift.Priority;
                updatedGift.Url = gift.Url;
                updatedGift.UserId = gift.UserId;
                return RedirectToAction(nameof(Index));
            }

            return View(gift);
        }
        // POST: GiftsController/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            GiftViewModel gift = MockData.Gifts.Where(g => g.Id == id).FirstOrDefault();
            if (gift is not null)
                MockData.Gifts.Remove(gift);
            return RedirectToAction(nameof(Index));
        }
    }
}
