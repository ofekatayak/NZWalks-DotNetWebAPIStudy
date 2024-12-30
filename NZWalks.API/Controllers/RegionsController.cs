using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CastumActuonFilters;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegioRepository regioRepository;
        private readonly IMapper autoMapper;
        private readonly ILogger<RegionsController> logger1;

        public RegionsController(IRegioRepository regioRepository, IMapper autoMapper,ILogger<RegionsController> logger1)
        {
            this.regioRepository = regioRepository;
            this.autoMapper = autoMapper;
            this.logger1 = logger1;
        }

        //Get ALL regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                logger1.LogInformation("GetAll Action Method was invoked");
                //GET Data from database - Domain Models using Repository
                var regionsDomain = await regioRepository.GetAllAsync();

                logger1.LogInformation($"Finished GetAll Request with data:{JsonSerializer.Serialize(regionsDomain)}");

                //Map domain Models to DTOs and Return DTOs
                return Ok(autoMapper.Map<List<RegionDto>>(regionsDomain));
            }
            catch (Exception ex) 
            { 
                logger1.LogError(ex,ex.Message);
                throw;
            }
            
        }

        //Get Region By ID
        [HttpGet("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById(Guid id)
        {
            //Get region Domain Model from DB using Repository
            var regionDomain = await regioRepository.GetByIdAsync(id);
            if (regionDomain is null)
                return NotFound();

            //Map Domain Model to DTO and Return DTO
            return Ok(autoMapper.Map<RegionDto>(regionDomain));
        }

        //Create New Region
        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map Region DTO to Region Model
            var regionDomain = autoMapper.Map<Region>(addRegionRequestDto);

            //Use Domain model to create a region with Repository
            await regioRepository.CreateRegionAsync(regionDomain);

            //Map Region Domain Model to Region DTO
            var regionDto = autoMapper.Map<RegionDto>(regionDomain);

            //Return DTO
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id}, regionDto);
        }

        //Update Region
        [HttpPut("{id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionDto)
        {
            //Map DTO to Domain Model
            var regionDomain = autoMapper.Map<Region>(updateRegionDto);

            //Check if region exists and update using Repository
            regionDomain = await regioRepository.UpdateRegionAsync(id, regionDomain);

            if (regionDomain is null)
                return NotFound();

            //Map Region Domain Model to Region DTO and Return DTO
            return Ok(autoMapper.Map<RegionDto>(regionDomain));
        }

        //Delete Region
        [HttpDelete("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteRegionById([FromRoute] Guid id)
        {
            //Get region Domain Model from DB and remove it using Repository
            var regionDomain = await regioRepository.DeleteRegionAsync(id);
            if (regionDomain is null)
                return NotFound();
          
            //Map Domain Model to DTO and Return DTO
            return Ok(autoMapper.Map<RegionDto>(regionDomain));
        }
    }
}
