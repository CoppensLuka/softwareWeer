using WeerEventsApi.Metingen;

namespace WeerEventsApi.Weerberichten.WeerberichtGenerators
{
    public class WeerberichtGeneratorProxy : IWeerberichtGenerator
    {
        private readonly IWeerberichtGenerator _echteGenerator;
        private Weerbericht? _cachedBericht;
        private DateTime _laatsteGenereerTijd;
        private readonly TimeSpan _cacheDuur = TimeSpan.FromMinutes(1);

        public WeerberichtGeneratorProxy(IWeerberichtGenerator echteGenerator)
        {
            _echteGenerator = echteGenerator;
        }

        public Weerbericht GenereerWeerbericht(List<Meting> metingen)
        {
            if (_cachedBericht == null || DateTime.Now - _laatsteGenereerTijd > _cacheDuur)
            {
                _cachedBericht = _echteGenerator.GenereerWeerbericht(metingen);
                _laatsteGenereerTijd = DateTime.Now;
            }

            return _cachedBericht;
        }
    }
}
