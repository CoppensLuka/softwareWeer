namespace WeerEventsApi.Facade.Dto;
using WeerEventsApi.Metingen;

public class MetingDto
{
    //TODO
    public required DateTime Moment { get; set; }
    public required double Waarde { get; set; }
    public required Eenheid Eenheid { get; set; }
    public required string StadNaam { get; set; }
}