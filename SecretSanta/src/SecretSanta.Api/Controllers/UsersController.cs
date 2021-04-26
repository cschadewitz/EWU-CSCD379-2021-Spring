using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository UserRepository { get; }
        
        public UsersController(IUserRepository userRepository)
        {
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        // GET: /api/users/
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return UserRepository.List();
        }

        // GET: /api/users/{id}
        [HttpGet("{id}")]
        public ActionResult<User?> Get(int id)
        {
            User? user = UserRepository.GetItem(id);
            if (user is null)
                return NotFound();
            return user;
        }

        // POST: /api/users/
        [HttpPost]
        public ActionResult Post([FromBody] User? user)
        {
            if (user is null)
                return BadRequest();
            UserRepository.Create(user);
            return Ok();
        }

        // PUT: /api/users/{id}
        [HttpPut("{id}")]
        public ActionResult Edit(int id, [FromBody]User editedUser)
        {
            User? user = UserRepository.GetItem(id);
            if (user is null)
                return NotFound();
            if (!string.IsNullOrWhiteSpace(editedUser.FirstName) && !string.IsNullOrWhiteSpace(editedUser.LastName))
            {
                user.FirstName = editedUser.FirstName;
                user.LastName = editedUser.LastName;
            }
            UserRepository.Save(user);
            return Ok();
        }

        // DELETE: /api/users/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (UserRepository.Remove(id))
                return Ok();
            return NotFound();
        }
    }
}
