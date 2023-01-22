using System.ComponentModel.DataAnnotations.Schema;

namespace Back1.Models
{
    public class Offre
    {
        public int Id { get; set; }
        
        public string Libelle { get; set; }

        public double Remise { get; set; }

        [ForeignKey("Voiture")]
        public int IdVoiture { get; set; }
        

        
    }
}
