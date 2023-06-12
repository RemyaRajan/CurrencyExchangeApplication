namespace CurrencyExchange.APIService.DataAccess
{
    public class Constants
    {
        public const string ApiKey = "apikey";
        public const string ApiKeyValue = "e927b1ca0943a2ce389cbfbf3e822ba5";
        public const string baseURI = "http://data.fixer.io/api/";
        public const string ExternalApiLatestRateRoute = "latest?access_key=e927b1ca0943a2ce389cbfbf3e822ba5&symbols={0}&base={1}";
        public const string ExternalApiGivenDateRateRoute = "{0}?access_key=e927b1ca0943a2ce389cbfbf3e822ba5&symbols={1}&base={2}";
    }
}
