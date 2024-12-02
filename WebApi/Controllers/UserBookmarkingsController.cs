using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBookmarkingsController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public UserBookmarkingsController(
            IDataService dataService,
            LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }
        
        // GET all Titles with pagination
        [HttpGet(Name = nameof(GetUserBookmarkings))]
        public ActionResult<IEnumerable<UserBookmarkingsModel>> GetUserBookmarkings(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var UserBookmarkingsList = _dataService.GetUserBookmarkings()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreateUserBookmarkingsModel) // Convert to UserBookmarkingsModel with URL
                .ToList();

            var totalItems = _dataService.GetUserBookmarkingsCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<UserBookmarkingsModel>
            {
                Items = UserBookmarkingsList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetUserBookmarkings), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetUserBookmarkings), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };
            
            return Ok(result);
        }

        // Get specific title by TConst
        [HttpGet("{userBookmarkingsId}", Name = nameof(GetUserBookmarkingsById))]
        public ActionResult<UserBookmarkingsModel> GetUserBookmarkingsById(int userBookmarkingsId)
        {
            var title = _dataService.GetUserBookmarkingsById(userBookmarkingsId);

            if (title == null)
            {
                return NotFound();
            }

            var model = CreateUserBookmarkingsModel(title);
            return Ok(model);
        }

        // POST: Create a new UserBookmarkings entry
        [HttpPost]
        public ActionResult<UserBookmarkingsModel> CreateUserBookmarkings([FromBody] UserBookmarkingsCreateModel newBookmark)
        {

            var bookmarkEntity = new UserBookmarkings
            {
                UserId = newBookmark.UserId,
                TConst = newBookmark.TConst,
                Note = newBookmark.Note
            };
            
            var createdTitle = _dataService.AddUserBookmarkings(bookmarkEntity);

            if (createdTitle == null)
            {
                return BadRequest("An entry with this TConst already exists.");
            }

            var model = CreateUserBookmarkingsModel(createdTitle);
            return CreatedAtAction(nameof(GetUserBookmarkingsById), new { UserBookmarkingsId = createdTitle.UserBookmarkingsId}, model);
        }

        
        [HttpPut("{userBookmarkingsId}")]
        public IActionResult UpdateUserBookmarkings(int userBookmarkingsId, [FromBody] UserBookmarkingsCreateModel updatedBookmark)
        {
            var updatedEntity = new UserBookmarkings
            {
                UserId = updatedBookmark.UserId,
                TConst = updatedBookmark.TConst,
                Note = updatedBookmark.Note
            };
            
            var success = _dataService.UpdateUserBookmarkings(userBookmarkingsId, updatedEntity);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        
        [HttpDelete("{userBookmarkingsId}")]
        public IActionResult DeleteUserBookmarkings(int userBookmarkingsId)
        {
            var success = _dataService.DeleteUserBookmarkings(userBookmarkingsId);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }


        private UserBookmarkingsModel CreateUserBookmarkingsModel(UserBookmarkings userBookmarkings)
        {
            return new UserBookmarkingsModel
            {
                UserId = userBookmarkings.UserId,
                TConst = userBookmarkings.TConst,
                Note = userBookmarkings.Note,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetUserBookmarkingsById),
                    new { userBookmarkingsId = userBookmarkings.UserBookmarkingsId })
            };
        }
    }
}
