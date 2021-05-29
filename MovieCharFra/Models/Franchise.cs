using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharFra.Models
{
    public class Franchise
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }
        //relationship one to many (franchise to movie)
        public ICollection<Movie> Movies { get; set; }
    }
}
