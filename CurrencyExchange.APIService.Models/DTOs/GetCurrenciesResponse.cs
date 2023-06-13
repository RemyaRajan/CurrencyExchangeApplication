namespace CurrencyExchange.APIService.Models.DTOs
{
    public class GetCurrenciesResponse
    {
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; } = null!;

    }
}
