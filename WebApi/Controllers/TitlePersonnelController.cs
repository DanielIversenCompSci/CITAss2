using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitlePersonnelController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public TitlePersonnelController(
            IDataService dataService,
            LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        // GET all Titles, limited by page or it wont load in swagger...
        // GET: api/TitlePersonnel
        [HttpGet]
        public ActionResult<IEnumerable<TitlePersonnelModel>> GetTitlePersonnel(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var titlePersonnelList = _dataService.GetTitlePersonnelList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreateTitlePersonnelModel) // Convert to TitlePersonnelModel with URL
                .ToList();

            var totalItems = _dataService.GetTitlePersonnelCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<TitlePersonnelModel>
            {
                Items = titlePersonnelList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetTitlePersonnel), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetTitlePersonnel), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };
            
            return Ok(result);
        }

        // Get specific title by TConst
        [HttpGet("{id}", Name = nameof(GetTitlePersonnelById))]
        public ActionResult<TitlePersonnelModel> GetTitlePersonnelById(string id)
        {
            var title = _dataService.GetTitlePersonnelById(id);

            if (title == null)
            {
                return NotFound();
            }

            var model = CreateTitlePersonnelModel(title);
            return Ok(model);
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
        
        // Helper method to create TitlePersonnelModel with URL
        private TitlePersonnelModel CreateTitlePersonnelModel(TitlePersonnel titlePersonnel)
        {
            return new TitlePersonnelModel
            {
                TConst = titlePersonnel.TConst,
                NConst = titlePersonnel.NConst,
                Role = titlePersonnel.Role,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetTitlePersonnelById), new { id = titlePersonnel.TConst })
            };
        }
    }
}
