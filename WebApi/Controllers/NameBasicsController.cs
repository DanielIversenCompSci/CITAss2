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
        [HttpGet("{nConst}", Name = nameof(GetNameBasicsById))]
        public ActionResult<NameBasicsModel> GetNameBasicsById(string nConst)
        {
            var title = _dataService.GetNameBasicsById(nConst);

            if (title == null)
            {
                return NotFound();
            }

            var model = CreateNameBasicsModel(title);
            return Ok(model);
        }
        
        
        //GET Limit 100
        [HttpGet("limited", Name = nameof(GetLimitedNameBasics))]
        public async Task<ActionResult<PagedResultModel<NameBasicsModel>>> GetLimitedNameBasics(int limit = 100, int pageNumber = 1)
        {
            if (limit <= 0 || pageNumber <= 0)
            {
                return BadRequest("Limit and page number must be greater than zero.");
            }

            var totalItems = await _dataService.GetNameBasicsCountAsync(); // Fetch the total count of records
            var offset = (pageNumber - 1) * limit;

            if (offset >= totalItems)
            {
                return BadRequest("Page number exceeds total pages available.");
            }

            var nameBasicsList = await _dataService.GetLimitedNameBasicsAsync(limit, offset);

            var totalPages = (int)Math.Ceiling(totalItems / (double)limit);

            var result = new PagedResultModel<NameBasicsModel>
            {
                Items = nameBasicsList.Select(CreateNameBasicsModel),
                PageNumber = pageNumber,
                PageSize = limit,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetLimitedNameBasics), new { limit, pageNumber = pageNumber + 1 })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetLimitedNameBasics), new { limit, pageNumber = pageNumber - 1 })
                    : null
            };

            return Ok(result);
        }
        
        [HttpGet("topNames100")]
        public async Task<ActionResult<List<NameWithRating>>> GetTopRatedNames()
        {
            var topRatedNames = await _dataService.GetTopRatedNamesAsync();
            return Ok(topRatedNames);
        }
        
        // Top w Searchd
        [HttpGet("topNames100Sub")]
        public async Task<ActionResult<List<NameWithRating>>> GetTopRatedNamesSub([FromQuery] string substring_filter = null)
        {
            var topRatedNamesSub = await _dataService.GetTopRatedNamesSubAsync(substring_filter);
            return Ok(topRatedNamesSub);
        }

        
        // details/nconst
        [HttpGet("detailss/{nConst}", Name = nameof(GetNameWithRatingById))]
        public async Task<ActionResult<NameWithRating>> GetNameWithRatingById(string nConst)
        {
            var nameWithRating = await _dataService.GetNameWithRatingByIdAsync(nConst);

            if (nameWithRating == null)
                return NotFound();

            return Ok(nameWithRating);
        }
        
        // details/nconst
        [HttpGet("details/{nConst}", Name = nameof(GetNameByNConstSQL))]
        public async Task<ActionResult<NameWithRating>> GetNameByNConstSQL(string nConst)
        {
            var nameWithRating = await _dataService.GetNameByNConstSQL(nConst);

            if (nameWithRating == null)
                return NotFound();

            return Ok(nameWithRating);
        }



        
        
        // POST: api/NameBasics
        [HttpPost]
        public ActionResult<NameBasics> CreateNameBasics([FromBody] NameBasicsCreateModel newName)
        {
            var nameEntity = new NameBasics
            {
                NConst = newName.NConst,
                PrimaryName = newName.PrimaryName,
                BirthYear = newName.BirthYear,
                DeathYear = newName.DeathYear
            };
            
            var createdTitle = _dataService.AddNameBasics(nameEntity);

            if (createdTitle == null)
            {
                return BadRequest("An entry with this TConst already exists.");
            }

            return CreatedAtAction(nameof(GetNameBasicsById), new { nConst = createdTitle.NConst }, createdTitle);
        }

        
        // PUT: api/NameBasics/{id}
        [HttpPut("{nConst}")]
        public IActionResult UpdateNameBasics(string nConst, [FromBody] NameBasicsCreateModel updatedName)
        {
            var updatedEntity = new NameBasics
            {
                NConst = updatedName.NConst,
                PrimaryName = updatedName.PrimaryName,
                BirthYear = updatedName.BirthYear,
                DeathYear = updatedName.DeathYear
            };
            
            var success = _dataService.UpdateNameBasics(nConst, updatedEntity);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }

        // DELETE: api/NameBasics/{id}
        [HttpDelete("{nConst}")]
        public IActionResult DeleteNameBasics(string nConst)
        {
            var success = _dataService.DeleteNameBasics(nConst);

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
                NConst = title.NConst,
                PrimaryName = title.PrimaryName,
                BirthYear = title.BirthYear,
                DeathYear = title.DeathYear,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetNameBasicsById), new { nConst = title.NConst }),
            };
        }
    }
}