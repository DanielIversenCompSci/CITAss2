using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        [HttpGet("{userId}", Name = nameof(GetUserById))]
        public ActionResult<Users> GetUserById(int userId)
        {
            var user = _dataService.GetUserById(userId);
            
            if (user == null)
            {
                return NotFound();
            }
            
            var model = CreateUsersModel(user);
            return Ok(model);
        }
        

        // POST: api/Users
        [HttpPost]
        public ActionResult<Users> AddUser([FromBody] UsersCreateModel newUser)
        {

            if (string.IsNullOrWhiteSpace(newUser.Email) /*|| string.IsNullOrWhiteSpace(newUser.Username) */|| string.IsNullOrWhiteSpace(newUser.Password))
            {
                return BadRequest("Email, Username and Password are required.");
            }

            try
            {
                if (_dataService.GetUsersList().Any(u => u.Email == newUser.Email))
                {
                    return Conflict("A user with this email already exists.");
                }

                // Check if the username already exists
                if (_dataService.GetUsersList().Any(u => u.Username == newUser.Username))
                {
                    return Conflict("A user with this username already exists.");
                }

                var userEntity = new Users
                {
                    //UserId = newUser.UserId,
                    Email = newUser.Email,
                    Username = newUser.Username,
                    Password = newUser.Password
                };

                var createdUser = _dataService.AddUser(userEntity);

                if (createdUser == null)
                {
                    return BadRequest("Could not create user");
                }

                var model = CreateUsersModel(createdUser);
                return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.UserId }, model);
            }
            catch(Exception ex)
            {
                return BadRequest($"Internal server error: {ex.Message}");
            }
           
        }


        // PUT: api/Users/{id}
        [HttpPut("{userId}")]
        public ActionResult UpdateUser(int userId, [FromBody] UsersCreateModel updatedUser)
        {
            var updatedEntity = new Users
            {
                //UserId = updatedUser.UserId,
                Email = updatedUser.Email,
                Username = updatedUser.Username,
                Password = updatedUser.Password
            };
            
            var succes = _dataService.UpdateUser(userId, updatedEntity);

            if (!succes)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        [HttpPost("login")]
        public ActionResult LoginUser([FromBody] LoginModel loginModel)
        {
            if (string.IsNullOrWhiteSpace(loginModel.Email) || string.IsNullOrWhiteSpace(loginModel.Password))
            {
                return (BadRequest("Email and Password are required"));
            }

            var user = _dataService.GetUsersList().FirstOrDefault(u => u.Email == loginModel.Email);

            var isAuthenticated = _dataService.LoginUser(loginModel.Email, loginModel.Password);

            if (!isAuthenticated)
            {
                return Unauthorized("Invalid Email or Password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("vaeP+GUisPHhX+33WaDHzxCULyUd/zC+OTMRZJyWjzU=");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim ("UserId", user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);


            return Ok(new {
                token = jwt,
                userId = user.UserId,
                email = user.Email,
                username = user.Username
            });
        }

        // DELETE: api/Users/{id}
        [HttpDelete("{userId}")]
        public ActionResult DeleteUser(int userId)
        {
            var success = _dataService.DeleteUser(userId);
            
            if (!success)
            {
                return NotFound("Could not delete user");
            }
            
            return NoContent();
        }
        
        
        
        
        private UsersModel CreateUsersModel(Users user)
        {
            return new UsersModel
            {
                UserId = user.UserId,
                Email = user.Email,
                Password = user.Password,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetUserById), new { userId = user.UserId })
            };
        }
    }
}
