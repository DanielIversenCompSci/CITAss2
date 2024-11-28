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


        [HttpGet("{tConst}/{nConst}", Name = nameof(GetKnownForTitleById))]
        public ActionResult<KnownForTitleModel> GetKnownForTitleById(string tConst, string nConst)
        {
            var title = _dataService.GetKnownForTitleById(tConst, nConst);

            if (title == null)
            {
                return NotFound();
            }

            var model = CreateKnownForTitleModel(title);
            return Ok(model);
        }


        [HttpPost]
        public ActionResult<KnownForTitle> CreateKnownForTitle([FromBody] KnownForTitle newTitle)
        {
            var createdTitle = _dataService.AddKnownForTitle(newTitle);

            if (createdTitle == null)
            {
                return BadRequest("Could not create new known title.");
            }
            
            var model = CreateKnownForTitleModel(createdTitle);
            return CreatedAtAction(nameof(GetKnownForTitleById), new { userId = createdTitle.TConst, timestamp = createdTitle.NConst}, model);
        }


        [HttpPut("{tConst}/{nConst}")]
        public ActionResult<KnownForTitle> UpdateKnownForTitle(string tConst, string nConst, [FromBody] KnownForTitle updatedTitle)
        {
            var success = _dataService.UpdateKnownForTitle(tConst, nConst, updatedTitle);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{tConst}/{nConst}")]
        public IActionResult DeleteKnownForTitle(string tConst, string nConst)
        {
            var success = _dataService.DeleteKnownForTitle(tConst, nConst);

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
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetKnownForTitleById), new { tConst = title.TConst, nConst = title.NConst })
            };
        }
    }
}
