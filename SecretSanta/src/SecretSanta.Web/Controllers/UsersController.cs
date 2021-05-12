using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Data;
using SecretSanta.Web.Api;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        public IUsersClient Client { get; }
        public IMapper Mapper { get; }
        public UsersController(IUsersClient usersClient, IMapper mapper)
        {
            Client = usersClient ?? throw new ArgumentNullException(nameof(usersClient));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            
        }
        public async Task<IActionResult> Index()
        {            
            ICollection<UserDTO> users = await Client.GetAllAsync();
            ICollection<UserViewModel> userViewModels = Mapper.Map<ICollection<UserDTO>, ICollection<UserViewModel>>(users);
            return View(userViewModels);
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
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid && viewModel is not null)
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
            if (ModelState.IsValid && viewModel is not null)
            {
                await Client.PutAsync(viewModel.Id, Mapper.Map<UserViewModel, UserDTO>(viewModel));
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await Client.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
