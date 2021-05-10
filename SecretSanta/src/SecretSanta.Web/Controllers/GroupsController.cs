using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Data;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class GroupsController : Controller
    {
        public IActionResult Index()
        {
            return View(MockData.Groups);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GroupViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                MockData.Groups.Add(viewModel);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            return View(MockData.Groups[id]);
        }

        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Null check is performed but not recognized by analyzer")]
        public IActionResult Edit(GroupViewModel viewModel)
        {
            if (ModelState.IsValid && viewModel is not null)
            {
                MockData.Groups[viewModel.Id] = viewModel;
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            MockData.Groups.RemoveAt(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
