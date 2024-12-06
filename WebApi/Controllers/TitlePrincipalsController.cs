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
    [HttpGet("{nConst}/{category}", Name = nameof(GetTitlePrincipalsById))]
    public ActionResult<TitlePrincipals> GetTitlePrincipalsById(string nConst, string category)
    {
        var title = _dataService.GetTitlePrincipalsById(nConst, category);

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
    public ActionResult<TitlePrincipals> CreateTitlePrincipals([FromBody] TitlePrincipalsCreateModel newTitle)
    {
        var titleEntity = new TitlePrincipals
        {
            TConst = newTitle.TConst,
            Ordering = newTitle.Ordering,
            NConst = newTitle.NConst,
            Category = newTitle.Category,
            Job = newTitle.Job,
            Characters = newTitle.Characters
        };
        
        var createdTitle = _dataService.AddTitlePrincipals(titleEntity);

        if (createdTitle == null)
        {
            return BadRequest("An entry with this TConst already exists.");
        }
        
        var model = CreateTitlePrincipalsModel(createdTitle);
        return CreatedAtAction(nameof(GetTitlePrincipalsById), new { nConst = createdTitle.NConst, category = createdTitle.Category }, model);
    }
    
    
    // PUT: api/TitlePrincipals/{id}
    [HttpPut("{nConst}/{category}")]
    public IActionResult UpdateTitlePrincipals(string nConst, string category, [FromBody] TitlePrincipalsCreateModel updatedTitle)
    {
        var updatedEntity = new TitlePrincipals
        {
            TConst = updatedTitle.TConst,
            Ordering = updatedTitle.Ordering,
            NConst = updatedTitle.NConst,
            Category = updatedTitle.Category,
            Job = updatedTitle.Job,
            Characters = updatedTitle.Characters
        };
        
        var success = _dataService.UpdateTitlePrincipals(nConst, category, updatedEntity);

        if (!success)
        {
            return NotFound();
        }

        return NoContent(); // Success, no content to return
    }
    
    
    // DELETE: api/TitlePrincipals/{id}
    [HttpDelete("{nConst}/{category}")]
    public IActionResult DeleteTitlePrincipals(string nConst, string category)
    {
        var success = _dataService.DeleteTitlePrincipals(nConst, category);

        if (!success)
        {
            return NotFound("An entry with this TConst dosent exists.");
        }

        return NoContent(); // Success, no content to return
    }
    
    // Helper method to create TitleBasicsModel with URL
    private TitlePrincipalsModel CreateTitlePrincipalsModel(TitlePrincipals title)
    {
        return new TitlePrincipalsModel
        {
            TConst = title.TConst,
            Ordering = title.Ordering,
            NConst = title.NConst,
            Category = title.Category,
            Job = title.Job,
            Characters = title.Characters,
            Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetTitlePrincipalsById), new { nConst = title.NConst, category = title.Category })
        };
    }
}