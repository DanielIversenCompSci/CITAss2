// TitleGenreController.cs
using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleGenreController : ControllerBase
    {
        private readonly IDataService _dataService;

        public TitleGenreController(IDataService dataService)
        {
            _dataService = dataService;
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<TitleGenre>> GetTitleGenreList([FromQuery] int limit = 100)
        {
            var genres = _dataService.GetTitleGenreList(limit);
            return Ok(genres);
        }

        
        [HttpGet("{tConst}/{genre}")]
        public ActionResult<TitleGenre> GetTitleGenreById(string tConst, string genre)
        {
            var genreEntry = _dataService.GetTitleGenreById(tConst, genre);
            if (genreEntry == null)
            {
                return NotFound();
            }
            return Ok(genreEntry);
        }

        
        [HttpPost]
        public ActionResult<TitleGenre> AddTitleGenre(TitleGenre newTitleGenre)
        {
            var genreEntry = _dataService.AddTitleGenre(newTitleGenre);
            return CreatedAtAction(nameof(GetTitleGenreById), new { tConst = genreEntry.TConst, genre = genreEntry.Genre }, genreEntry);
        }

        
        [HttpPut("{tConst}/{genre}")]
        public IActionResult UpdateTitleGenre(string tConst, string genre, TitleGenre updatedTitleGenre)
        {
            if (!_dataService.UpdateTitleGenre(tConst, genre, updatedTitleGenre))
            {
                return NotFound();
            }
            return NoContent();
        }

        
        [HttpDelete("{tConst}/{genre}")]
        public IActionResult DeleteTitleGenre(string tConst, string genre)
        {
            if (!_dataService.DeleteTitleGenre(tConst, genre))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
