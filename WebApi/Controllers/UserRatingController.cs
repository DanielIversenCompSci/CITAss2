using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRatingsController : ControllerBase
    {
        private readonly IDataService _dataService;

        public UserRatingsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<UserRating>> GetUserRatings()
        {
            var ratingsList = _dataService.GetUserRatingsList();
            return Ok(ratingsList);
        }

        
        [HttpGet("{userId}/{tConst}")]
        public ActionResult<UserRating> GetUserRatingById(string userId, string tConst)
        {
            var rating = _dataService.GetUserRatingById(userId, tConst);

            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        
        [HttpPost]
        public ActionResult<UserRating> CreateUserRating([FromBody] UserRating newUserRating)
        {
            var createdRating = _dataService.AddUserRating(newUserRating);

            if (createdRating == null)
            {
                return BadRequest("User rating for this movie already exists.");
            }

            return CreatedAtAction(nameof(GetUserRatingById), new { userId = createdRating.UserId, tConst = createdRating.TConst }, createdRating);
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
    }
}
