using Api.Controllers;
using Application.Features.Money.Commands.GetConvertedMoney;
using AutoMapper;
using Azure.Core;
using Core.Interfaces;
using Core.Models;
using Core.Models.Response;
using ExchangeRate.Application.Interfaces;
using ExchangeRate.Domain.GetConvertedMondy;
using ExchangeRate.Domain.Models;
using ExchangeRate.Domain.Primitive.Result;
using ExchangeRate.Donmain.Contract;
using ExchangeRate.Persistence.Repositories;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using static ExchangeServiceAPI.Application.IntegrationTests.ExchangeServiceControllerTest;

namespace ExchangeServiceAPI.Application.IntegrationTests
{
    //MediatR: How to Quickly Test Your Handlers with Unit Tests
    //https://goatreview.com/mediatr-quickly-test-handlers-with-unit-tests/#:~:text=To%20build%20an%20effective%20unit,and%20not%20for%20a%20class.
    public class ExchangeServiceControllerTest
    {
        private Mock<IMediator> _mediator;
        private ExchangeServiceController _controller;
        private GetExchangeRateCommand _command;
        private Mock<IExternalVendorRepository> _externalVendorRepository;
        private Mock<IQueryHistoryRepository> _queryHistoryRepository;
        private Mock<ICurrencyCodeRepository> _currencyCodeRepository;
        private CurrencyConvertResponse _expectedResult;
        private GetExchangeRateCommand _request;
        private Mock<IMapper> _mapper;
        private Mock<IHistoryService> _historyService;
        private CurrencyConvertResponse _expect;


        [SetUp]
        public void Setup()
        {        
            _mediator = new Mock<IMediator>();
            _controller = new ExchangeServiceController(_mediator.Object);
            _externalVendorRepository = new Mock<IExternalVendorRepository>();
            _queryHistoryRepository = new Mock<IQueryHistoryRepository>();
            _currencyCodeRepository = new Mock<ICurrencyCodeRepository>();
            _historyService = new Mock<IHistoryService>();
            _mapper = new Mock<IMapper>();
            _request = new GetExchangeRateCommand() { Amount = (float)1, InputCurrency = "AUD", OutputCurrancy = "USD" };
            _currencyCodeRepository
                .Setup<Task<bool>>(r => r.IsCurrencyCodeExisted("AUD"))
                .ReturnsAsync(true);
            _currencyCodeRepository
                .Setup<Task<bool>>(r => r.IsCurrencyCodeExisted("USD"))
                .ReturnsAsync(true);

            var mockExchangeRateData = ResultT<ExchangeRateData>.Success(new ExchangeRateData
            {
                Base_code = "AUD", 
                Result = "success", 
                Time_last_update_utc = "Mon, 16 Dec 2024 00:00:02 +0000", 
                Conversion_rates = new Conversion_Rates() 
                { 
                    USD = 0.6367 
                } 
            });
            _externalVendorRepository
                .Setup<Task<ResultT<ExchangeRateData>>>(r => r.GetExchangeRate(_request.InputCurrency))
                .ReturnsAsync(mockExchangeRateData);

            CalculatedAmount ca = new CalculatedAmount
            {
                Amount = (float)1,
                InputCurrency = "AUD",
                OutputCurrancy = "USD",
                value = (double)1,
                Rate = (double)1,
            };
            QueryHistory qh = new QueryHistory
            {
                Id = 1,
                InputCurrency = "AUD",
                OutputCurrancy = "USD",
                DateQueried = DateTime.Now,
                Rate = (double)0.6367,
            };
            _expect = new CurrencyConvertResponse
            {
                Amount = 1,
                InputCurrency = "AUD",
                OutputCurrancy = "USD",
                value = (double)0.6367,
            };

            _mapper.Setup(r => r.Map<CalculatedAmount>(It.IsAny<GetExchangeRateCommand>()))
            .Returns(ca);

            _mapper.Setup(r => r.Map<QueryHistory>(It.IsAny<CalculatedAmount>()))
            .Returns(qh);

            _mapper.Setup(r => r.Map<CurrencyConvertResponse>(It.IsAny<CalculatedAmount>()))
            .Returns(_expect);
        }

        [Test]
        public async Task IntegrationTest1()
        {
            // Arranged
            _currencyCodeRepository
                .Setup<Task<bool>>(r => r.IsCurrencyCodeExisted("AUD"))
                .ReturnsAsync(true);

            GetExchangeRateCommandHandler handler = new GetExchangeRateCommandHandler(_externalVendorRepository.Object
                , _queryHistoryRepository.Object, _mapper.Object, _currencyCodeRepository.Object, _historyService.Object);

            // Act
            var result = await handler.Handle(_request, new CancellationToken());

            //Assert
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(_expect, result.Value);
        }


        public async Task MediatRTest1()
        {
            var services = new ServiceCollection();

            var serviceProvider = services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetExchangeRateCommandHandler).Assembly))
                .AddScoped<IExternalVendorRepository, MockExternalVendorRepository>()
                .AddScoped<IExternalVendorRepository, MockExternalVendorRepository>()
                .BuildServiceProvider();
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            //Expected invocation on the mock once, but was 0 times: x => x.Send<ResultT<CurrencyConvertResponse>>(It.IsAny<GetExchangeRateCommand>(), It.IsAny<CancellationToken>())

            // Act
            var response = await mediator.Send(_command);

            Assert.IsNotNull(response);
        }


        public async Task MediatRTest2()
        {
            // Arranged
            MyController myController = new MyController(_mediator.Object);

            var mockResponse = ResultT<CurrencyConvertResponse>.Success(new CurrencyConvertResponse
            {
                // Populate the properties of CurrencyConvertResponse as needed
            });

            _mediator.Setup(x => x.Send(It.IsAny<GetExchangeRateCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockResponse);

            // Act
            //await myController.Index();

            // Assert
            _mediator.Verify(x => x.Send<ResultT<CurrencyConvertResponse>>(It.IsAny<GetExchangeRateCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            //Assert.IsNotNull(response);
        }
        public class MockExternalVendorRepository : IExternalVendorRepository
        {
            public Task<ResultT<ExchangeRateData>> GetExchangeRate(string inputCurrency)
            {
                ExchangeRateData httpResponse = new ExchangeRateData() { Base_code = "AUD", Result = "success", Time_last_update_utc = "Mon, 16 Dec 2024 00:00:02 +0000", Conversion_rates = new Conversion_Rates() { USD = 0.6367 } };
                //string _requestJs = JsonConvert.SerializeObject(httpResponse);
                //HttpResponseMessage ret = new HttpResponseMessage()
                //{
                //    StatusCode = HttpStatusCode.OK,
                //    Content = new StringContent(_requestJs)
                //};

                return Task.FromResult<ResultT<ExchangeRateData>> (httpResponse);
            }
        }

        public class MyController : Controller
        {
            private readonly IMediator _mediator;

            public MyController(IMediator mediator)
            {
                _mediator = mediator;
            }

            //below is for when we don't use Result pattern
            //public async Task<CurrencyConvertResponse> Index()
            //{
            //    var query = new GetExchangeRateCommand();

            //    var result = await _mediator.Send(query);

            //    return await Task.FromResult(result).ConfigureAwait(false);
            //}
            //public async Task<CurrencyConvertResponse> Index()
            //{
            //    var query = new GetExchangeRateCommand();

            //    var result = await _mediator.Send(query);

            //    return await Task.FromResult(result).ConfigureAwait(false);
            //}
        }
    }
}