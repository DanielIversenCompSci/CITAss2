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
        
        // ********** **********
        // This controller dosen't have any ENDPOINTs used by the frontend
        // However the group decided to keep them just for showcase of basic CRUD for all object
        // Fully aware they are not used for anything at the current state of the application
        // ********** **********
        // Endpoints NOT IN USE
        // ********** **********
        
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
        [HttpGet("{userRatingId}", Name = nameof(GetUserRatingById))]
        public ActionResult<UserRatingModel> GetUserRatingById(int userRatingId)
        {
            var title = _dataService.GetUserRatingById(userRatingId);

            if (title == null)
            {
                return NotFound();
            }

            var model = CreateUserRatingModel(title);
            return Ok(model);
        }

        // POST: Create a new UserRating entry
        [HttpPost]
        public ActionResult<UserRatingModel> CreateUserRating([FromBody] UserRatingCreateModel newRating)
        {

            var ratingEntity = new UserRating
            {
                UserId = newRating.UserId,
                TConst = newRating.TConst,
                Rating = newRating.Rating
            };
            
            var createdTitle = _dataService.AddUserRating(ratingEntity);

            if (createdTitle == null)
            {
                return BadRequest("An entry with this TConst already exists.");
            }

            var model = CreateUserRatingModel(createdTitle);
            return CreatedAtAction(nameof(GetUserRatingById), new { userId = createdTitle.UserId, tConst = createdTitle.TConst }, model);
        }

        
        [HttpPut("{userRatingId}")]
        public IActionResult UpdateUserRating(int userRatingId, [FromBody] UserRatingCreateModel updatedRating)
        {
            var updatedEntity = new UserRating
            {
                UserId = updatedRating.UserId,
                TConst = updatedRating.TConst,
                Rating = updatedRating.Rating
            };
            
            var success = _dataService.UpdateUserRating(userRatingId, updatedEntity);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        
        [HttpDelete("{userRatingId}")]
        public IActionResult DeleteUserRating(int userRatingId)
        {
            var success = _dataService.DeleteUserRating(userRatingId);

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
                    new { userRatingId = userRating.UserRatingId })
            };
        }
    }
}
