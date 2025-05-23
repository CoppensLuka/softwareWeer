using WeerEventsApi.Logging;
using WeerEventsApi.Metingen;
using WeerEventsApi.Steden;

namespace WeerEventsApi.Facade.Dto;

public class WeerStationDto
{
    //TODO
    //private readonly IMetingLogger _logger;
    public required string Id { get; set; }
    public required StadDto StadDto { get; set; }
    public required List<MetingDto> MetingenDto { get; set; }
}