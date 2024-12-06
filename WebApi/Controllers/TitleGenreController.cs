// TitleGenreController.cs
using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleGenreController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public TitleGenreController(
            IDataService dataService,
            LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        /*
        [HttpGet]
        public ActionResult<IEnumerable<TitleGenre>> GetTitleGenreList([FromQuery] int limit = 100)
        {
            var genres = _dataService.GetTitleGenreList(limit);
            return Ok(genres);
        }
        */
        
        // Get all TitleGenres with pagination and URL
        [HttpGet(Name = nameof(GetTitleGenre))]
        public ActionResult<IEnumerable<TitleGenreModel>> GetTitleGenre(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }
            
            var titleGenreList = _dataService.GetTitleGenreList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreateTitleGenreModel)
                .ToList();
            
            var totalItems = _dataService.GetTitleGenreCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<TitleGenreModel>
            {
                Items = titleGenreList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleGenre), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleGenre), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };
            
            return Ok(result);
        }

        
        [HttpGet("{tConst}/{genre}", Name = nameof(GetTitleGenreById))]
        public ActionResult<TitleGenre> GetTitleGenreById(string tConst, string genre)
        {
            var genreEntry = _dataService.GetTitleGenreById(tConst, genre);
            
            if (genreEntry == null)
            {
                return NotFound();
            }
            
            var model = CreateTitleGenreModel(genreEntry);
            return Ok(model);
        }

        
        [HttpPost]
        public ActionResult<TitleGenreModel> AddTitleGenre([FromBody] TitleGenreCreateModel newTitleGenre)
        {
            var titleEntity = new TitleGenre
            {
                TConst = newTitleGenre.TConst,
                Genre = newTitleGenre.Genre
            };
            
            var genreEntry = _dataService.AddTitleGenre(titleEntity);

            if (genreEntry == null)
            {
                return BadRequest("Title genre could not be added.");
            }
            
            var model = CreateTitleGenreModel(genreEntry);
            return CreatedAtAction(nameof(GetTitleGenreById), new { tConst = genreEntry.TConst, genre = genreEntry.Genre }, model);
        }


        [HttpPut("{tConst}/{genre}")]
        public IActionResult UpdateTitleGenre(string tConst, string genre, TitleGenreCreateModel updatedTitleGenre)
        {
            var updatedEntity = new TitleGenre
            {
                TConst = tConst,
                Genre = genre
            };
            
            var succes = _dataService.UpdateTitleGenre(tConst, genre, updatedEntity);

            if (!succes)
            {
                return NotFound();
            }

            return NoContent();
        }

        
        [HttpDelete("{tConst}/{genre}")]
        public IActionResult DeleteTitleGenre(string tConst, string genre)
        {
            var succes = _dataService.DeleteTitleGenre(tConst, genre);

            if (!succes)
            {
                return NotFound("Title genre could not be deleted.");
            }
            
            return NoContent();
        }

        private TitleGenreModel CreateTitleGenreModel(TitleGenre title)
        {
            return new TitleGenreModel
            {
                TConst = title.TConst,
                Genre = title.Genre,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleGenreById), new { tConst = title.TConst, genre = title.Genre })
            };
        }
    }
}
