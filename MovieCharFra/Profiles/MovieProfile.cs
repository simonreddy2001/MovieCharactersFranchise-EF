using AutoMapper;
using MovieCharFra.Models;
using MovieCharFra.Models.DTOs.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharFra.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            // Movie<->MovieReadDTO
            CreateMap<Movie, MovieReadDTO>()
                .ForMember(mdto => mdto.Franchise, opt => opt
                 .MapFrom(m => m.FranchiseId))
                .ForMember(mdto => mdto.Characters, opt => opt
                .MapFrom(m => m.Characters.Select(m => m.Id).ToList()))
                .ReverseMap();
            // Movie<->MovieCreateDTO
            CreateMap<Movie, MovieCreateDTO>()
                .ForMember(mdto => mdto.Franchise, opt => opt
                .MapFrom(m => m.FranchiseId))
                .ReverseMap();
            // Movie<->MovieEditDTO
            CreateMap<Movie, MovieEditDTO>()
                .ForMember(mdto => mdto.Franchise, opt => opt
                .MapFrom(m => m.FranchiseId))
                .ForMember(mdto => mdto.Characters, opt => opt
                .MapFrom(m => m.Characters.Select(m => m.Id).ToList()))
                .ReverseMap();
        }

    }
}
