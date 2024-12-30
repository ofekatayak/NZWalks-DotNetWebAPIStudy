using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Tests.Controllers;
using Xunit.Abstractions;

namespace NZWalks.API.Controllers
{
    public class RegionsControllerTests
    {
        private readonly ITestOutputHelper _output;
        public RegionsControllerTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task GetAllRequest_Test()
        {
            // Arrange + Act
            var response = await GetAllRegionsListResponse();

            //Assert
            response.EnsureSuccessStatusCode();

            var regions = await response.Content.ReadFromJsonAsync<List<RegionDto>>();
            regions.Should().NotBeNullOrEmpty();

            _output.WriteLine("Regions returned from database:");
            foreach (var region in regions)
                _output.WriteLine($"Id: {region.Id},\tCode: {region.Code}," +
                $"\tName: {region.Name},\tImage URL: {region.RegionImageUrl}");
            regions.Should().NotBeEmpty("because the database has data.");
        }

        [Fact]
        public async Task GetByIDRequest_Test()
        {
            /******* Arrange ******/
            // Create WebApp and Client.
            var apllication = new RegionsWebAplicationFactory();
            var client = apllication.CreateClient();
            // Get The Regions List Response
            var allRegionsResponse = await GetAllRegionsListResponse();
            allRegionsResponse.EnsureSuccessStatusCode();
            // Get The actual Regions List
            var regions = await allRegionsResponse.Content.ReadFromJsonAsync<List<RegionDto>>();
            regions.Should().NotBeNullOrEmpty();
            // Get A Random Region ID From The Regions List
            var random = new Random();
            var randomIndex = random.Next(0, regions.Count);
            Guid randomRegionId = regions[randomIndex].Id;
            
            //Act
            var response = await client.GetAsync($"api/regions/{randomRegionId}");

            // Assert
            response.EnsureSuccessStatusCode();

            var region = await response.Content.ReadFromJsonAsync<RegionDto>();
            region.Should().BeOfType<RegionDto>();

            _output.WriteLine("Region returned from database:");
            _output.WriteLine($"Id: {region.Id},\tCode: {region.Code}," +
                $"\tName: {region.Name},\tImage URL: {region.RegionImageUrl}");
        }

        private async Task<HttpResponseMessage> GetAllRegionsListResponse()
        {
            var apllication = new RegionsWebAplicationFactory();
            var client = apllication.CreateClient();
            var response = await client.GetAsync("api/regions");
            return response;
        }
    }
}
