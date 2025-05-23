using WeerEventsApi.Metingen;

namespace WeerEventsApi.Weerberichten.WeerberichtGenerators
{
    public interface IWeerberichtGenerator
    {
        Weerbericht GenereerWeerbericht(List<Meting> metingen);
    }
}
