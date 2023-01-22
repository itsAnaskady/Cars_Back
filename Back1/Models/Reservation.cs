using System.ComponentModel.DataAnnotations.Schema;

namespace Back1.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [ForeignKey("Voiture")]
        public int IdVoiture { get; set; }

        [ForeignKey("User")]
        public string IdLocataire { get; set; }


        [ForeignKey("ModePaiement")]
        public int IdModePaiement { get; set; }
        public DateTime DateReservation { get; set; }

        public DateTime DateRetour { get; set; }

        public string Remarque { get; set; }

        public double Mantant { get; set; } 
    }
}
