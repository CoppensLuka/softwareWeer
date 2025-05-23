using WeerEventsApi.Steden;

namespace WeerEventsApi.Metingen
{

    public class Meting
    {
        public DateTime Moment { get; set; }
        public double Waarde { get; set; }
        public Eenheid Eenheid { get; set; }
        public Stad Stad { get; set; }

        public Meting(double waarde, Eenheid eenheid, Stad stad)
        {
            Moment = DateTime.Now;
            Waarde = waarde;
            Eenheid = eenheid;
            Stad = stad;
        }

        public override string ToString() => $"{Moment} - {Waarde} {Eenheid} in {Stad.Naam}";
    }
}
