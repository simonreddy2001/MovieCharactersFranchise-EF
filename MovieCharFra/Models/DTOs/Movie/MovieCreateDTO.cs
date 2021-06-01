using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharFra.Models.DTOs.Movie
{
    public class MovieCreateDTO
    {
        public string Title { get; set; }
        public string Genre { get; set; }

        public int Year { get; set; }
        public string Director { get; set; }

        public int Franchise { get; set; }
    }
}
