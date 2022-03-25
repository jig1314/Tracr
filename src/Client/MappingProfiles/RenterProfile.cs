using AutoMapper;
using Tracr.Client.ViewModels;
using Tracr.Shared.DTOs;

namespace Tracr.Client.MappingProfiles
{
    public class RenterProfile : Profile
    {
        public RenterProfile()
        {
            CreateMap<RenterDto, RenterTableViewModel>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => src.LastName))
                .ForMember(
                    dest => dest.MonthlyRent,
                    opt => opt.MapFrom(src => src.MonthlyRent.ToString("C")))
                .ForMember(
                    dest => dest.StartingMonth,
                    opt => opt.MapFrom(src => src.StartingMonth.ToString("MM/dd/yyyy")))
                .ForMember(
                    dest => dest.EndingMonth,
                    opt => opt.MapFrom(src => src.EndingMonth.ToString("MM/dd/yyyy")));

            CreateMap<RenterViewModel, RenterDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => src.LastName))
                .ForMember(
                    dest => dest.MonthlyRent,
                    opt => opt.MapFrom(src => decimal.Parse(src.MonthlyRent)))
                .ForMember(
                    dest => dest.StartingMonth,
                    opt => opt.MapFrom(src => src.StartingMonth.GetValueOrDefault().ToDateTime(TimeOnly.MinValue)))
                .ForMember(
                    dest => dest.EndingMonth,
                    opt => opt.MapFrom(src => src.EndingMonth.GetValueOrDefault().ToDateTime(TimeOnly.MinValue)));


            CreateMap<RenterDto, RenterViewModel>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => src.LastName))
                .ForMember(
                    dest => dest.MonthlyRent,
                    opt => opt.MapFrom(src => src.MonthlyRent.ToString()))
                .ForMember(
                    dest => dest.StartingMonth,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime(src.StartingMonth)))
                .ForMember(
                    dest => dest.EndingMonth,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime(src.EndingMonth)));
        }
    }
}
