using WeerEventsApi.Metingen;
using WeerEventsApi.Steden;

namespace WeerEventsApi.Stations
{
    public interface IWeerstation
    {
        string Id { get; }
        Stad Locatie { get; }
        List<Meting> Metingen { get; }

        void MaakMeting();
        List<Meting> GetMetingen();
    }
}
