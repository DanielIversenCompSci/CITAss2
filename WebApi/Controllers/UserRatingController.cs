using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRatingController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public UserRatingController(
            IDataService dataService,
            LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }
        
        // GET all Titles with pagination
        [HttpGet(Name = nameof(GetUserRating))]
        public ActionResult<IEnumerable<UserRatingModel>> GetUserRating(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var UserRatingList = _dataService.GetUserRatingsList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreateUserRatingModel) // Convert to UserRatingModel with URL
                .ToList();

            var totalItems = _dataService.GetUserRatingsCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<UserRatingModel>
            {
                Items = UserRatingList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetUserRating), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetUserRating), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };
            
            return Ok(result);
        }

        // Get specific title by TConst
        [HttpGet("{userId}/{tConst}", Name = nameof(GetUserRatingById))]
        public ActionResult<UserRatingModel> GetUserRatingById(string userId, string tConst)
        {
            var title = _dataService.GetUserRatingById(userId, tConst);

            if (title == null)
            {
                return NotFound();
            }

            var model = CreateUserRatingModel(title);
            return Ok(model);
        }

        // POST: Create a new UserRating entry
        [HttpPost]
        public ActionResult<UserRatingModel> CreateUserRating([FromBody] UserRating newTitle)
        {
            var createdTitle = _dataService.AddUserRating(newTitle);

            if (createdTitle == null)
            {
                return BadRequest("An entry with this TConst already exists.");
            }

            var model = CreateUserRatingModel(createdTitle);
            return CreatedAtAction(nameof(GetUserRatingById), new { userId = createdTitle.UserId, tConst = createdTitle.TConst }, model);
        }

        
        [HttpPut("{userId}/{tConst}")]
        public IActionResult UpdateUserRating(string userId, string tConst, [FromBody] UserRating updatedRating)
        {
            var success = _dataService.UpdateUserRating(userId, tConst, updatedRating);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        
        [HttpDelete("{userId}/{tConst}")]
        public IActionResult DeleteUserRating(string userId, string tConst)
        {
            var success = _dataService.DeleteUserRating(userId, tConst);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }


        private UserRatingModel CreateUserRatingModel(UserRating userRating)
        {
            return new UserRatingModel
            {
                UserId = userRating.UserId,
                TConst = userRating.TConst,
                Rating = userRating.Rating,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetUserRatingById),
                    new { userId = userRating.UserId, tConst = userRating.TConst })
            };
        }
    }
}
