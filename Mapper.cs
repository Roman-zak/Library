using AutoMapper;
using Library.Data;
using Library.Dtos;
using Library.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Library
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Book, BookOverviewDto>()
                .ForMember(
                    dest => dest.Rating,
                    opt => opt.MapFrom(src => $"{src.Ratings.Average(r=>r.Score)}")
                )
                .ForMember(
                    dest => dest.ReviewsNumber,
                    opt => opt.MapFrom(src => $"{src.Reviews.Count}")
                );

            CreateMap<Book, BookDetalizedDto>()
                .ForMember(
                    dest => dest.Rating, 
                    opt => opt.MapFrom(src => src.Ratings.Any() ? src.Ratings.Average(r => r.Score) : 0))
                .ForMember(
                    dest => dest.Reviews,
                    opt => opt.MapFrom(src => src.Reviews
                        .Select(r => new ReviewGetDto { 
                            Id = r.Id, 
                            Message = r.Message, 
                            Reviewer = r.Reviewer 
                        }))
                );
            CreateMap<BookSaveDto, Book>();


            /*    CreateMap<Review, ReviewDto>()
                    .ForMember(
                        dest => dest.BookId,
                        opt => opt.MapFrom(src => $"{src.Book.Id}")
                    );
                CreateMap<ReviewDto, Review>()
                                .ForMember(
                                    dest => dest.Book,
                                    opt => opt.MapFrom(src => $"{_context.Books.Find(src.BookId)}")
                                );

                CreateMap<Rating, RatingDto>()
                                .ForMember(
                                    dest => dest.BookId,
                                    opt => opt.MapFrom(src => $"{src.Book.Id}")
                                );
                CreateMap<RatingDto, Rating>()
                    .ForMember(
                        dest => dest.Book,
                        opt => opt.MapFrom(src => $"{_context.Books.Find(src.BookId)}")
                    );*/
        }
    }
}
