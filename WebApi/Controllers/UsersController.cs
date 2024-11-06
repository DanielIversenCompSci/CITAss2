using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDataService _dataService;

        public UsersController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<Users>> GetUsersList()
        {
            var users = _dataService.GetUsersList();
            return Ok(users);
        }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public ActionResult<Users> GetUserById(string id)
        {
            var user = _dataService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            var response = new
            {
                user,
                _links = new
                {
                    self = Url.Action(nameof(GetUserById), new { id = user.UserId }),
                    SearchHistory = Url.Action(nameof(GetUserWithSearchHistory), new { id = user.UserId }),
                }
            };
            return Ok(response);
        }
        // GET: api/Users/{id}/searchhistory
        [HttpGet("{id}/searchhistory")]
        public ActionResult<Users> GetUserWithSearchHistory(string id)
        {
            var user = _dataService.GetUserWithSearchHistory(id);
            if (user == null)
            {
                return NotFound();
            }
            var response = new
            {
                user,
                _links = new
                {
                    self = Url.Action(nameof(GetUserById), new { id = user.UserId }),
                    SearchHistory = Url.Action(nameof(GetUserWithSearchHistory), new { id = user.UserId }),
                }
            };
            return Ok(response);
        }
    

        // POST: api/Users
        [HttpPost]
        public ActionResult<Users> AddUser([FromBody] Users newUser)
        {
            var createdUser = _dataService.AddUser(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
        }

        // PUT: api/Users/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateUser(string id, [FromBody] Users updatedUser)
        {
            if (!_dataService.UpdateUser(id, updatedUser)) return NotFound();
            return NoContent();
        }

        // DELETE: api/Users/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(string id)
        {
            if (!_dataService.DeleteUser(id)) return NotFound();
            return NoContent();
        }
    }
}
