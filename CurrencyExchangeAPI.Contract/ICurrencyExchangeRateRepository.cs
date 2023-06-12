using CurrencyExchange.APIService.Models.Domain;
using CurrencyExchange.APIService.Models.DTO;
using CurrencyExchange.APIService.Models.DTOs;

namespace CurrencyExchangeAPI.Contract
{
    public interface ICurrencyExchangeRateRepository
    {
         Task<List<GetCurrenciesResponse>> GetCurrencies();
        Task<decimal> GetExchageRate(string baseCurrency, string targetCurrency, decimal amount,DateTime? date);
        decimal GetExchageRateByCode(string baseCode, string targetCode, decimal amount);
        decimal GetCurrencyRatesByDate(string baseCode, string targetCode,decimal amount, DateTime date);
        List<GetCurrencyRateResponse> GetCurrencyRatesByPeriod(string baseCode, string targetCode, DateTime fromDate, DateTime toDate);

    }
}