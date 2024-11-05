using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameBasicsController : ControllerBase
    {
        private readonly IDataService _dataService;

        public NameBasicsController(IDataService dataService)
        {
            _dataService = dataService;
        }
        
        // GET all Names, limited by page or it wont load in swagger...
        // GET: api/NameBasics
        [HttpGet]
        public ActionResult<IEnumerable<NameBasics>> GetNameBasics(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var nameBasicsList = _dataService.GetNameBasicsList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(nameBasicsList);
        }

        // Get specific title by NConst
        // GET: api/NameBasics/{id}
        [HttpGet("{id}")]
        public ActionResult<NameBasics> GetNameBasicsById(string id)
        {
            var title = _dataService.GetNameBasicsById(id);

            if (title == null)
            {
                return NotFound();
            }

            return Ok(title);
        }
        
        
        // POST: api/NameBasics
        [HttpPost]
        public ActionResult<NameBasics> CreateNameBasics([FromBody] NameBasics newTitle)
        {
            var createdTitle = _dataService.AddNameBasics(newTitle);

            if (createdTitle == null)
            {
                return BadRequest("An entry with this TConst already exists.");
            }

            return CreatedAtAction(nameof(GetNameBasicsById), new { id = createdTitle.Nconst }, createdTitle);
        }

        
        // PUT: api/NameBasics/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateNameBasics(string id, [FromBody] NameBasics updatedTitle)
        {
            var success = _dataService.UpdateNameBasics(id, updatedTitle);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }

        // DELETE: api/NameBasics/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteNameBasics(string id)
        {
            var success = _dataService.DeleteNameBasics(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }
    }
}