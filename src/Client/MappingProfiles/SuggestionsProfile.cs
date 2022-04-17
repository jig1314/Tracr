using AutoMapper;
using Tracr.Client.ViewModels;
using Tracr.Shared.ResourceParameters;

namespace Tracr.Client.MappingProfiles
{
    public class SuggestionsProfile : Profile
    {
        public SuggestionsProfile()
        {
            CreateMap<SuggestionsFilterViewModel, ForSaleResourceParameters>()
                .ForMember(
                    dest => dest.city,
                    opt => opt.MapFrom(src => src.City))
                .ForMember(
                    dest => dest.stateId,
                    opt => opt.MapFrom(src => src.State))
                .ForMember(
                    dest => dest.price_max,
                    opt => opt.MapFrom(src => (int)decimal.Round(src.MaxListPrice.Value)))
                .ForMember(
                    dest => dest.sortByOption,
                    opt => opt.MapFrom(src => src.SortBy));
        }
    }
}
