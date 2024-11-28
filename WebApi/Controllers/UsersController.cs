using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public UsersController(
            IDataService dataService,
            LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        // GET all Users with pagination
        [HttpGet(Name = nameof(GetUsers))]
        public ActionResult<IEnumerable<UsersModel>> GetUsers(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var UsersList = _dataService.GetUsersList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreateUsersModel) // Convert to UsersModel with URL
                .ToList();

            var totalItems = _dataService.GetUsersCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<UsersModel>
            {
                Items = UsersList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetUsers), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetUsers), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };
            
            return Ok(result);
        }

        // GET: api/Users/{id}
        [HttpGet("{id}", Name = nameof(GetUserById))]
        public ActionResult<Users> GetUserById(string id)
        {
            var user = _dataService.GetUserById(id);
            
            if (user == null)
            {
                return NotFound();
            }
            
            var model = CreateUsersModel(user);
            return Ok(model);
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

            if (createdUser == null)
            {
                return BadRequest("Could not create user");
            }
            
            var model = CreateUsersModel(createdUser);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, model);
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
        
        
        private UsersModel CreateUsersModel(Users user)
        {
            return new UsersModel
            {
                UserId = user.UserId,
                Email = user.Email,
                Password = user.Password,
                SearchHistory = user.SearchHistory,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetUserById), new { id = user.UserId })
            };
        }
    }
}
