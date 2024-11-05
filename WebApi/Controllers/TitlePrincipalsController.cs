using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TitlePrincipalsController : ControllerBase
{
    // Dataservice var
    private readonly IDataService _dataService;
    
    // Inject the dataservice var
    public TitlePrincipalsController(IDataService dataService)
    {
        _dataService = dataService;
    }
    
    
    // GET all etities limited by page or it wont load in swagger
    // GET: api/TitlePrincipals
    [HttpGet]
    public ActionResult<IEnumerable<TitlePrincipals>> GetTitlePrincipals(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            return BadRequest("Page number and page size must be greater than zero.");
        }
        
        var titlePrincipalsList = _dataService.GetTitlePrincipalsList()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        return Ok(titlePrincipalsList);
    }
    
    
    // Get spicific title by TConst
    // GET: api/TitlePrincipals/{id}
    [HttpGet("{id}")]
    public ActionResult<TitlePrincipals> GetTitlePrincipalsById(string id)
    {
        var title = _dataService.GetTitlePrincipalsById(id);

        if (title == null)
        {
            return NotFound();
        }
        
        return Ok(title);
    }
    
    
    // CREATE funcs
    // POST: api/TitlePrincipals
    [HttpPost]
    public ActionResult<TitlePrincipals> CreateTitlePrincipals([FromBody] TitlePrincipals newTitle)
    {
        var createdTitle = _dataService.AddTitlePrincipals(newTitle);

        if (createdTitle == null)
        {
            return BadRequest("An entry with this TConst already exists.");
        }
        
        return CreatedAtAction(nameof(GetTitlePrincipalsById), new { id = createdTitle.TConst }, createdTitle);
    }
    
    
    // PUT: api/TitlePrincipals/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateTitlePrincipals(string id, [FromBody] TitlePrincipals updatedTitle)
    {
        var success = _dataService.UpdateTitlePrincipals(id, updatedTitle);

        if (!success)
        {
            return NotFound();
        }

        return NoContent(); // Success, no content to return
    }
    
    
    // DELETE: api/TitlePrincipals/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteTitlePrincipals(string id)
    {
        var success = _dataService.DeleteTitlePrincipals(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent(); // Success, no content to return
    }
    
}