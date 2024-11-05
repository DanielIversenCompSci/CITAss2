using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrimaryProfessionController : ControllerBase
    {
        private readonly IDataService _dataService;

        // Inject the data service
        public PrimaryProfessionController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET all Titles, limited by page or it wont load in swagger...
        // GET: api/PrimaryProfession
        [HttpGet]
        public ActionResult<IEnumerable<PrimaryProfession>> GetPrimaryProfession(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var PrimaryProfessionList = _dataService.GetPrimaryProfessionList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(PrimaryProfessionList);
        }

        // Get specific title by Nconst
        // GET: api/PrimaryProfession/{id}
        [HttpGet("{id}")]
        public ActionResult<PrimaryProfession> GetPrimaryProfessionById(string id)
        {
            var title = _dataService.GetPrimaryProfessionById(id);

            if (title == null)
            {
                return NotFound();
            }

            return Ok(title);
        }

        
        // POST: api/PrimaryProfession
        [HttpPost]
        public ActionResult<PrimaryProfession> CreatePrimaryProfession([FromBody] PrimaryProfession newTitle)
        {
            var createdTitle = _dataService.AddPrimaryProfession(newTitle);

            if (createdTitle == null)
            {
                return BadRequest("An entry with this NConst already exists.");
            }

            return CreatedAtAction(nameof(GetPrimaryProfessionById), new { id = createdTitle.NConst }, createdTitle);
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
    }
}
