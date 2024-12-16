using Application.Features.Money.Commands.GetConvertedMoney;
using Application.Test.Mocks;
using Core.Interfaces;
using Core.Models;
using Core.Models.Response;
using MediatR;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Net;

namespace Application.Test
{
    public class CurrencyConvertRequestValidatorTest
    {
        GetExchangeRateCommandValidator validator = new GetExchangeRateCommandValidator();
        readonly GetExchangeRateCommand request_InCurEmt = new GetExchangeRateCommand() { Amount = (float)1, InputCurrency = "", OutputCurrancy = "USD" };

        private Mock<IExternalVendorRepository> _externalVendorRepository;
        private GetExchangeRateCommand _command;
        CurrencyConvertResponse _expectedResult;

        public void Setup()
        {
            var mediator = new Mock<IMediator>();
            _externalVendorRepository = new Mock<IExternalVendorRepository>();

            ExchangeRate httpResponse = new ExchangeRate() { Base_code = "AUD", Result = "success", Time_last_update_utc = "Mon, 16 Dec 2024 00:00:02 +0000", Conversion_rates = new Conversion_Rates() { USD = 0.6367 } };
            _expectedResult = new CurrencyConvertResponse() { Amount = 1, InputCurrency = "AUD", OutputCurrancy = "USD", value = 0.6367 };
            var js = JsonConvert.SerializeObject(httpResponse);

            var mockMessageHandler = new Mock<HttpMessageHandler>();
            _externalVendorRepository
                .Setup<Task<HttpResponseMessage>>(r => r.GetExchangeRate())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(js)
                });

            _command = new GetExchangeRateCommand() { Amount = 1, InputCurrency = "AUD", OutputCurrancy = "USD" };
        }

        [Test]
        public async Task CurrencyConvertRequest_Amount_Is_Negative_Fail()
        {
            Setup();
            //Act
            var handler = new GetExchangeRateCommandHandler(_externalVendorRepository.Object);
            var result = await handler.Handle(_command, CancellationToken.None);

            //Assert
            Assert.That(JsonConvert.SerializeObject(_expectedResult), Is.EqualTo(JsonConvert.SerializeObject(result)));
        }

        [Test]
        public void CurrencyConvertRequest_InputCurrency_Is_Empty_Fail()
        {
            var errors = validator.Validate(request_InCurEmt);
            Assert.IsFalse(errors.IsValid);
        }


    }

}