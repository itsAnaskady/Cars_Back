using Back1.Models;
using Microsoft.AspNetCore.Identity;

namespace Back1.Services
{
    public interface IProprietaireService
    {
    }

    public class ProprietaireService : IProprietaireService
    {
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public ProprietaireService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

    }
    
}
