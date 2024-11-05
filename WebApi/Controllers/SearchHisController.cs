using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchHisController : ControllerBase
    {
        private readonly IDataService _dataService;

        public SearchHisController(IDataService dataService)
        {
            _dataService = dataService;
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<SearchHis>> GetSearchHisList()
        {
            var searchHistoryList = _dataService.GetSearchHisList();
            return Ok(searchHistoryList);
        }


        [HttpGet("{userId}/{timestamp}")]
        public ActionResult<SearchHis> GetSearchHistoryById(string userId, DateTime timestamp)
        {
            var searchEntry = _dataService.GetSearchHistoryById(userId, timestamp);

            if (searchEntry == null)
            {
                return NotFound();
            }

            return Ok(searchEntry);
        }


        [HttpPost]
        public ActionResult<SearchHis> CreateSearchHistory([FromBody] SearchHis newSearch)
        {
            var createdSearch = _dataService.AddSearchHistory(newSearch);

            return CreatedAtAction(nameof(GetSearchHistoryById), new { userId = createdSearch.UserId, timestamp = createdSearch.SearchTimeStamp }, createdSearch);
        }


        [HttpPut("{userId}/{timestamp}")]
        public IActionResult UpdateSearchHistory(string userId, DateTime timestamp, [FromBody] SearchHis updatedSearch)
        {
            var success = _dataService.UpdateSearchHistory(userId, timestamp, updatedSearch);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{userId}/{timestamp}")]
        public IActionResult DeleteSearchHistory(string userId, DateTime timestamp)
        {
            var success = _dataService.DeleteSearchHistory(userId, timestamp);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
