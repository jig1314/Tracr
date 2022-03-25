using AutoMapper;
using Tracr.Client.ViewModels;
using Tracr.Shared.DTOs;

namespace Tracr.Client.MappingProfiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<PropertyViewModel, PropertyDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(
                    dest => dest.NumBedrooms,
                    opt => opt.MapFrom(src => int.Parse(src.NumBedrooms)))
                .ForMember(
                    dest => dest.NumBathrooms,
                    opt => opt.MapFrom(src => decimal.Parse(src.NumBathrooms)))
                .ForMember(
                    dest => dest.Mortage,
                    opt => opt.MapFrom(src => new MortageDto()
                    {
                        Principal = decimal.Parse(src.Principal),
                        MonthlyPayment = decimal.Parse(src.MonthlyPayment),
                        APR = decimal.Parse(src.APR)
                    }))
                .ForMember(
                    dest => dest.Address,
                    opt => opt.MapFrom(src => new AddressDto()
                    {
                        StreetAddress = src.StreetAddress,
                        City = src.City,
                        State = src.State,
                        ZipCode = src.ZipCode
                    }));

            CreateMap<PropertyDto, PropertyViewModel>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(
                    dest => dest.NumBedrooms,
                    opt => opt.MapFrom(src => src.NumBedrooms.ToString()))
                .ForMember(
                    dest => dest.NumBathrooms,
                    opt => opt.MapFrom(src => src.NumBathrooms.ToString()))
                .ForMember(
                    dest => dest.Principal,
                    opt => opt.MapFrom(src => src.Mortage.Principal.ToString()))
                .ForMember(
                    dest => dest.MonthlyPayment,
                    opt => opt.MapFrom(src => src.Mortage.MonthlyPayment.ToString()))
                .ForMember(
                    dest => dest.APR,
                    opt => opt.MapFrom(src => src.Mortage.APR.ToString()))
                .ForMember(
                    dest => dest.StreetAddress,
                    opt => opt.MapFrom(src => src.Address.StreetAddress))
                .ForMember(
                    dest => dest.City,
                    opt => opt.MapFrom(src => src.Address.City))
                .ForMember(
                    dest => dest.State,
                    opt => opt.MapFrom(src => src.Address.State))
                .ForMember(
                    dest => dest.ZipCode,
                    opt => opt.MapFrom(src => src.Address.ZipCode));
        }
    }
}
