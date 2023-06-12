
namespace CurrencyExchange.APIService.Models.DTO
{
    public class GetCurrencyRateResponse
    {
        public decimal ExchageRate { get; set; }
        public DateTime Date { get; set; }

    }

    public class ExchangeRates
    {
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }

        public static explicit operator List<GetCurrencyRateResponse>(ExchangeRates tmpRates)
        {
            var exchangeRates = new List<GetCurrencyRateResponse>();

            if (tmpRates.Rates != null)
            {
                GetCurrencyRateResponse exchangeRate = null;
                foreach (var de in tmpRates.Rates)
                {
                    exchangeRate = new GetCurrencyRateResponse();
                    exchangeRate.Date = tmpRates.Date;
                    exchangeRate.ExchageRate = de.Value;
                }
            }
            return exchangeRates;
        }

        public static explicit operator GetCurrencyRateResponse(ExchangeRates tmpRates)
        {
            GetCurrencyRateResponse exchangeRate = null;
            if (tmpRates.Rates != null)
            {

                foreach (var de in tmpRates.Rates)
                {
                    exchangeRate = new GetCurrencyRateResponse();
                    exchangeRate.Date = tmpRates.Date;
                    exchangeRate.ExchageRate = de.Value;
                }
            }
            return exchangeRate;
        }


    }
}
