using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        public UsersClient Client { get; }
        public IMapper Mapper { get; }
        public UsersController(UsersClient client, IMapper mapper)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IActionResult> Index()
        {
            ICollection<UserDTO> users = await Client.GetAllAsync();
            ICollection<UserViewModel> userViewModels = Mapper.Map<ICollection<UserDTO>, ICollection<UserViewModel>>(users);
            return View(userViewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = await Client.PostAsync(Mapper.Map<UserViewModel, UserDTO>(viewModel));
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            UserDTO user = await Client.GetAsync(id);
            UserViewModel userViewModel = Mapper.Map<UserDTO, UserViewModel>(user);
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await Client.PutAsync(viewModel.Id, Mapper.Map<UserViewModel, UserDTO>(viewModel));
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await Client.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
