using System.ComponentModel.DataAnnotations.Schema;

namespace Back1.Models
{
    public class Voiture
    {
        public int Id { get; set; }


        [ForeignKey("User")]
        public string IdProp { get; set; }


        [ForeignKey("Marque")]
        public int IdMarque { get; set; }


        [ForeignKey("Categorie")]
        public int IdCatego { get; set; }
        

        public int NombrePassager { get; set; }

        public string Couleur { get; set; }
        
        public double Prix { get; set; }

        public string photo { get; set; }

        public int Annee { get; set; }

        public int Km { get; set; }

        public string Immatriculation { get; set; }

        public DateTime DateAjout { get; set; }

        public bool Disponible { get; set; }

        

    }
}
