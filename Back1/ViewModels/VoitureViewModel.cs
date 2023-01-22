namespace Back1.ViewModels
{
    public class VoitureViewModel
    {
        public string UrlPhoto { get; set; }
        public int NombrePassagers { get; set; }
        public string  INomProprietaire { get; set; }

        public string NomCategorie { get; set; }
        public string NomMarque { get; set; }

        public string? Couleur { get; set; }

        public double Prix { get; set; }

        public int Annee { get; set; }

        public int Km { get; set;}

        public string? Immatriculation { get; set; }

        public bool Disponible { get; set; }


    }
}
