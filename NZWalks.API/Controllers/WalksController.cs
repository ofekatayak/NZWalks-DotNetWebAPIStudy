using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CastumActuonFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        private readonly ILogger<RegionsController> logger1;

        public WalksController(IMapper mapper, IWalkRepository walkRepository, ILogger<RegionsController> logger1)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
            this.logger1 = logger1;
        }

        
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalkAsync([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            //Create new walk using Repository
            await walkRepository.CreateAsync(walkDomainModel);

            //Map Domain model to DTO and return DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            //Get all Walks using Repository
            var domainWalks = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            // Create an exception
            throw new Exception("This is a new exception");

            //Map Domain model to DTO and return DTO
            return Ok(mapper.Map<List<WalkDto>>(domainWalks));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetWalkByIdAsync([FromRoute] Guid id)
        {
            var domainWalk = await walkRepository.GetByIdAsync(id);
            if (domainWalk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(domainWalk));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteWalkByIdAsync([FromRoute] Guid id)
        {
            var domianWalk = await walkRepository.DeleteByIdAsync(id);
            if (domianWalk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(domianWalk));
        }

        [HttpPut("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            var domainWalk = mapper.Map<Walk>(updateWalkRequestDto);

            domainWalk = await walkRepository.UpdateAsync(id, domainWalk);

            if (domainWalk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Walk>(domainWalk));
        }
    }
}
