using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleRatingsController : ControllerBase
    {
        private readonly IDataService _dataService;

        public TitleRatingsController(IDataService dataService)
        {
            _dataService = dataService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<TitleRatings>> GetTitleRatingsList([FromQuery] int limit = 100)
        {
            var ratingsList = _dataService.GetTitleRatingsList(limit);
            return Ok(ratingsList);
        }

        [HttpGet("{tConst}")]
        public ActionResult<TitleRatings> GetTitleRatingById(string tConst)
        {
            var rating = _dataService.GetTitleRatingById(tConst);

            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        [HttpPost]
        public ActionResult<TitleRatings> CreateTitleRating([FromBody] TitleRatings newTitleRating)
        {
            var createdRating = _dataService.AddTitleRating(newTitleRating);
            return CreatedAtAction(nameof(GetTitleRatingById), new { tConst = createdRating.TConst }, createdRating);
        }

        [HttpPut("{tConst}")]
        public IActionResult UpdateTitleRating(string tConst, [FromBody] TitleRatings updatedTitleRating)
        {
            var success = _dataService.UpdateTitleRating(tConst, updatedTitleRating);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{tConst}")]
        public IActionResult DeleteTitleRating(string tConst)
        {
            var success = _dataService.DeleteTitleRating(tConst);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
