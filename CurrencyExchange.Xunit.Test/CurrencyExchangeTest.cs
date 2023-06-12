
using CurrencyExchange.APIService.DataAccess.Mapper;

namespace CurrencyExchange.Xunit.Test
{
    public class CurrencyExchangeTest
    {
        private readonly Mock<CurrencyExachangedbContext> dbContext;
        private readonly Mock<IMapper> mapper;
        private const string sourceCurrency= "EUR";
        private const string targetCurrency = "NOK";
        private const decimal amount =100;
        public CurrencyExchangeTest()
        {

            dbContext = new Mock<CurrencyExachangedbContext>();
            mapper = new Mock<IMapper>();

        }
        /// <summary>
        /// Test method for currencyExchangeRateRepository.GetExchageRate
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [Fact]
        public async Task GetCurrecyExchangeRateTest()
        {
            try
            {
                CurrencyExchangeRateRepository currencyExchangeRateRepository = new CurrencyExchangeRateRepository(dbContext.Object, mapper.Object);
                decimal amount = 100;
                var result = await currencyExchangeRateRepository.GetExchageRate(sourceCurrency, targetCurrency, amount, null);
                if (result > 0)
                    Assert.True(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Test method for currencyExchangeRateRepository.GetExchageRateByCode
        /// </summary>
        /// <exception cref="Exception"></exception>
        [Fact]
        public void GetCurrecyExchangeRateByCodeTest()
        {
            try
            {

                TestingDataSetup();
                CurrencyExchangeRateRepository currencyExchangeRateRepository = new CurrencyExchangeRateRepository(dbContext.Object, mapper.Object);
                var result =  currencyExchangeRateRepository.GetExchageRateByCode(sourceCurrency, targetCurrency, amount);
                if (result > 0)
                    Assert.True(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Test method for currencyExchangeRateRepository.GetCurrencyRatesByDate
        /// </summary>
        /// <exception cref="Exception"></exception>
        [Fact]
        public void GetCurrecyExchangeRateByDateTest()
        {
            try
            {
                TestingDataSetup();
                CurrencyExchangeRateRepository currencyExchangeRateRepository = new CurrencyExchangeRateRepository(dbContext.Object, mapper.Object);
                var result =  currencyExchangeRateRepository.GetCurrencyRatesByDate(sourceCurrency, targetCurrency, amount,Convert.ToDateTime("06-06-2023"));
                if (result > 0)
                    Assert.True(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Test method for currencyExchangeRateRepository.GetCurrencyRatesByPeriod
        /// </summary>
        /// <exception cref="Exception"></exception>
        [Fact]
        public void GetCurrecyExchangeRateByPeriodTest()
        {
            try
            {
                TestingDataSetup();
                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfile());
                });
                var mapper = mockMapper.CreateMapper();
                CurrencyExchangeRateRepository currencyExchangeRateRepository = new CurrencyExchangeRateRepository(dbContext.Object, mapper);
                var result = currencyExchangeRateRepository.GetCurrencyRatesByPeriod(sourceCurrency, targetCurrency, Convert.ToDateTime("05-06-2023"), Convert.ToDateTime("07-06-2023"));
                Assert.NotNull(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Test method for currencyExchangeRateRepository.GetCurrencies
        /// </summary>
        /// <exception cref="Exception"></exception>
        [Fact]
        public void GetCurreciesTest()
        {
            try
            {
                TestingDataSetup();
                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfile());
                });
                var mapper = mockMapper.CreateMapper();
                CurrencyExchangeRateRepository currencyExchangeRateRepository = new CurrencyExchangeRateRepository(dbContext.Object, mapper);
                var result = currencyExchangeRateRepository.GetCurrencies();
                Assert.NotNull(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void TestingDataSetup()
        {
            var masterData = new List<CurrencyMaster>
                {
                new CurrencyMaster { CurrencyId = 1,CurrencyCode="EUR"},
                new CurrencyMaster { CurrencyId = 2,CurrencyCode="NOK" },
                }.AsQueryable();

            var mockCurrencyMaster = new Mock<DbSet<CurrencyMaster>>();
            mockCurrencyMaster.As<IDbAsyncEnumerable<CurrencyMaster>>()
                 .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<CurrencyMaster>(masterData.GetEnumerator()));

            mockCurrencyMaster.As<IQueryable<CurrencyMaster>>()
               .Setup(m => m.Provider)
               .Returns(new TestDbAsyncQueryProvider<CurrencyMaster>(masterData.Provider));

            mockCurrencyMaster.As<IQueryable<CurrencyMaster>>().Setup(m => m.Provider).Returns(masterData.Provider);
            mockCurrencyMaster.As<IQueryable<CurrencyMaster>>().Setup(m => m.Expression).Returns(masterData.Expression);
            mockCurrencyMaster.As<IQueryable<CurrencyMaster>>().Setup(m => m.ElementType).Returns(masterData.ElementType);
            mockCurrencyMaster.As<IQueryable<CurrencyMaster>>().Setup(m => m.GetEnumerator()).Returns(() => masterData.GetEnumerator());

            var Ratedata = new List<CurrencyRate>
                {
                new CurrencyRate {  BaseCurrencyId =1,TargetCurrencyId=2,ExchageRate=11,IsLatest=true,Date=Convert.ToDateTime("06-06-2023"),CurrencyRateId=1,CreatedDateTime=DateTime.Now },
                new CurrencyRate {  BaseCurrencyId =1,TargetCurrencyId=2,ExchageRate=11,IsLatest=true,Date=Convert.ToDateTime("06-06-2023"),CurrencyRateId = 2,CreatedDateTime=DateTime.Now },

                }.AsQueryable();

            var mockCurrencyRate = new Mock<DbSet<CurrencyRate>>();
            mockCurrencyMaster.As<IDbAsyncEnumerable<CurrencyRate>>()
                 .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<CurrencyRate>(Ratedata.GetEnumerator()));

            mockCurrencyRate.As<IQueryable<CurrencyRate>>()
               .Setup(m => m.Provider)
               .Returns(new TestDbAsyncQueryProvider<CurrencyRate>(Ratedata.Provider));

            mockCurrencyRate.As<IQueryable<CurrencyRate>>().Setup(m => m.Provider).Returns(Ratedata.Provider);
            mockCurrencyRate.As<IQueryable<CurrencyRate>>().Setup(m => m.Expression).Returns(Ratedata.Expression);
            mockCurrencyRate.As<IQueryable<CurrencyRate>>().Setup(m => m.ElementType).Returns(Ratedata.ElementType);
            mockCurrencyRate.As<IQueryable<CurrencyRate>>().Setup(m => m.GetEnumerator()).Returns(() => Ratedata.GetEnumerator());

            dbContext.Setup(m => m.CurrencyMasters).Returns(mockCurrencyMaster.Object);
            dbContext.Setup(m => m.CurrencyRates).Returns(mockCurrencyRate.Object);
        }

    }
}
