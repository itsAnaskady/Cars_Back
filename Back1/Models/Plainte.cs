using System.ComponentModel.DataAnnotations.Schema;

namespace Back1.Models
{
    public class Plainte
    {
        public int Id { get; set; }
        
        public string Description { get; set; }


        [ForeignKey("User")]
        public int IdUser { get; set; }
       


    }
}
