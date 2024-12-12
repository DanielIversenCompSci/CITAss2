using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;
using WebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBookmarksController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public UserBookmarksController(
            IDataService dataService,
            LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }
        
        // GET all Titles with pagination
        [HttpGet(Name = nameof(GetUserBookmarks))]
        //[Authorize]
        public ActionResult<IEnumerable<UserBookmarksModel>> GetUserBookmarks(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var UserBookmarksList = _dataService.GetUserBookmarks()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreateUserBookmarksModel) // Convert to UserBookmarksModel with URL
                .ToList();

            var totalItems = _dataService.GetUserBookmarksCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<UserBookmarksModel>
            {
                Items = UserBookmarksList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetUserBookmarks), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetUserBookmarks), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };
            
            return Ok(result);
        }

        [HttpGet("user/{userId}/bookmarksWithTitles", Name = nameof(GetBookmarksWithTitlesAsync))]
        public async Task<ActionResult<List<BookmarksWithTitles>>> GetBookmarksWithTitlesAsync(int userId)
        {
            var bookmarks = await _dataService.GetBookmarksWithTitlesAsync(userId);

            if (bookmarks == null || !bookmarks.Any())
            {
                return NotFound("No bookmarks found for the specified user.");
            }

            return Ok(bookmarks);
        }

        // Get specific title by TConst
        [HttpGet("{userBookmarksId}", Name = nameof(GetUserBookmarksById))]
        public ActionResult<UserBookmarksModel> GetUserBookmarksById(int userBookmarksId)
        {
            var title = _dataService.GetUserBookmarksById(userBookmarksId);

            if (title == null)
            {
                return NotFound();
            }

            var model = CreateUserBookmarksModel(title);
            return Ok(model);
        }

        // POST: Create a new UserBookmarks entry
        [HttpPost]
        public ActionResult<UserBookmarksModel> CreateUserBookmarks([FromBody] UserBookmarksCreateModel newBookmark)
        {
            try
            {
                var bookmarkEntity = new UserBookmarks
                {
                    UserId = newBookmark.UserId,
                    TConst = newBookmark.TConst,
                    Note = newBookmark.Note
                };

                var createdBookmark = _dataService.AddUserBookmarks(bookmarkEntity);

                if (createdBookmark == null)
                {
                    return BadRequest("An entry with this TConst already exists.");
                }

                var model = CreateUserBookmarksModel(createdBookmark);
                return CreatedAtAction(nameof(GetUserBookmarksById), new { UserBookmarksId = createdBookmark.UserBookmarksId }, model);

            }
            catch(Exception ex) 
            {
                return StatusCode(500, "$internal server error: {ex.message}");
            }
        }

        
        [HttpPut("{userBookmarksId}")]
        public IActionResult UpdateUserBookmarks(int userBookmarksId, [FromBody] UserBookmarksCreateModel updatedBookmark)
        {
            var updatedEntity = new UserBookmarks
            {
                UserId = updatedBookmark.UserId,
                TConst = updatedBookmark.TConst,
                Note = updatedBookmark.Note
            };
            
            var success = _dataService.UpdateUserBookmarks(userBookmarksId, updatedEntity);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        
        [HttpDelete("{userBookmarksId}")]
        public IActionResult DeleteUserBookmarks(int userBookmarksId)
        {
            var success = _dataService.DeleteUserBookmarks(userBookmarksId);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }


        private UserBookmarksModel CreateUserBookmarksModel(UserBookmarks userBookmarks)
        {
            return new UserBookmarksModel
            {
                UserBookmarksId = userBookmarks.UserBookmarksId,
                UserId = userBookmarks.UserId,
                TConst = userBookmarks.TConst,
                Note = userBookmarks.Note,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetUserBookmarksById),
            new { userBookmarksId = userBookmarks.UserBookmarksId })
            };
        }
    }
}
