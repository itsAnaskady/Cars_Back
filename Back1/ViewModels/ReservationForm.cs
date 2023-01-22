using System.ComponentModel.DataAnnotations.Schema;

namespace Back1.ViewModels
{
    public class ReservationForm
    {
        public int IdVoiture { get; set; }

        public string IdLocataire { get; set; }

        public int IdModePaiement { get; set; }

        public DateTime DateRetour { get; set; }

        public string Remarque { get; set; }

        public double Mantant { get; set; }

    }
}
