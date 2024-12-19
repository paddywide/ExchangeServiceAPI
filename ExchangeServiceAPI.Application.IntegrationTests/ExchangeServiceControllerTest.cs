using Api.Controllers;
using Application.Features.Money.Commands.GetConvertedMoney;
using Core.Interfaces;
using Core.Models;
using Core.Models.Response;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Moq;
using Newtonsoft.Json;
using System.Net;
using static ExchangeServiceAPI.Application.IntegrationTests.ExchangeServiceControllerTest;

namespace ExchangeServiceAPI.Application.IntegrationTests
{
    public class ExchangeServiceControllerTest
    {
        private Mock<IMediator> _mediator;
        private ExchangeServiceController _controller;
        private GetExchangeRateCommand _command;


        [SetUp]
        public void Setup()
        {        
            _mediator = new Mock<IMediator>();
            _controller = new ExchangeServiceController(_mediator.Object);
            _command = new GetExchangeRateCommand() { Amount = 1, InputCurrency = "AUD", OutputCurrancy = "USD" };

        }

        [Test]
        public async Task MediatRTest1()
        {
            var services = new ServiceCollection();

            var serviceProvider = services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetExchangeRateCommandHandler).Assembly))
                .AddScoped<IExternalVendorRepository, MockExternalVendorRepository>()
                .BuildServiceProvider();
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            // Act
            var response = await mediator.Send(_command);

            Assert.IsNotNull(response);
        }

        [Test]
        public async Task MediatRTest2()
        {
            // Arranged
            MyController myController = new MyController(_mediator.Object);
            _mediator.Setup(x => x.Send(It.IsAny<GetExchangeRateCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CurrencyConvertResponse ());

            // Act
            await myController.Index();

            // Assert
            _mediator.Verify(x => x.Send(It.IsAny<GetExchangeRateCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            //Assert.IsNotNull(response);
        }
        public class MockExternalVendorRepository : IExternalVendorRepository
        {
            public Task<HttpResponseMessage> GetExchangeRate(string inputCurrency)
            {
                ExchangeRate httpResponse = new ExchangeRate() { Base_code = "AUD", Result = "success", Time_last_update_utc = "Mon, 16 Dec 2024 00:00:02 +0000", Conversion_rates = new Conversion_Rates() { USD = 0.6367 } };
                string _requestJs = JsonConvert.SerializeObject(httpResponse);
                HttpResponseMessage ret = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(_requestJs)
                };

                return Task.FromResult<HttpResponseMessage> (ret);
            }
        }

        public class MyController : Controller
        {
            private readonly IMediator _mediator;

            public MyController(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<CurrencyConvertResponse> Index()
            {
                var query = new GetExchangeRateCommand();

                var result = await _mediator.Send(query);

                return await Task.FromResult(result).ConfigureAwait(false);
            }
        }
    }
}