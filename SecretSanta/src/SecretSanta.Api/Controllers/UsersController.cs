using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.DTO;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository Repository { get; }
        private IMapper Mapper { get; }

        public UsersController(IUserRepository repository, IMapper mapper)
        {
            Repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
            Mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), StatusCodes.Status200OK)]
        public IEnumerable<UserDTO> Get()
        {
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(Repository.List());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        public ActionResult<UserDTO?> Get(int id)
        {
            User? user = Repository.GetItem(id);
            if (user is null) 
                return NotFound();
            return Mapper.Map<User, UserDTO>(user);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(int id)
        {
            if (Repository.Remove(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        public ActionResult<UserDTO?> Post([FromBody] UserDTO? user)
        {
            if (user is null)
            {
                return BadRequest();
            }
            return Mapper.Map<User, UserDTO>(Repository.Create(Mapper.Map<UserDTO, User>(user)));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Put(int id, [FromBody] UserDTO? user)
        {
            if (user is null)
            {
                return BadRequest();
            }

            User? foundUser = Repository.GetItem(id);
            if (foundUser is not null)
            {
                foundUser.FirstName = user.FirstName ?? "";
                foundUser.LastName = user.LastName ?? "";

                Repository.Save(foundUser);
                return Ok();
            }
            return NotFound();
        }
    }
}
