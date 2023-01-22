using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Back1.Models
{
    public class User : IdentityUser
    {
        
      
        public string Name { get; set; }

        public string FirstName { get; set; }

        public string Adress { get; set; }

        
        public bool ListeNoir { get; set; }

        
        public bool ListeFavori { get; set; }


        //Lists
       



    }
}
