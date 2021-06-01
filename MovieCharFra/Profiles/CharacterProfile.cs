using AutoMapper;
using MovieCharFra.Models;
using MovieCharFra.Models.DTOs.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharFra.Profiles
{
    public class CharacterProfile : Profile
    {
        
        public CharacterProfile()
        {
            // Character<->CharacterReadDTO
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(cdto => cdto.Movies, opt => opt
                .MapFrom(c => c.Movies.Select(m => m.Id).ToList()))
                    .ReverseMap();
            // Character<->CharacterCreateDTO
            CreateMap<Character, CharacterCreateDTO>()
                    .ReverseMap();
            // Character<->CharacterEditDTO
            CreateMap<Character, CharacterEditDTO>()
                    .ReverseMap();
        }
    }
}
