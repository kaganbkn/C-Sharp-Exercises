using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.Api.Entities;
using CourseLibrary.Api.Helpers;
using CourseLibrary.Api.Models;

namespace CourseLibrary.Api.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest=>dest.Age,
                    opt=>opt.MapFrom(src=>src.DateOfBirth.GetCurrentAge()));
            CreateMap<Course, CourseDto>();
            CreateMap<CreateAuthorDto, Author>();
        }
    }
}
