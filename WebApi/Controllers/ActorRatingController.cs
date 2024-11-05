// ActorRatingController.cs
using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorRatingController : ControllerBase
    {
        private readonly IDataService _dataService;

        public ActorRatingController(IDataService dataService)
        {
            _dataService = dataService;
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<ActorRating>> GetActorRatingList([FromQuery] int limit = 100)
        {
            var ratings = _dataService.GetActorRatingList(limit);
            return Ok(ratings);
        }

        
        [HttpGet("{nConst}")]
        public ActionResult<ActorRating> GetActorRatingById(string nConst)
        {
            var rating = _dataService.GetActorRatingById(nConst);
            if (rating == null)
                return NotFound();
            return Ok(rating);
        }

        
        [HttpPost]
        public ActionResult<ActorRating> AddActorRating([FromBody] ActorRating newActorRating)
        {
            var createdRating = _dataService.AddActorRating(newActorRating);
            return CreatedAtAction(nameof(GetActorRatingById), new { nConst = createdRating.NConst }, createdRating);
        }

        
        [HttpPut("{nConst}")]
        public IActionResult UpdateActorRating(string nConst, [FromBody] ActorRating updatedActorRating)
        {
            var success = _dataService.UpdateActorRating(nConst, updatedActorRating);
            if (!success)
                return NotFound();
            return NoContent();
        }

        
        [HttpDelete("{nConst}")]
        public IActionResult DeleteActorRating(string nConst)
        {
            var success = _dataService.DeleteActorRating(nConst);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
