using WeerEventsApi.Logging;
using WeerEventsApi.Metingen;
using WeerEventsApi.Steden;

namespace WeerEventsApi.Stations
{
    public abstract class AbstractWeerstation : IWeerstation
    {
        private readonly IMetingLogger _logger;
        public string Id { get; } = Guid.NewGuid().ToString();
        public Stad Locatie { get; }
        public List<Meting> Metingen { get; } = new();

        protected AbstractWeerstation(Stad locatie, IMetingLogger logger)
        {
            Locatie = locatie;
            _logger = logger;
        }

        public void MaakMeting()
        {
            Meting meting = new Meting(GenereerWaarde(), GetEenheid(), Locatie)
            {
                Moment = DateTime.Now
            };
            Metingen.Add(meting);
            _logger.Log(meting);

            //return meting;
            //mizerie met domeincontroller, negeer de return.
        }

        public List<Meting> GetMetingen() => new(Metingen);

        protected abstract double GenereerWaarde();
        protected abstract Eenheid GetEenheid();
    }
}
