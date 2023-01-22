using Back1.Models;
using Back1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Back1.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<UserGetViewModel>>GetListFavoriAsync();
        Task<IEnumerable<UserGetViewModel>> GetListNoirAsync();
        Task<IEnumerable<string>> GetAllMarks();
        //Task<IEnumerable<VoitureViewModel>> GetVoituresByMarque(string marque);
        Task<IEnumerable<UserGetViewModel>> GetAllUsers();
        Task Delete(User user);
        Task<User> GetById(string id);
        Task<IEnumerable<VoitureViewModel>> GetAllVoitures();
        Task<UserManagerResponse> PostVoiture(PostVoitureViewModel model);
        Task<IEnumerable<string>> GetAllProprietaires();
        Task<int> GetNombreVoitures();
        Task<int> GetNombreClients();
        Task<int> GetNombreReservation();
        Task<int> GetNombrePlainte();
        Task<UserManagerResponse> addToListeNoir(string Id);
        Task<UserManagerResponse> addToListeFavori(string Id);
        Task<bool> UpdateUser(UserGetViewModel model, string id);
        Task<IEnumerable<string>> AllCategories();
        Task<UserManagerResponse> RemoveFromListeFavori(string Id);
        Task<UserManagerResponse> RemoveFromListeNoir(string Id);
        Task DeleteVoiture(Voiture voiture);
        Task <string> GetIdbyEmail(string email);
    }

    public class AdminService : IAdminService
    {
        private  ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public AdminService(ApplicationDbContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IEnumerable<UserGetViewModel>> GetListFavoriAsync()
        {
            var a = await _context.Users.Where(v => v.ListeFavori == true).ToListAsync();

            var e = new List<UserGetViewModel>();
            foreach(User i in a)
            {
                IdentityUser Iuser = i;
                var role = await _userManager.GetRolesAsync(Iuser);
                UserGetViewModel x = new UserGetViewModel();
                x.Adress = i.Adress;
                x.PhoneNumber = i.PhoneNumber;
                x.FirstName = i.FirstName;
                x.Role = role.First();
                x.Email = i.Email;
                x.Name= i.Name;
                e.Add(x);
            }
            return e;
        }
        public async Task<IEnumerable<UserGetViewModel>> GetListNoirAsync()
        {
            var a = await _context.Users.Where(v => v.ListeNoir == true).ToListAsync();

            var e = new List<UserGetViewModel>();
            foreach (User i in a)
            {
                IdentityUser Iuser = i;
                var role = await _userManager.GetRolesAsync(Iuser);
                UserGetViewModel x = new UserGetViewModel();
                x.Adress = i.Adress;
                x.PhoneNumber = i.PhoneNumber;
                x.FirstName = i.FirstName;
                x.Role = role.First();
                x.Email = i.Email;
                x.Name = i.Name;
                e.Add(x);
            }
            return e;
        }
        public async Task<IEnumerable<string>> GetAllMarks()
        {
            var a = await _context.Marques.ToListAsync();

            var e = new List<string>();
            foreach(var x in a)
                e.Add(x.Name); 
            
            return e;
        }
        public async Task<IEnumerable<UserGetViewModel>> GetAllUsers()
        {
            List<UserGetViewModel> result = new List<UserGetViewModel>();

            var users = await _context.Users.Where(x=>x==x).ToListAsync();
            foreach ( var user in users)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                UserGetViewModel model = new UserGetViewModel();

                model.Adress = user.Adress;
                model.Email = user.Email;
                model.FirstName = user.FirstName;
                model.PhoneNumber = user.PhoneNumber;
                model.Name = user.Name;
                model.Role = userRole.First();
                model.IdUser = user.Id;
                
                result.Add(model);
            }
            return result;
        }
        public async Task<IEnumerable<VoitureViewModel>> GetAllVoitures()
        {
            List<VoitureViewModel> result = new List<VoitureViewModel>();

            var voitures = await _context.Voitures.ToListAsync();
            foreach (var v in voitures)
            {
                Categorie c = _context.Categories.Where(x => x.Id == v.IdCatego).FirstOrDefault();

                Marque m = _context.Marques.Where(x => x.Id == v.IdMarque).FirstOrDefault();

                User u = _context.Users.Where(x => x.Id == v.IdProp).FirstOrDefault();
                
                /*if(c == null || m == null || u == null) 
                    return null;*/
                
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
        public async Task Delete(User user)
        {
            await _userManager.DeleteAsync(user);
        }
        public async Task DeleteVoiture(Voiture voiture)
        {
            _context.Voitures.Remove(voiture);
            _context.SaveChanges();
        }
        public async Task<User> GetById(string id)
        {
            return (User) await _userManager.FindByIdAsync(id);
        }
        public async Task<UserManagerResponse> PostVoiture(PostVoitureViewModel model)
        {
           /* try
            {*/
                
                var voiture = new Voiture()
                {
                    Prix = model.Prix,
                    Couleur = model.Couleur,
                    IdMarque = model.IdMarque,
                    Annee = model.Annee,
                    DateAjout = DateTime.Now,
                    Disponible = true,
                    IdCatego = model.IdCategorie,
                    IdProp = model.IdProp,
                    Immatriculation = model.Immatriculation,
                    Km = model.Km,
                    NombrePassager = model.NombrePassagers,
                    photo = model.UrlPhoto
                };
                 _context.Voitures.Add(voiture);
                 _context.SaveChanges();
                

            return new UserManagerResponse() { Message="Voiture ajouté",IsSuccess=true };

           /* }catch(Exception ex)
            {
                return new UserManagerResponse() { Message = "Error", IsSuccess = false };
            }*/
        }
        public async  Task<IEnumerable<string>> GetAllProprietaires()
        {
            var users = _context.Users.ToList();
            List<string> result = new List<string>();
            foreach(var x in users)
            {
                var userRole = await _userManager.GetRolesAsync(x);
                string role = userRole.First();
                if(role == "proprietaire")
                {
                    string N = x.Name + " " + x.FirstName;
                    result.Add(N);
                }
            }
            return result;
        }
        public async Task<UserManagerResponse> addToListeFavori(string Id)
        {
            var user = await _context.Users.FindAsync(Id);
            
            if(user == null)
                return new UserManagerResponse { Message = "Client not found", IsSuccess = false };
            
            var userRole = await _userManager.GetRolesAsync(user);
            
            if (userRole.First() == "admin")
                return new UserManagerResponse { Message = "admins can't be in this liste", IsSuccess = false };

            user.ListeNoir= false;
            user.ListeFavori = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new UserManagerResponse { Message = "client is in Favorite list now",IsSuccess=true};
        }
        public async Task<UserManagerResponse> RemoveFromListeFavori(string Id)
        {
            var user = await _context.Users.FindAsync(Id);

            if (user == null)
                return new UserManagerResponse { Message = "Client not found", IsSuccess = false };

            var userRole = await _userManager.GetRolesAsync(user);

            if (userRole.First() == "admin")
                return new UserManagerResponse { Message = "admins can't be in this liste", IsSuccess = false };

            
            user.ListeFavori = false;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new UserManagerResponse { Message = "client is in Favorite list now", IsSuccess = true };
        }
        public async Task<UserManagerResponse> RemoveFromListeNoir(string Id)
        {
            var user = await _context.Users.FindAsync(Id);

            if (user == null)
                return new UserManagerResponse { Message = "Client not found", IsSuccess = false };

            var userRole = await _userManager.GetRolesAsync(user);

            if (userRole.First() == "admin")
                return new UserManagerResponse { Message = "admins can't be in this liste", IsSuccess = false };

            
            user.ListeNoir = false;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new UserManagerResponse { Message = "client is in Favorite list now", IsSuccess = true };
        }
        public async Task<UserManagerResponse> addToListeNoir(string Id)
        {
            var user = await _context.Users.FindAsync(Id);

            if (user == null)
                return new UserManagerResponse { Message = "Client not found", IsSuccess = false };

            var userRole = await _userManager.GetRolesAsync(user);

            if (userRole.First() == "admin")
                return new UserManagerResponse { Message = "admins can't be in this liste", IsSuccess = false };
            
            user.ListeFavori = false;
            user.ListeNoir = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new UserManagerResponse { Message = "client is in black list now", IsSuccess = true };
        }
        public async Task<int> GetNombreVoitures()
        {
            return _context.Voitures.Count();
        }
        public async Task<int> GetNombreClients()
        {
            var users = _context.Users.ToList();
            int result = 0;

            foreach (var x in users)
            {
                var userRole = await _userManager.GetRolesAsync(x);
                string role = userRole.First();
                if (role == "proprietaire" || role == "locataire")
                {
                    result++;
                }
            }
            
            return result;
        }
        public async Task<int> GetNombreReservation()
        {
            return _context.Reservations.Count();
        }
        public async Task<int> GetNombrePlainte()
        {
            return _context.Plaintes.Count();
        }
        public async Task<bool> UpdateUser(UserGetViewModel model, string id)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user == null)
                return false;
            user.Adress = model.Adress;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.PhoneNumber = model.PhoneNumber;
            user.Name = model.Name;
            user.UserName = model.Email;
            await _context.SaveChangesAsync();
            return true;
        }
         
        public async Task<IEnumerable<string>> AllCategories()
        {
            var categorie =  _context.Categories.ToList();
            List<string> categories = new List<string>();
            foreach (var category in categorie)
            {
                categories.Add(category.Name);
            }
            return categories;
        }
        public async Task<string> GetIdbyEmail(string email)
        {
            var user = _context.Users.Where(x => x.Email== email).FirstOrDefault();
            return user.Id;
        }
    }
}
