using WeerEventsApi.Metingen;

namespace WeerEventsApi.Weerberichten.WeerberichtGenerators
{
    public class EchteWeerberichtGenerator : IWeerberichtGenerator
    {
        public Weerbericht GenereerWeerbericht(List<Meting> metingen)
        {
            Thread.Sleep(5000);

            var inhoud = $"Op basis van {metingen.Count} metingen en mijn diepzinnig computermodel kan ik zeggen dat er kans is op ";
            inhoud += metingen.Any(m => m.Waarde < 5 || m.Waarde > 80) ? "slecht" : "goed";
            inhoud += " weer.";

            return new Weerbericht
            {
                Moment = DateTime.Now,
                Inhoud = inhoud
            };
        }
    }
}
