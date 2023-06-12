using CurrencyExchange.APIService.Models.Domain;
using CurrencyExchange.APIService.Models.DTOs;

namespace CurrencyExchange.APIService.DataAccess.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetCurrencyRateResponse, CurrencyRate>().ReverseMap();
            CreateMap<CurrencyMaster,GetCurrenciesResponse>();
        }
    }
}
