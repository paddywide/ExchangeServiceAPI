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
        public async Task Test1()
        {
            var services = new ServiceCollection();
            //var serviceProvider = services.AddMediatR(cfg =>
            //{
            //    cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            //}).BuildServiceProvider();




            var serviceProvider = services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetExchangeRateCommandHandler).Assembly))
                .AddScoped<IExternalVendorRepository, MockExternalVendorRepository>()
                .BuildServiceProvider();
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            // Act
            var response = await mediator.Send(_command);

            Assert.IsNotNull(response);
            //var fixture = new Fixture();
            //// Arranged
            //Setup();
            //_ = _mediator.Setup(x => x.Send(request: _command))
            //    .Returns(new Task<CurrencyConvertResponse>());

            //// Act
            //var returns = await _mediator.Send(timeRegisterCommandRequest);

            //// Assert
            //_mediator.Verify(x => x.Send(It.IsAny<GetExchangeRateCommand>()), Times.Once);
        }

        public class MockExternalVendorRepository : IExternalVendorRepository
        {
            public Task<HttpResponseMessage> GetExchangeRate()
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
    }
}