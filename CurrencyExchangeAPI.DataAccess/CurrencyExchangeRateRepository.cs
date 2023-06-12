namespace CurrencyExchangeAPI.DataAccess;
public class CurrencyExchangeRateRepository : ICurrencyExchangeRateRepository
{
    private readonly CurrencyExachangedbContext _currencyContext;
    private readonly IMapper _mapper;

    public CurrencyExchangeRateRepository(CurrencyExachangedbContext currencyContext, IMapper mapper)
    {
        _currencyContext = currencyContext;
        _mapper = mapper;
    }
    /// <summary>
    /// Method to get Exchange Rate from Fixer API
    /// </summary>
    /// <param name="baseCurrencyCode"></param>
    /// <param name="targetCurrencyCode"></param>
    /// <param name="amount"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    public async Task<decimal> GetExchageRate(string baseCurrencyCode, string targetCurrencyCode, decimal amount, DateTime? date)
    {
        HttpClient client = new HttpClient();
        string baseURI = Constants.baseURI;

        var rates = string.Empty;
        string requestParam = date == null ? string.Format(Constants.ExternalApiLatestRateRoute, targetCurrencyCode, baseCurrencyCode)
               : string.Format(Constants.ExternalApiGivenDateRateRoute, date.Value.ToString("yyyy-MM-dd"), targetCurrencyCode, baseCurrencyCode);

        rates = await client.GetStringAsync(baseURI + requestParam);

        var currencyRate = (GetCurrencyRateResponse)JsonConvert.DeserializeObject<ExchangeRates>(rates);
        decimal convertedAmount = 0;
        if (currencyRate != null)
        {
            convertedAmount = Math.Round(currencyRate.ExchageRate * amount, 2);
        }
        return convertedAmount;
    }

    /// <summary>
    /// Method to get exchage rate from db by params basecode,targetcode and amount
    /// </summary>
    /// <param name="baseCode"></param>
    /// <param name="targetCode"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public decimal GetExchageRateByCode(string baseCode, string targetCode, decimal amount)
    {
        var currencyRate = from cr in _currencyContext.CurrencyRates
                           join cmb in _currencyContext.CurrencyMasters on cr.BaseCurrencyId equals cmb.CurrencyId
                           join cmt in _currencyContext.CurrencyMasters on cr.TargetCurrencyId equals cmt.CurrencyId
                           where cmb.CurrencyCode == baseCode && cmt.CurrencyCode == targetCode && cr.IsLatest == true
                           select cr;
        decimal convertedAmount = 0;
        if (currencyRate != null)
        {
            convertedAmount = Math.Round(currencyRate.First().ExchageRate * amount,2);
        }
        return convertedAmount;

    }

    /// <summary>
    /// Method to get exchage rate from db by params basecode,targetcode,amount and date
    /// </summary>
    /// <param name="baseCode"></param>
    /// <param name="targetCode"></param>
    /// <param name="amount"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    public decimal GetCurrencyRatesByDate(string baseCode, string targetCode,decimal amount, DateTime date)
    {
        var currencyRates = from cr in _currencyContext.CurrencyRates
                            join cmb in _currencyContext.CurrencyMasters on cr.BaseCurrencyId equals cmb.CurrencyId
                            join cmt in _currencyContext.CurrencyMasters on cr.TargetCurrencyId equals cmt.CurrencyId
                            orderby cr.Date ascending
                            where cmb.CurrencyCode == baseCode && cmt.CurrencyCode == targetCode &&
                            (cr.Date == date)
                            select cr;
        decimal convertedAmount = 0;
        if (currencyRates != null)
        {
            convertedAmount = Math.Round(currencyRates.First().ExchageRate * amount,2);
        }
        return convertedAmount;
    }

    /// <summary>
    /// Method to get exchange rates for a time period
    /// </summary>
    /// <param name="baseCode"></param>
    /// <param name="targetCode"></param>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <returns></returns>
    public List<GetCurrencyRateResponse> GetCurrencyRatesByPeriod(string baseCode, string targetCode, DateTime fromDate, DateTime toDate)
    {
        var currencyRates = from cr in _currencyContext.CurrencyRates
                            join cmb in _currencyContext.CurrencyMasters on cr.BaseCurrencyId equals cmb.CurrencyId
                            join cmt in _currencyContext.CurrencyMasters on cr.TargetCurrencyId equals cmt.CurrencyId
                            orderby cr.Date ascending
                            where cmb.CurrencyCode == baseCode && cmt.CurrencyCode == targetCode &&
                            (cr.Date >= fromDate && cr.Date <= toDate)
                            select cr;
        List<GetCurrencyRateResponse> CurrenyRateResponse = new List<GetCurrencyRateResponse>();
        var currencyRateDTO = _mapper.Map(currencyRates, CurrenyRateResponse);
        return currencyRateDTO;

    }
    /// <summary>
    /// Method to get all currencies
    /// </summary>
    /// <returns></returns>
    public async Task<List<GetCurrenciesResponse>> GetCurrencies()
    {
        var currencies = await _currencyContext.CurrencyMasters.ToListAsync();
        List<GetCurrenciesResponse> CurrenicesResponse = new List<GetCurrenciesResponse>();
        var currenciesDTO = _mapper.Map(currencies, CurrenicesResponse);
        return currenciesDTO;
    }
}
