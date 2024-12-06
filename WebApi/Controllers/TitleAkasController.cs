using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleAkasController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public TitleAkasController(
            IDataService dataService,
            LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        // GET all Titles, limited by page or it wont load in swagger...
        // GET: api/TitleAkas
        [HttpGet(Name = nameof(GetTitleAkas))]
        public ActionResult<IEnumerable<TitleAkasModel>> GetTitleAkas(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var titleAkasList = _dataService.GetTitleAkasList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreateTitleAkasModel) // Convert to TitleAkasModel with URL
                .ToList();

            var totalItems = _dataService.GetTitleAkasCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<TitleAkasModel>
            {
                Items = titleAkasList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleAkas), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleAkas), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };
            
            return Ok(result);
        }

        // Get specific title by TConst
        // GET: api/TitleAkas/{id}
        [HttpGet("{titleId}/{ordering}", Name = nameof(GetTitleAkasById))]
        public ActionResult<TitleAkas> GetTitleAkasById(string titleId, int ordering)
        {
            var title = _dataService.GetTitleAkasById(titleId, ordering);

            if (title == null)
            {
                return NotFound();
            }

            var model = CreateTitleAkasModel(title);
            return Ok(model);
        }

        
        // POST: api/TitleAkas
        [HttpPost]
        public ActionResult<TitleAkas> CreateTitleAkas([FromBody] TitleAkasCreateModel newTitle)
        {
            var titleEntity = new TitleAkas
            {
                TitleId = newTitle.TitleId,
                Ordering = newTitle.Ordering,
                Title = newTitle.Title,
                Region = newTitle.Region,
                Language = newTitle.Language,
                Types = newTitle.Types,
                Attributes = newTitle.Attributes,
                IsOriginalTitle = newTitle.IsOriginalTitle
            };
            
            var createdTitle = _dataService.AddTitleAkas(titleEntity);

            if (createdTitle == null)
            {
                return BadRequest("An entry with this TConst already exists.");
            }

            var model = CreateTitleAkasModel(createdTitle);
            return CreatedAtAction(nameof(GetTitleAkasById), new { titleId = createdTitle.TitleId, ordering = createdTitle.Ordering }, model);
        }

        
        // PUT: api/TitleAkas/{id}
        [HttpPut("{titleId}/{ordering}")]
        public IActionResult UpdateTitleAkas(string titleId, int ordering, [FromBody] TitleAkasCreateModel updatedTitle)
        {
            var updatedEntity = new TitleAkas
            {
                TitleId = updatedTitle.TitleId,
                Ordering = updatedTitle.Ordering,
                Title = updatedTitle.Title,
                Region = updatedTitle.Region,
                Language = updatedTitle.Language,
                Types = updatedTitle.Types,
                Attributes = updatedTitle.Attributes,
                IsOriginalTitle = updatedTitle.IsOriginalTitle
            };
            
            var success = _dataService.UpdateTitleAkas(titleId, ordering, updatedEntity);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }

        // DELETE: api/TitleAkas/{id}
        [HttpDelete("{titleId}/{ordering}")]
        public IActionResult DeleteTitleAkas(string titleId, int ordering)
        {
            var success = _dataService.DeleteTitleAkas(titleId, ordering);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }
        
        private TitleAkasModel CreateTitleAkasModel(TitleAkas titleAkas)
        {
            return new TitleAkasModel
            {
                TitleId = titleAkas.TitleId,
                Ordering = titleAkas.Ordering,
                Title = titleAkas.Title,
                Region = titleAkas.Region,
                Language = titleAkas.Language,
                Types = titleAkas.Types,
                Attributes = titleAkas.Attributes,
                IsOriginalTitle = titleAkas.IsOriginalTitle,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleAkasById), new { titleId = titleAkas.TitleId, ordering = titleAkas.Ordering })
            };
        }
    }
}
