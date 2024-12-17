using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnownForTitleController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public KnownForTitleController(
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
        
        [HttpGet(Name = nameof(GetKnownForTitle))]
        public ActionResult<IEnumerable<KnownForTitleModel>> GetKnownForTitle(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var KnownForTitleList = _dataService.GetKnownForTitleList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreateKnownForTitleModel) // Convert to KnownForTitleModel with URL
                .ToList();

            var totalItems = _dataService.GetKnownForTitleCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<KnownForTitleModel>
            {
                Items = KnownForTitleList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetKnownForTitle), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetKnownForTitle), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };
            
            return Ok(result);
        }


        [HttpGet("{knownForTitleId}", Name = nameof(GetKnownForTitleById))]
        public ActionResult<KnownForTitleModel> GetKnownForTitleById(int knownForTitleId)
        {
            var title = _dataService.GetKnownForTitleById(knownForTitleId);

            if (title == null)
            {
                return NotFound();
            }

            var model = CreateKnownForTitleModel(title);
            return Ok(model);
        }


        [HttpPost]
        public ActionResult<KnownForTitle> CreateKnownForTitle([FromBody] KnownForTitleCreateModel newTitle)
        {
            var titleEntity = new KnownForTitle
            {
                TConst = newTitle.TConst,
                NConst = newTitle.NConst
            };
            
            var createdTitle = _dataService.AddKnownForTitle(titleEntity);

            if (createdTitle == null)
            {
                return BadRequest("Could not create new known title.");
            }
            
            var model = CreateKnownForTitleModel(createdTitle);
            return CreatedAtAction(nameof(GetKnownForTitleById), new { knownForTitleId = createdTitle.KnownForTitleId }, model);
        }


        [HttpPut("{knownForTitleId}")]
        public ActionResult<KnownForTitle> UpdateKnownForTitle(int knownForTitleId, [FromBody] KnownForTitleCreateModel updatedTitle)
        {
            var updatedEntity = new KnownForTitle
            {
                TConst = updatedTitle.TConst,
                NConst = updatedTitle.NConst
            };
            
            var success = _dataService.UpdateKnownForTitle(knownForTitleId, updatedEntity);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{knownForTitleId}")]
        public IActionResult DeleteKnownForTitle(int knownForTitleId)
        {
            var success = _dataService.DeleteKnownForTitle(knownForTitleId);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        // Helper for creating a model of this object, adding URL
        private KnownForTitleModel CreateKnownForTitleModel(KnownForTitle title)
        {
            return new KnownForTitleModel
            {
                TConst = title.TConst,
                NConst = title.NConst,
                KnownForTitleId = title.KnownForTitleId,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetKnownForTitleById), new { knownForTitleId = title.KnownForTitleId })
            };
        }
    }
}
