using WeerEventsApi.Facade.Dto;

namespace WeerEventsApi.Facade.Controllers;

public interface IDomeinController
{
    IEnumerable<StadDto> GeefSteden();
    
    IEnumerable<WeerStationDto> GeefWeerstations();

    IEnumerable<MetingDto> GeefMetingen();
    
    void MaakMetingen();
    
    WeerBerichtDto GeefWeerbericht();
}