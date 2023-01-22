namespace Back1.ViewModels
{
    public class PostVoitureViewModel
    {
        public string UrlPhoto { get; set; }
        public int NombrePassagers { get; set; }
        public string IdProp { get; set; }

        public int  IdCategorie { get; set; }
        public int IdMarque { get; set; }

        public string? Couleur { get; set; }

        public double Prix { get; set; }

        public int Annee { get; set; }

        public int Km { get; set; }

        public string? Immatriculation { get; set; }

    }
}
