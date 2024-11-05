using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleAkasController : ControllerBase
    {
        private readonly IDataService _dataService;

        // Inject the data service
        public TitleAkasController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET all Titles, limited by page or it wont load in swagger...
        // GET: api/TitleAkas
        [HttpGet]
        public ActionResult<IEnumerable<TitleAkas>> GetTitleAkas(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var TitleAkasList = _dataService.GetTitleAkasList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(TitleAkasList);
        }

        // Get specific title by TConst
        // GET: api/TitleAkas/{id}
        [HttpGet("{id}")]
        public ActionResult<TitleAkas> GetTitleAkasById(string id)
        {
            var title = _dataService.GetTitleAkasById(id);

            if (title == null)
            {
                return NotFound();
            }

            return Ok(title);
        }

        
        // POST: api/TitleAkas
        [HttpPost]
        public ActionResult<TitleAkas> CreateTitleAkas([FromBody] TitleAkas newTitle)
        {
            var createdTitle = _dataService.AddTitleAkas(newTitle);

            if (createdTitle == null)
            {
                return BadRequest("An entry with this TConst already exists.");
            }

            return CreatedAtAction(nameof(GetTitleAkasById), new { id = createdTitle.TitleId }, createdTitle);
        }

        
        // PUT: api/TitleAkas/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTitleAkas(string id, [FromBody] TitleAkas updatedTitle)
        {
            var success = _dataService.UpdateTitleAkas(id, updatedTitle);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }

        // DELETE: api/TitleAkas/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTitleAkas(string id)
        {
            var success = _dataService.DeleteTitleAkas(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }
    }
}
