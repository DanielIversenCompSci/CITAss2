using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameBasicsController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public NameBasicsController(
            IDataService dataService,
            LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }
        
        // GET all Names, limited by page or it wont load in swagger...
        // GET: api/NameBasics
        [HttpGet(Name = nameof(GetNameBasics))]
        public ActionResult<IEnumerable<NameBasicsModel>> GetNameBasics(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var nameBasicsList = _dataService.GetNameBasicsList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreateNameBasicsModel)
                .ToList();
            
            var totalItems = _dataService.GetNameBasicsCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<NameBasicsModel>
            {
                Items = nameBasicsList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetNameBasics), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetNameBasics), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };

            return Ok(result);
        }

        // Get specific Name by NConst
        // GET: api/NameBasics/{id}
        [HttpGet("{id}", Name = nameof(GetNameBasicsById))]
        public ActionResult<NameBasicsModel> GetNameBasicsById(string id)
        {
            var title = _dataService.GetNameBasicsById(id);

            if (title == null)
            {
                return NotFound();
            }

            var model = CreateNameBasicsModel(title);
            return Ok(model);
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
        
        // Helper method for creating NameBasicsModel

        private NameBasicsModel CreateNameBasicsModel(NameBasics title)
        {
            return new NameBasicsModel
            {
                Nconst = title.Nconst,
                PrimaryName = title.PrimaryName,
                BirthYear = title.BirthYear,
                DeathYear = title.DeathYear,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetNameBasicsById), new { id = title.Nconst }),
            };
        }
    }
}