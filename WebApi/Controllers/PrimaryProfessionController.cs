using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrimaryProfessionController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public PrimaryProfessionController(
            IDataService dataService,
            LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        // ********** **********
        // This controller dosen't have any ENDPOINTs used by the frontend
        // However the group decided to keep them just for showcase of basic CRUD for all object
        // Fully aware they are not used for anything at the current state of the application
        // ********** **********
        // Endpoints NOT IN USE
        // ********** **********
        
        // GET all Titles, limited by page or it wont load in swagger...
        // GET: api/PrimaryProfession
        [HttpGet(Name = nameof(GetPrimaryProfession))]
        public ActionResult<IEnumerable<PrimaryProfessionModel>> GetPrimaryProfession(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var PrimaryProfessionList = _dataService.GetPrimaryProfessionList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreatePrimaryProfessionModel) // Convert to PrimaryProfessionModel with URL
                .ToList();

            var totalItems = _dataService.GetPrimaryProfessionCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<PrimaryProfessionModel>
            {
                Items = PrimaryProfessionList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetPrimaryProfession), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetPrimaryProfession), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };
            
            return Ok(result);
        }

        // Get specific title by Nconst
        // GET: api/PrimaryProfession/{id}
        [HttpGet("{primaryProfessionId}", Name = nameof(GetPrimaryProfessionById))]
        public ActionResult<PrimaryProfessionModel> GetPrimaryProfessionById(int primaryProfessionId)
        {
            var title = _dataService.GetPrimaryProfessionById(primaryProfessionId);

            if (title == null)
            {
                return NotFound();
            }

            var model = CreatePrimaryProfessionModel(title);
            return Ok(model);
        }

        
        // POST: api/PrimaryProfession
        [HttpPost]
        public ActionResult<PrimaryProfessionModel> CreatePrimaryProfession([FromBody] PrimaryProfessionCreateModel newTitle)
        {
            var professionEntity = new PrimaryProfession
            {
                NConst = newTitle.NConst,
                Role = newTitle.Role
            };
            
            var createdTitle = _dataService.AddPrimaryProfession(professionEntity);

            if (createdTitle == null)
            {
                return BadRequest("An entry with this NConst already exists.");
            }

            var model = CreatePrimaryProfessionModel(createdTitle);
            return CreatedAtAction(nameof(GetPrimaryProfessionById), new { primaryProfessionId = createdTitle.PrimaryProfessionId }, model);
        }

        
        // PUT: api/PrimaryProfession/{id}
        [HttpPut("{primaryProfessionId}")]
        public IActionResult UpdatePrimaryProfession(int primaryProfessionId, [FromBody] PrimaryProfessionCreateModel updatedTitle)
        {
            var updatedEntity = new PrimaryProfession
            {
                NConst = updatedTitle.NConst,
                Role = updatedTitle.Role,
            };
            
            var success = _dataService.UpdatePrimaryProfession(primaryProfessionId, updatedEntity);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }

        // DELETE: api/PrimaryProfession/{id}
        [HttpDelete("{primaryProfessionId}")]
        public IActionResult DeletePrimaryProfession(int primaryProfessionId)
        {
            var success = _dataService.DeletePrimaryProfession(primaryProfessionId);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Success, no content to return
        }
        
        
        // Helper method to create a model for the object
        private PrimaryProfessionModel CreatePrimaryProfessionModel(PrimaryProfession title)
        {
            return new PrimaryProfessionModel
            {
                NConst = title.NConst,
                Role = title.Role,
                PrimaryProfessionId = title.PrimaryProfessionId,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetPrimaryProfessionById), new { primaryProfessionId = title.PrimaryProfessionId })
            };
        }
    }
}
