﻿using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccessLayer;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleRatingsController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public TitleRatingsController(
            IDataService dataService,
            LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }
        
        // ********** **********
        // This controller dosen't have any ENDPOINTs used by the frontend
        // However the group decided to keep them just for showcase of basic CRUD for all object
        // Fully aware they are not used for anything at the current state of the application
        // ********** **********
        // Endpoints NOT IN USE
        // ********** **********

        [HttpGet(Name = nameof(GetTitleRatings))]
        public ActionResult<IEnumerable<TitleRatingsModel>> GetTitleRatings(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var TitleRatingsList = _dataService.GetTitleRatingsList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CreateTitleRatingsModel) // Convert to TitleRatingsModel with URL
                .ToList();

            var totalItems = _dataService.GetTitleRatingsCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var result = new PagedResultModel<TitleRatingsModel>
            {
                Items = TitleRatingsList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                NextPage = pageNumber < totalPages
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleRatings), new { pageNumber = pageNumber + 1, pageSize })
                    : null,
                PrevPage = pageNumber > 1
                    ? _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleRatings), new { pageNumber = pageNumber - 1, pageSize })
                    : null
            };
            
            return Ok(result);
        }

        [HttpGet("{tConst}", Name = nameof(GetTitleRatingById))]
        public ActionResult<TitleRatings> GetTitleRatingById(string tConst)
        {
            var rating = _dataService.GetTitleRatingById(tConst);

            if (rating == null)
            {
                return NotFound();
            }

            var model = CreateTitleRatingsModel(rating);
            return Ok(model);
        }

        [HttpPost]
        public ActionResult<TitleRatings> CreateTitleRating([FromBody] TitleRatingsCreateModel newTitleRating)
        {
            var titleEntity = new TitleRatings
            {
                TConst = newTitleRating.TConst,
                AverageRating = newTitleRating.AverageRating,
                NumVotes = newTitleRating.NumVotes
            };
            
            var createdRating = _dataService.AddTitleRating(titleEntity);

            if (createdRating == null)
            {
                return BadRequest("Could not create new title rating");
            }
            
            var model = CreateTitleRatingsModel(createdRating);
            return CreatedAtAction(nameof(GetTitleRatingById), new { tConst = createdRating.TConst }, model);
        }

        [HttpPut("{tConst}")]
        public IActionResult UpdateTitleRating(string tConst, [FromBody] TitleRatingsCreateModel updatedTitleRating)
        {
            var updatedEntity = new TitleRatings
            {
                TConst = updatedTitleRating.TConst,
                AverageRating = updatedTitleRating.AverageRating,
                NumVotes = updatedTitleRating.NumVotes
            };
            
            var success = _dataService.UpdateTitleRating(tConst, updatedEntity);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{tConst}")]
        public IActionResult DeleteTitleRating(string tConst)
        {
            var success = _dataService.DeleteTitleRating(tConst);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }


        private TitleRatingsModel CreateTitleRatingsModel(TitleRatings title)
        {
            return new TitleRatingsModel
            {
                TConst = title.TConst,
                AverageRating = title.AverageRating,
                NumVotes = title.NumVotes,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleRatingById), new { tConst = title.TConst }),
            };
        }
    }
}
