using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Data;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class GiftsController : Controller
    {
        public IActionResult Index()
        {
            var gifts = MockData.Gifts.OrderBy(g => g.Priority).ToList();
            return View(gifts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GiftViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Id = MockData.Gifts.Max(g => g.Id) + 1;
                MockData.Gifts.Add(viewModel);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            return View(MockData.Gifts.Single(g => g.Id == id));
        }

        [HttpPost]
        public IActionResult Edit(GiftViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                MockData.Gifts[MockData.Gifts.FindIndex(g => g.Id == viewModel.Id)] = viewModel;
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            MockData.Gifts.Remove(MockData.Gifts.Single(g => g.Id == id));
            return RedirectToAction(nameof(Index));
        }
    }
}