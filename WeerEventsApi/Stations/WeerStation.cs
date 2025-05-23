using WeerEventsApi.Steden;
using WeerEventsApi.Metingen;
using WeerEventsApi.Logging;

namespace WeerEventsApi.Stations
{
    public class WindStation : AbstractWeerstation
    {
        public WindStation(Stad locatie, IMetingLogger logger) : base(locatie, logger) { }
        protected override double GenereerWaarde() => new Random().Next(0, 100);
        protected override Eenheid GetEenheid() => Eenheid.kmh;
    }

    public class NeerslagStation : AbstractWeerstation
    {
        public NeerslagStation(Stad locatie, IMetingLogger logger) : base(locatie, logger) { }
        protected override double GenereerWaarde() => Math.Round(new Random().NextDouble() * 20, 1);
        protected override Eenheid GetEenheid() => Eenheid.mm;
    }

    public class TemperatuurStation : AbstractWeerstation
    {
        public TemperatuurStation(Stad locatie, IMetingLogger logger) : base(locatie, logger) { }
        protected override double GenereerWaarde() => new Random().Next(-10, 35);
        protected override Eenheid GetEenheid() => Eenheid.celsius;
    }

    public class LuchtdrukStation : AbstractWeerstation
    {
        public LuchtdrukStation(Stad locatie, IMetingLogger logger) : base(locatie, logger) { }
        protected override double GenereerWaarde() => new Random().Next(950, 1050);
        protected override Eenheid GetEenheid() => Eenheid.hpa;
    }

}
