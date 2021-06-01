using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharFra.Models.DTOs.Character
{
    public class CharacterEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }

        public string Picture { get; set; }

        public string Trailer { get; set; }

        public List<int> Movies { get; set; }
    }
}
