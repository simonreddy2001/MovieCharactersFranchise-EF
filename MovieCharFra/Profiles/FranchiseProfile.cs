using AutoMapper;
using MovieCharFra.Models;
using MovieCharFra.Models.DTOs.Franchise;
using MovieCharFra.Models.DTOs.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharFra.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseReadDTO>()
                // Turning related movies into int arrays
                .ForMember(cdto => cdto.Movies, opt => opt
                .MapFrom(c => c.Movies.Select(c => c.Id).ToList()))
                .ReverseMap(); ;
;
            // Franchise<->FranchiseCreateDTO
            CreateMap<Franchise, FranchiseCreateDTO>()
                .ReverseMap();
            // Franchise<->FranchiseEditDTO
            CreateMap<Franchise, FranchiseEditDTO>()
                 .ForMember(cdto => cdto.Movies, opt => opt
                .MapFrom(c => c.Movies.Select(c => c.Id).ToList()))
                 .ReverseMap(); ;
        }

    }
}

