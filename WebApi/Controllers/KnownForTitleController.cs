using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnownForTitleController : ControllerBase
    {
        private readonly IDataService _dataService;

        public KnownForTitleController(IDataService dataService)
        {
            _dataService = dataService;
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<KnownForTitle>>GetKnownForTitleList()
        {
            var knownForTitlelist = _dataService.GetKnownForTitleList();
            return Ok(knownForTitlelist);
        }


        [HttpGet("{tConst}/{nConst}")]
        public ActionResult<KnownForTitle> GetKnownForTitleById(string tConst, string nConst)
        {
            var title = _dataService.GetKnownForTitleById(tConst, nConst);

            if (title == null)
            {
                return NotFound();
            }

            return Ok(title);
        }


        [HttpPost]
        public ActionResult<KnownForTitle> CreateKnownForTitle([FromBody] KnownForTitle newTitle)
        {
            var createdTitle = _dataService.AddKnownForTitle(newTitle);

            return CreatedAtAction(nameof(GetKnownForTitleById), new { userId = createdTitle.TConst, timestamp = createdTitle.NConst}, createdTitle);
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
    }
}
