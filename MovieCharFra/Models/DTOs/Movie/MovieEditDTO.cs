using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharFra.Models.DTOs.Movie
{
    public class MovieEditDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }

        public int Year { get; set; }
        public string Director { get; set; }

        public string Picture { get; set; }

        public string Trailer { get; set; }

        public List<int> Characters { get; set; }

        public int Franchise { get; set; }
    }
}
