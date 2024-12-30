using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace NZWalks.API.Tests.Controllers;
internal class RegionsWebAplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var testImagesPath = Path.Combine(AppContext.BaseDirectory, "TestImages");

        if (!Directory.Exists(testImagesPath))
        {
            Directory.CreateDirectory(testImagesPath);
        }

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "StaticFiles:ImagesPath", testImagesPath }
            });
        });

        base.ConfigureWebHost(builder);
    }

}

