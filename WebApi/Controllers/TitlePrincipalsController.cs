using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TitlePrincipalsController : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly LinkGenerator _linkGenerator;

    public TitlePrincipalsController(
        IDataService dataService,
        LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }
    
    
    // GET all etities limited by page or it wont load in swagger
    // GET: api/TitlePrincipals
    [HttpGet(Name = nameof(GetTitlePrincipals))]
    public ActionResult<PagedResultModel<TitlePrincipalsModel>> GetTitlePrincipals(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            return BadRequest("Page number and page size must be greater than zero.");
        }

        var titlePrincipalsList = _dataService.GetTitlePrincipalsList()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(CreateTitlePrincipalsModel)
            .ToList();

        var totalItems = _dataService.GetTitlePrincipalsCount();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        var result = new PagedResultModel<TitlePrincipalsModel>
        {
            Items = titlePrincipalsList,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalItems = totalItems,
            NextPage = pageNumber < totalPages
                ? _linkGenerator.GetUriByName(HttpContext, nameof(GetTitlePrincipals), new { pageNumber = pageNumber + 1, pageSize })
                : null,
            PrevPage = pageNumber > 1
                ? _linkGenerator.GetUriByName(HttpContext, nameof(GetTitlePrincipals), new { pageNumber = pageNumber - 1, pageSize })
                : null
        };

        return Ok(result);
    }
    
    
    // Get spicific title by TConst
    // GET: api/TitlePrincipals/{id}
    [HttpGet("{id}", Name = nameof(GetTitlePrincipalsById))]
    public ActionResult<TitlePrincipals> GetTitlePrincipalsById(string id)
    {
        var title = _dataService.GetTitlePrincipalsById(id);

        if (title == null)
        {
            return NotFound();
        }
        
        var model = CreateTitlePrincipalsModel(title);
        return Ok(model);
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
        
        var model = CreateTitlePrincipalsModel(createdTitle);
        return CreatedAtAction(nameof(GetTitlePrincipalsById), new { id = createdTitle.TConst }, model);
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
    
    // Helper method to create TitleBasicsModel with URL
    private TitlePrincipalsModel CreateTitlePrincipalsModel(TitlePrincipals title)
    {
        return new TitlePrincipalsModel
        {
            TConst = title.TConst,
            NConst = title.NConst,
            Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetTitlePrincipalsById), new { id = title.TConst })
        };
    }
}