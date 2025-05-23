using WeerEventsApi.Steden.Repositories;

namespace WeerEventsApi.Steden.Managers;

public class StadManager(IStadRepository repository) : IStadManager
{
    public IEnumerable<Stad> GeefSteden()
    {
        return repository.GetSteden();
    }
}