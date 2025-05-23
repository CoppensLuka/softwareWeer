using System.Text.Json;

namespace WeerEventsApi.Steden.Repositories;

public class StadRepository : IStadRepository
{
    public IEnumerable<Stad> GetSteden()
    {
        return JsonSerializer.Deserialize<List<Stad>>(File.ReadAllText("Steden/Data/steden.json"))!;
    }
}