using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitlePersonnelController : ControllerBase
    {
        private readonly IDataService _dataService;

        // Inject the data service
        public TitlePersonnelController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET all Titles, limited by page or it wont load in swagger...
        // GET: api/TitlePersonnel
        [HttpGet]
        public ActionResult<IEnumerable<TitlePersonnel>> GetTitlePersonnel(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var TitlePersonnelList = _dataService.GetTitlePersonnelList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(TitlePersonnelList);
        }

        // Get specific title by TConst
        // GET: api/TitlePersonnel/{id}
        [HttpGet("{id}")]
        public ActionResult<TitlePersonnel> GetTitlePersonnelById(string id)
        {
            var title = _dataService.GetTitlePersonnelById(id);

            if (title == null)
            {
                return NotFound();
            }

            return Ok(title);
        }

        
        // POST: api/TitlePersonnel
        [HttpPost]
        public ActionResult<TitlePersonnel> CreateTitlePersonnel([FromBody] TitlePersonnel newTitle)
        {
            var createdTitle = _dataService.AddTitlePersonnel(newTitle);

            if (createdTitle == null)
            {
                return BadRequest("An entry with this TConst already exists.");
            }

            return CreatedAtAction(nameof(GetTitlePersonnelById), new { id = createdTitle.TConst }, createdTitle);
        }

        
        // PUT: api/TitlePersonnel/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTitlePersonnel(string id, [FromBody] TitlePersonnel updatedTitle)
        {
            var success = _dataService.UpdateTitlePersonnel(id, updatedTitle);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }

        // DELETE: api/TitlePersonnel/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTitlePersonnel(string id)
        {
            var success = _dataService.DeleteTitlePersonnel(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }
    }
}
