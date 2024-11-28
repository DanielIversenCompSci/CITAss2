using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchHisController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public SearchHisController(
            IDataService dataService,
            LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        
        // GET all SearchHis with pagination
        [HttpGet(Name = nameof(GetSearchHis))]
        public ActionResult<IEnumerable<SearchHisModel>> GetSearchHis(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var SearchHisList = _dataService.GetSearchHisList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreateSearchHisModel) // Convert to SearchHisModel with URL
                .ToList();

            var totalItems = _dataService.GetSearchHisCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<SearchHisModel>
            {
                Items = SearchHisList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetSearchHis), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetSearchHis), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };
            
            return Ok(result);
        }


        [HttpGet("{userId}/{timestamp}", Name = nameof(GetSearchHistoryById))]
        public ActionResult<SearchHis> GetSearchHistoryById(string userId, DateTime timestamp)
        {
            var searchEntry = _dataService.GetSearchHistoryById(userId, timestamp);

            if (searchEntry == null)
            {
                return NotFound();
            }

            var model = CreateSearchHisModel(searchEntry);
            return Ok(model);
        }


        [HttpPost]
        public ActionResult<SearchHis> CreateSearchHistory([FromBody] SearchHis newSearch)
        {
            var createdSearch = _dataService.AddSearchHistory(newSearch);

            if (createdSearch == null)
            {
                return BadRequest("Could not create search history.");
            }
            
            var model = CreateSearchHisModel(createdSearch);
            return CreatedAtAction(nameof(GetSearchHistoryById), new { userId = createdSearch.UserId, timestamp = createdSearch.SearchTimeStamp }, model);
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
            Console.WriteLine($"[DEBUG] Delete request received: UserId='{userId}', Timestamp='{timestamp}'");

            var success = _dataService.DeleteSearchHistory(userId, timestamp);

            if (!success)
            {
                Console.WriteLine("[DEBUG] No matching record found in the database.");
                return NotFound($"No search history found for UserId='{userId}' and Timestamp='{timestamp}'.");
            }

            Console.WriteLine("[DEBUG] Record successfully deleted.");
            return NoContent();
        }



        private SearchHisModel CreateSearchHisModel(SearchHis searchHis)
        {
            return new SearchHisModel
            {
                UserId = searchHis.UserId,
                SearchQuery = searchHis.SearchQuery,
                SearchTimeStamp = searchHis.SearchTimeStamp,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetSearchHistoryById),
                    new { userId = searchHis.UserId, timestamp = searchHis.SearchTimeStamp })
            };
        }
        
    }
}
