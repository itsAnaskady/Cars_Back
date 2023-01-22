using Back1.Models;
using Back1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Back1.Services
{
    public interface ILocataireService
    {
        Task<DetailReservation> AjouterReservationAsync(ReservationForm model);
        Task<IEnumerable<VoitureViewModel>> GetAllVoituresNonDispo();
        Task<IEnumerable<VoitureViewModel>> GetAllVoituresDispo();
        Task<IEnumerable<ModePaiement>> GetAllModePaiement();
        Task<IEnumerable<DetailReservation>> GetAllReservations();
        Task<IEnumerable<DetailReservation>> GetAllReservationsByIdLocataire(string id);
        Task<IEnumerable<DetailReservation>> GetAllReservationsByIdProp(string id);

    }
    public class LocataireService : ILocataireService
    {
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;
        public LocataireService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<DetailReservation> AjouterReservationAsync(ReservationForm model)
        {
            User loca = await _context.Users.FindAsync(model.IdLocataire);
            
            Voiture v = await _context.Voitures.FindAsync(model.IdVoiture);
            ModePaiement mp = await _context.ModePaiments.FindAsync(model.IdModePaiement);

            User pro = _context.Users.Where(x => x.Id == v.IdProp).ToList().First();

            Reservation Myreservation = new Reservation()
            {
                DateReservation = DateTime.Now,
                DateRetour = model.DateRetour,
                IdLocataire = loca.Id,
                IdModePaiement = model.IdModePaiement,
                IdVoiture = model.IdVoiture,
                Mantant = model.Mantant,
                Remarque = model.Remarque,
            };
            await _context.Reservations.AddAsync(Myreservation);
            await _context.SaveChangesAsync();
            
            DetailReservation modelDetail = new DetailReservation()
            {
                Remarque= model.Remarque,
                DateReservation= Myreservation.DateReservation,
                DateRetour= model.DateRetour,
                Mantant = model.Mantant,
                NomLocataire = loca.Name,
                NomProp = pro.Name,
                modePaiement = mp.Libellé
            };

            v.Disponible = false;
            _context.Voitures.Update(v);
            await _context.SaveChangesAsync();

            return modelDetail;
        }
        public async Task<IEnumerable<VoitureViewModel>> GetAllVoituresDispo()
        {
            List<VoitureViewModel> result = new List<VoitureViewModel>();

            var voitures = await _context.Voitures.Where(x => x.Disponible == true).ToListAsync();

            foreach (var v in voitures)
            {
                Categorie c = _context.Categories.Where(x => x.Id == v.IdCatego).FirstOrDefault();

                Marque m = _context.Marques.Where(x => x.Id == v.IdMarque).FirstOrDefault();

                User u = _context.Users.Where(x => x.Id == v.IdProp).FirstOrDefault();

                VoitureViewModel model = new VoitureViewModel();
                model.Prix = v.Prix;
                model.NomCategorie = c.Name;
                model.INomProprietaire = u.Name;
                model.Annee = v.Annee;
                model.NomMarque = m.Name;
                model.Couleur = v.Couleur;
                model.Disponible = v.Disponible;
                model.Immatriculation = v.Immatriculation;
                model.Km = v.Km;
                model.NombrePassagers = v.NombrePassager;
                model.UrlPhoto = v.photo;

                result.Add(model);
            }
            
            return result;
        }
        public async Task<IEnumerable<VoitureViewModel>> GetAllVoituresNonDispo()
        {
            List<VoitureViewModel> result = new List<VoitureViewModel>();

            var voitures = await _context.Voitures.Where(x => x.Disponible == false).ToListAsync();

            foreach (var v in voitures)
            {
                Categorie c = _context.Categories.Where(x => x.Id == v.IdCatego).FirstOrDefault();

                Marque m = _context.Marques.Where(x => x.Id == v.IdMarque).FirstOrDefault();

                User u = _context.Users.Where(x => x.Id == v.IdProp).FirstOrDefault();

                VoitureViewModel model = new VoitureViewModel();
                model.Prix = v.Prix;
                model.NomCategorie = c.Name;
                model.INomProprietaire = u.Name;
                model.Annee = v.Annee;
                model.NomMarque = m.Name;
                model.Couleur = v.Couleur;
                model.Disponible = v.Disponible;
                model.Immatriculation = v.Immatriculation;
                model.Km = v.Km;
                model.NombrePassagers = v.NombrePassager;
                model.UrlPhoto = v.photo;

                result.Add(model);
            }

            return result;
        }
        public async Task<IEnumerable<ModePaiement>> GetAllModePaiement()
        {
            return await _context.ModePaiments.ToListAsync();
        }
        public async Task<IEnumerable<DetailReservation>> GetAllReservations()
        {
            var reservations = await _context.Reservations.ToListAsync();

            List<DetailReservation> listeReservation= new List<DetailReservation>();
            
            foreach (Reservation r in reservations)
            {
                ModePaiement mp = _context.ModePaiments.Where(p => p.Id == r.IdModePaiement).ToList().First();
                User loca = _context.Users.Where(i => i.Id == r.IdLocataire).ToList().First();
                Voiture v = _context.Voitures.Where(x => x.Id == r.IdVoiture).ToList().First();
                User Prop = _context.Users.Where(x => x.Id == v.IdProp).ToList().First();

                DetailReservation dr = new DetailReservation()
                {
                    DateReservation = r.DateReservation,
                    DateRetour = r.DateRetour,
                    Mantant = r.Mantant,
                    modePaiement = mp.Libellé,
                    NomLocataire = loca.FirstName + " " + loca.Name,
                    NomProp = Prop.FirstName + " " + Prop.Name,
                    Remarque = r.Remarque
                };
                listeReservation.Add(dr);
            }

            return listeReservation;
        }
        public async Task<IEnumerable<DetailReservation>> GetAllReservationsByIdLocataire(string id)
        {
            var reservations = await _context.Reservations.Where(u => u.IdLocataire == id).ToListAsync();

            List<DetailReservation> listeReservation = new List<DetailReservation>();

            foreach (Reservation r in reservations)
            {
                ModePaiement mp = _context.ModePaiments.Where(p => p.Id == r.IdModePaiement).ToList().First();
                User loca = _context.Users.Where(i => i.Id == r.IdLocataire).ToList().First();
                Voiture v = _context.Voitures.Where(x => x.Id == r.IdVoiture).ToList().First();
                User Prop = _context.Users.Where(x => x.Id == v.IdProp).ToList().First();

                DetailReservation dr = new DetailReservation()
                {
                    DateReservation = r.DateReservation,
                    DateRetour = r.DateRetour,
                    Mantant = r.Mantant,
                    modePaiement = mp.Libellé,
                    NomLocataire = loca.FirstName + " " + loca.Name,
                    NomProp = Prop.FirstName + " " + Prop.Name,
                    Remarque = r.Remarque
                };
                listeReservation.Add(dr);
            }

            return listeReservation;
        }

        public async Task<IEnumerable<DetailReservation>> GetAllReservationsByIdProp(string id)
        {

            List<Voiture> voitures = _context.Voitures.Where(x => x.IdProp == id).ToList();
            List<Reservation> reservations = _context.Reservations.ToList();
            List<Reservation> reserv = new List<Reservation>();
            foreach (Reservation reservation in reservations)
            {
                foreach (Voiture v in voitures)
                {
                    if (reservation.IdVoiture == v.Id)
                    {
                        reserv.Add(reservation);
                    }
                }
            }

                List<DetailReservation> listeReservation = new List<DetailReservation>();

                foreach (Reservation r in reservations)
                {
                    ModePaiement mp = _context.ModePaiments.Where(p => p.Id == r.IdModePaiement).ToList().First();
                    User loca = _context.Users.Where(i => i.Id == r.IdLocataire).ToList().First();
                    Voiture v = _context.Voitures.Where(x => x.Id == r.IdVoiture).ToList().First();
                    User Prop = _context.Users.Where(x => x.Id == v.IdProp).ToList().First();

                    DetailReservation dr = new DetailReservation()
                    {
                        DateReservation = r.DateReservation,
                        DateRetour = r.DateRetour,
                        Mantant = r.Mantant,
                        modePaiement = mp.Libellé,
                        NomLocataire = loca.FirstName + " " + loca.Name,
                        NomProp = Prop.FirstName + " " + Prop.Name,
                        Remarque = r.Remarque

                    };
                    listeReservation.Add(dr);
                }


                return listeReservation;

            

        }
    }
}
