using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrimaryProfessionController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public PrimaryProfessionController(
            IDataService dataService,
            LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        // GET all Titles, limited by page or it wont load in swagger...
        // GET: api/PrimaryProfession
        [HttpGet(Name = nameof(GetPrimaryProfession))]
        public ActionResult<IEnumerable<PrimaryProfessionModel>> GetPrimaryProfession(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var PrimaryProfessionList = _dataService.GetPrimaryProfessionList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreatePrimaryProfessionModel) // Convert to PrimaryProfessionModel with URL
                .ToList();

            var totalItems = _dataService.GetPrimaryProfessionCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<PrimaryProfessionModel>
            {
                Items = PrimaryProfessionList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetPrimaryProfession), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetPrimaryProfession), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };
            
            return Ok(result);
        }

        // Get specific title by Nconst
        // GET: api/PrimaryProfession/{id}
        [HttpGet("{id}", Name = nameof(GetPrimaryProfessionById))]
        public ActionResult<PrimaryProfessionModel> GetPrimaryProfessionById(string id)
        {
            var title = _dataService.GetPrimaryProfessionById(id);

            if (title == null)
            {
                return NotFound();
            }

            var model = CreatePrimaryProfessionModel(title);
            return Ok(model);
        }

        
        // POST: api/PrimaryProfession
        [HttpPost]
        public ActionResult<PrimaryProfessionModel> CreatePrimaryProfession([FromBody] PrimaryProfession newTitle)
        {
            var createdTitle = _dataService.AddPrimaryProfession(newTitle);

            if (createdTitle == null)
            {
                return BadRequest("An entry with this NConst already exists.");
            }

            var model = CreatePrimaryProfessionModel(createdTitle);
            return CreatedAtAction(nameof(GetPrimaryProfessionById), new { id = createdTitle.NConst }, model);
        }

        
        // PUT: api/PrimaryProfession/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePrimaryProfession(string id, [FromBody] PrimaryProfession updatedTitle)
        {
            var success = _dataService.UpdatePrimaryProfession(id, updatedTitle);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }

        // DELETE: api/PrimaryProfession/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePrimaryProfession(string id)
        {
            var success = _dataService.DeletePrimaryProfession(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }
        
        
        // Helper method to create a model for the object
        private PrimaryProfessionModel CreatePrimaryProfessionModel(PrimaryProfession title)
        {
            return new PrimaryProfessionModel
            {
                NConst = title.NConst,
                Role = title.Role,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetPrimaryProfessionById), new { id = title.NConst })
            };
        }
    }
}
