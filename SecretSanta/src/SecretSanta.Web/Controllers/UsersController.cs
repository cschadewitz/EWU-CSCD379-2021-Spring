using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        public IUsersClient UserClient { get; }

        public UsersController(IUsersClient userClient)
        {
            UserClient = userClient ?? throw new ArgumentNullException(nameof(userClient));
        }

        public async Task<IActionResult> Index()
        {
            ICollection<User>? users = await UserClient.GetAllAsync();
            var userViewModels = users.Select(x => new UserViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToList();
            return View(userViewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Null check is performed but not recognized by analyzer")]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid && viewModel is not null)
            {
                await UserClient.PostAsync(new User
                {
                    Id = viewModel.Id,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName
                });
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            User user = await UserClient.GetAsync(id);
            return View(new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
        }

        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Null check is performed but not recognized by analyzer")]
        public async Task<IActionResult> Edit(UserViewModel viewModel)
        {
            if (ModelState.IsValid && viewModel is not null)
            {
                await UserClient.PutAsync(viewModel.Id, new UpdateUser
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName
                });
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await UserClient.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
