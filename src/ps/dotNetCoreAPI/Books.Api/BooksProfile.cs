﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Books.Api.Entities;
using Books.Api.ExternalModels;
using Books.Api.Models;

namespace Books.Api
{
    // AutoMapping.cs via profile inheritance
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Book, BookListDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src =>
                    src.Author.FirstName + " " + src.Author.LastName));
            CreateMap<BookCreationDto, Book>();
            CreateMap<Book, BookWithCoversDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(
                    src => $"{ src.Author.FirstName} {src.Author.LastName}"));
            CreateMap<IEnumerable<BookCover>, BookWithCoversDto>()
                .ForMember(dest => dest.BookCovers, opt => opt.MapFrom(
                    src => src));
        }
    }
}
