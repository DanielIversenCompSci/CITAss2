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


        [HttpGet("{userId}", Name = nameof(GetSearchHistoryByUserId))]
        public ActionResult<IEnumerable<SearchHisModel>> GetSearchHistoryByUserId(int userId)
        {
            var searchEntries = _dataService.GetSearchHistoryByUserId(userId);

            if (searchEntries == null || !searchEntries.Any())
            {
                return NotFound("No search history found for this user.");
            }

            var models = searchEntries.Select(CreateSearchHisModel).ToList();
            return Ok(models);
        }



        [HttpPost]
        public ActionResult<SearchHis> CreateSearchHistory([FromBody] SearchHisCreateModel newSearch)
        {
            var searchEntity = new SearchHis
            {
                UserId = newSearch.UserId,
                SearchQuery = newSearch.SearchQuery,
                SearchTimeStamp = newSearch.SearchTimeStamp
            };
            
            var createdSearch = _dataService.AddSearchHistory(searchEntity);

            if (createdSearch == null)
            {
                return BadRequest("Could not create search history.");
            }
            
            var model = CreateSearchHisModel(createdSearch);
            return CreatedAtAction(nameof(GetSearchHistoryByUserId), new { searchId = createdSearch.SearchId }, model);
        }


        [HttpPut("{searchId}")]
        public IActionResult UpdateSearchHis(int searchId, [FromBody] SearchHisCreateModel updatedSearch)
        {
            var updatedEntity = new SearchHis
            {
                UserId = updatedSearch.UserId, // Add back if you desire to allow altering of UserId
                SearchQuery = updatedSearch.SearchQuery
            };
            
            var success = _dataService.UpdateSearchHis(searchId, updatedEntity);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{searchId}")]
        public IActionResult DeleteSearchHis(int searchId)
        {
            Console.WriteLine($"[DEBUG] Delete request received: SearchId='{searchId}'");

            var success = _dataService.DeleteSearchHis(searchId);

            if (!success)
            {
                Console.WriteLine("[DEBUG] No matching record found in the database.");
                return NotFound($"No search history found for SearchId='{searchId}'.");
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
                SearchId = searchHis.SearchId,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetSearchHistoryByUserId),
                    new { searchId = searchHis.SearchId })
            };
        }
        
    }
}
