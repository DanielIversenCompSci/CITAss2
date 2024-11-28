using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleBasicsController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public TitleBasicsController(
            IDataService dataService,
            LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        // GET all Titles with pagination
        [HttpGet(Name = nameof(GetTitleBasics))]
        public ActionResult<IEnumerable<TitleBasicsModel>> GetTitleBasics(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var titleBasicsList = _dataService.GetTitleBasicsList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreateTitleBasicsModel) // Convert to TitleBasicsModel with URL
                .ToList();

            var totalItems = _dataService.GetTitleBasicsCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<TitleBasicsModel>
            {
                Items = titleBasicsList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleBasics), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleBasics), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };
            
            return Ok(result);
        }

        // Get specific title by TConst
        [HttpGet("{id}", Name = nameof(GetTitleBasicsById))]
        public ActionResult<TitleBasicsModel> GetTitleBasicsById(string id)
        {
            var title = _dataService.GetTitleBasicsById(id);

            if (title == null)
            {
                return NotFound();
            }

            var model = CreateTitleBasicsModel(title);
            return Ok(model);
        }

        // POST: Create a new TitleBasics entry
        [HttpPost]
        public ActionResult<TitleBasicsModel> CreateTitleBasics([FromBody] TitleBasics newTitle)
        {
            var createdTitle = _dataService.AddTitleBasics(newTitle);

            if (createdTitle == null)
            {
                return BadRequest("An entry with this TConst already exists.");
            }

            var model = CreateTitleBasicsModel(createdTitle);
            return CreatedAtAction(nameof(GetTitleBasicsById), new { id = createdTitle.TConst }, model);
        }

        // PUT: Update an existing TitleBasics entry
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

        // DELETE: Delete an existing TitleBasics entry
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

        // Helper method to create TitleBasicsModel with URL
        private TitleBasicsModel CreateTitleBasicsModel(TitleBasics title)
        {
            return new TitleBasicsModel
            {
                TConst = title.TConst,
                TitleType = title.TitleType,
                PrimaryTitle = title.PrimaryTitle,
                OriginalTitle = title.OriginalTitle,
                IsAdult = title.IsAdult,
                StartYear = title.StartYear,
                EndYear = title.EndYear,
                RuntimeMinutes = title.RuntimeMinutes,
                Plot = title.Plot,
                Poster = title.Poster,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleBasicsById), new { id = title.TConst })
            };
        }
    }
}
