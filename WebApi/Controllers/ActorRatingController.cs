// ActorRatingController.cs
using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorRatingController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public ActorRatingController(
            IDataService dataService,
            LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        
        [HttpGet(Name = nameof(GetActorRating))]
        public ActionResult<IEnumerable<ActorRatingModel>> GetActorRating(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var ActorRatingList = _dataService.GetActorRatingList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreateActorRatingModel) // Convert to ActorRatingModel with URL
                .ToList();

            var totalItems = _dataService.GetActorRatingCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<ActorRatingModel>
            {
                Items = ActorRatingList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetActorRating), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetActorRating), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };
            
            return Ok(result);
        }

        
        [HttpGet("{nConst}", Name = nameof(GetActorRatingById))]
        public ActionResult<ActorRatingModel> GetActorRatingById(string nConst)
        {
            var rating = _dataService.GetActorRatingById(nConst);
            
            if (rating == null)
                return NotFound();
            
            var model = CreateActorRatingModel(rating);
            return Ok(model);
        }

        
        [HttpPost]
        public ActionResult<ActorRatingModel> AddActorRating([FromBody] ActorRating newActorRating)
        {
            var createdRating = _dataService.AddActorRating(newActorRating);

            if (createdRating == null)
            {
                return BadRequest("Could not add actor rating to the database.");
            }
            
            var model = CreateActorRatingModel(createdRating);
            return CreatedAtAction(nameof(GetActorRatingById), new { nConst = createdRating.NConst }, model);
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


        private ActorRatingModel CreateActorRatingModel(ActorRating actorRating)
        {
            return new ActorRatingModel
            {
                NConst = actorRating.NConst,
                ARating = actorRating.ARating,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetActorRatingById),
                    new { nConst = actorRating.NConst })
            };
        }
    }
}
