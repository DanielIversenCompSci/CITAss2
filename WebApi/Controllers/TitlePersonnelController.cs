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
        [HttpGet("{titlePersonnelId}", Name = nameof(GetTitlePersonnelById))]
        public ActionResult<TitlePersonnelModel> GetTitlePersonnelById(int titlePersonnelId)
        {
            var title = _dataService.GetTitlePersonnelById(titlePersonnelId);

            if (title == null)
            {
                return NotFound();
            }

            var model = CreateTitlePersonnelModel(title);
            return Ok(model);
        }
        
        // POST: api/TitlePersonnel
        [HttpPost]
        public ActionResult<TitlePersonnel> CreateTitlePersonnel([FromBody] TitlePersonnelCreateModel newTitle)
        {
            var personnelEntity = new TitlePersonnel
            {
                TConst = newTitle.TConst,
                NConst = newTitle.NConst,
                Role = newTitle.Role
            };
            
            var createdTitle = _dataService.AddTitlePersonnel(personnelEntity);

            if (createdTitle == null)
            {
                return BadRequest("An entry with this TConst already exists.");
            }

            var model = CreateTitlePersonnelModel(createdTitle);
            return CreatedAtAction(nameof(GetTitlePersonnelById), new { titlePersonnelId = createdTitle.TitlePersonnelId }, model);
        }

        
        // PUT: api/TitlePersonnel/{id}
        [HttpPut("{titlePersonnelId}")]
        public IActionResult UpdateTitlePersonnel(int titlePersonnelId, [FromBody] TitlePersonnelCreateModel updatedTitle)
        {
            var updatedEntity = new TitlePersonnel
            {
                TConst = updatedTitle.TConst,
                NConst = updatedTitle.NConst,
                Role = updatedTitle.Role
            };
            
            
            
            var success = _dataService.UpdateTitlePersonnel(titlePersonnelId, updatedEntity);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }

        // DELETE: api/TitlePersonnel/{id}
        [HttpDelete("{titlePersonnelId}")]
        public IActionResult DeleteTitlePersonnel(int titlePersonnelId)
        {
            var success = _dataService.DeleteTitlePersonnel(titlePersonnelId);

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
                TitlePersonnelId = titlePersonnel.TitlePersonnelId,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetTitlePersonnelById), new { titlePersonnelId = titlePersonnel.TitlePersonnelId })
            };
        }
    }
}
