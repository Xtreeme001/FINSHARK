using AutoMapper;
using FINSHARK.Dtos.Comment;
using FINSHARK.Dtos.Stock;
using FINSHARK.Models;

namespace FINSHARK.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Stock, StockDto>().ReverseMap();
            CreateMap<Stock, CreateStockDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.AppUser.UserName));
            CreateMap<Comment, CreateCommentDto>().ReverseMap();
            CreateMap<Comment, UpdateCommentDto>().ReverseMap();
        }
    }
}