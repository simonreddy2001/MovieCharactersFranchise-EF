using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharFra.Models.DTOs.Character
{
    public class CharacterCreateDTO
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }

    }
}
