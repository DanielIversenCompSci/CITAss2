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
        [HttpGet("{id}", Name = nameof(GetTitleAkasById))]
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
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleAkasById), new { id = titleAkas.TitleId })
            };
        }
    }
}
