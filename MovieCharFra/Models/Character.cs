using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharFra.Models
{
    public class Character
    {
        
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Alias { get; set; }
        [MaxLength(50)]
        public string Gender { get; set; }

        public string Picture { get; set; }

        public string Trailer { get; set; }

        //relationship 
        public ICollection<Movie> Movies { get; set; }
    }
}
