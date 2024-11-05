using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleBasicsController : ControllerBase
    {
        private readonly IDataService _dataService;

        // Inject the data service
        public TitleBasicsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET all Titles, limited by page or it wont load in swagger...
        // GET: api/TitleBasics
        [HttpGet]
        public ActionResult<IEnumerable<TitleBasics>> GetTitleBasics(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var titleBasicsList = _dataService.GetTitleBasicsList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(titleBasicsList);
        }

        // Get specific title by TConst
        // GET: api/TitleBasics/{id}
        [HttpGet("{id}")]
        public ActionResult<TitleBasics> GetTitleBasicsById(string id)
        {
            var title = _dataService.GetTitleBasicsById(id);

            if (title == null)
            {
                return NotFound();
            }

            return Ok(title);
        }

        
        // POST: api/TitleBasics
        [HttpPost]
        public ActionResult<TitleBasics> CreateTitleBasics([FromBody] TitleBasics newTitle)
        {
            var createdTitle = _dataService.AddTitleBasics(newTitle);

            if (createdTitle == null)
            {
                return BadRequest("An entry with this TConst already exists.");
            }

            return CreatedAtAction(nameof(GetTitleBasicsById), new { id = createdTitle.TConst }, createdTitle);
        }

        
        // PUT: api/TitleBasics/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTitleBasics(string id, [FromBody] TitleBasics updatedTitle)
        {
            var success = _dataService.UpdateTitleBasics(id, updatedTitle);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }

        // DELETE: api/TitleBasics/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTitleBasics(string id)
        {
            var success = _dataService.DeleteTitleBasics(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }
    }
}
