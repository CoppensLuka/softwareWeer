
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using WeerEventsApi.Logging;
using WeerEventsApi.Steden.Repositories;
using WeerEventsApi.Steden.Managers;
using WeerEventsApi.Facade.Controllers;
using WeerEventsApi.Stations.Factories;
using WeerEventsApi.Weerberichten.WeerberichtGenerators;
using WeerEventsApi.Stations;

var builder = WebApplication.CreateBuilder(args);

//Logging configureren (met decorator pattern)
builder.Services.AddSingleton<IMetingLogger>(provider =>
{
    IMetingLogger logger = new MetingLogger();
    var config = builder.Configuration["Logging"] ?? "xml+json";

    if (config.Contains("xml"))
        logger = new XmlMetingLoggerDecorator(logger);
    if (config.Contains("json"))
        logger = new JsonMetingLoggerDecorator(logger);

    return logger;
});

//StadRepository en StadManager
builder.Services.AddSingleton<IStadRepository, StadRepository>();
builder.Services.AddSingleton<IStadManager, StadManager>();


builder.Services.AddSingleton(provider =>
{
    var logger = provider.GetRequiredService<IMetingLogger>();
    var stadRepository = provider.GetRequiredService<IStadRepository>();
    WeerstationFactory factory = new WeerstationFactory(logger, stadRepository.GetSteden().ToList());
    return factory.GenereerWillekeurigeStations(12);
});

//WeerberichtGenerator met caching via proxy
builder.Services.AddSingleton<IWeerberichtGenerator>(provider =>
{
    var echte = new EchteWeerberichtGenerator();
    return new WeerberichtGeneratorProxy(echte);
});

builder.Services.AddSingleton<IEnumerable<AbstractWeerstation>>(provider =>
{
    var logger = provider.GetRequiredService<IMetingLogger>();
    var stadRepository = provider.GetRequiredService<IStadRepository>();
    WeerstationFactory factory = new WeerstationFactory(logger, stadRepository.GetSteden().ToList());
    return factory.GenereerWillekeurigeStations(12).Cast<AbstractWeerstation>().ToList();
});

//Domeincontroller als facade
builder.Services.AddSingleton<IDomeinController, DomeinController>();
builder.Services.AddSingleton<DomeinController>();


//Controllers & JSON config
builder.Services.AddControllers()
    .AddJsonOptions(o =>
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

var app = builder.Build();

//Routing via Controllers (bv. WeerController.cs)
//
app.MapControllers();

app.Run();
