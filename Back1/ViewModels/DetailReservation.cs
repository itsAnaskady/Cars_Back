namespace Back1.ViewModels
{
    public class DetailReservation
    {
        public string NomProp { set; get;}

        public string NomLocataire { set; get;}

        public DateTime DateReservation { get; set; }

        public DateTime DateRetour { get; set; }

        public string Remarque { get; set; }

        public double Mantant { get; set; }

        public string modePaiement{ get; set;}

    }
}
