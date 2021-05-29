using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharFra.Models
{
    public class Movie
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(50)]
        public string Genre { get; set; }

        public int Year { get; set; }
        [MaxLength(50)]
        public string Director { get; set; }

        public string Picture { get; set; }

        public string Trailer { get; set; }
        //relationship
        public ICollection<Character> Characters { get; set; }

        public int FranchiseId { get; set; }

        public Franchise Franchise { get; set; }
    }
}
