using WeerEventsApi.Steden;
using WeerEventsApi.Metingen;
using WeerEventsApi.Logging;

namespace WeerEventsApi.Stations.Factories
{
    public class WeerstationFactory
    {
        private readonly IMetingLogger _logger;
        private readonly List<Stad> _steden;
        private readonly Random _rand = new();

        public WeerstationFactory(IMetingLogger logger, List<Stad> steden)
        {
            _logger = logger;
            _steden = steden;
        }

        public List<IWeerstation> GenereerWillekeurigeStations(int aantal)
        {
            var stations = new List<IWeerstation>();
            for (int i = 0; i < aantal; i++)
            {
                var stad = _steden[_rand.Next(_steden.Count)];
                stations.Add(CreateRandomStation(stad));
            }
            return stations;
        }

        private IWeerstation CreateRandomStation(Stad stad)
        {
            return _rand.Next(4) switch
            {
                0 => new WindStation(stad, _logger),
                1 => new NeerslagStation(stad, _logger),
                2 => new TemperatuurStation(stad, _logger),
                _ => new LuchtdrukStation(stad, _logger),
            };
        }
    }
}
