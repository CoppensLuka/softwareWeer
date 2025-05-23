using Microsoft.AspNetCore.Mvc;
using WeerEventsApi.Facade.Dto;
using WeerEventsApi.Logging;
using WeerEventsApi.Stations;
using WeerEventsApi.Steden.Managers;
using WeerEventsApi.Weerberichten.WeerberichtGenerators;
using WeerEventsApi.Metingen;

namespace WeerEventsApi.Facade.Controllers;

public class DomeinController : IDomeinController
{
    private readonly IStadManager _stadManager;
    private readonly IEnumerable<AbstractWeerstation> _stations;
    private readonly IMetingLogger _logger;
    private readonly IWeerberichtGenerator _weerberichtGenerator;

    public DomeinController(
        IStadManager stadManager,
        IEnumerable<AbstractWeerstation> stations,
        IMetingLogger logger,
        IWeerberichtGenerator weerberichtGenerator)
    {
        _stadManager = stadManager;
        _stations = stations;
        _logger = logger;
        _weerberichtGenerator = weerberichtGenerator;
    }

    public IEnumerable<StadDto> GeefSteden()
    {
        return _stadManager.GeefSteden().Select(s => new StadDto
        {
            Naam = s.Naam,
            Beschrijving = s.Beschrijving,
            GekendVoor = s.GekendVoor
        });
    }

    public IEnumerable<WeerStationDto> GeefWeerstations()
    {
        //List<StadDto> steden = GeefSteden().ToList();
        List<AbstractWeerstation> stations = _stations.ToList();
        return stations.Select(s => new WeerStationDto
        {
            Id = s.Id,
            StadDto = stations
                .Where(st => st.Locatie.Naam == s.Locatie.Naam)
                .Select(st => new StadDto
                {
                    Naam = st.Locatie.Naam,
                    Beschrijving = st.Locatie.Beschrijving,
                    GekendVoor = st.Locatie.GekendVoor
                }).FirstOrDefault(),
            MetingenDto = s.Metingen.Select(m => new MetingDto
            {
                Moment = m.Moment,
                Waarde = m.Waarde,
                Eenheid = m.Eenheid,
                StadNaam = s.Locatie.Naam
            }).ToList()
        });
    }

    public IEnumerable<MetingDto> GeefMetingen()
    {
        List<Meting> metingen = _stations
            .SelectMany(s => s.GetMetingen())
            .ToList();


        return metingen
            .Select(m => new MetingDto
            {
                Moment = m.Moment,
                Waarde = m.Waarde,
                Eenheid = m.Eenheid,
                StadNaam = m.Stad.Naam
            });
    }

    public void MaakMetingen()
    {
        foreach (var station in _stations)
        {
            station.MaakMeting();
            //_logger.Log(meting);
        }
    }

    public WeerBerichtDto GeefWeerbericht()
    {
        List<Meting> metingen = _stations
            .SelectMany(s => s.GetMetingen())
            .ToList();

        var bericht = _weerberichtGenerator.GenereerWeerbericht(metingen);
        return new WeerBerichtDto
        {
            Moment = bericht.Moment,
            Inhoud = bericht.Inhoud
        };
    }

}
