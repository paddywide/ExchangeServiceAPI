using Application.Exception;
using Application.Features.Money.Commands.GetConvertedMoney;
using Core.Interfaces;
using Core.Models;
using Core.Models.Response;
using MediatR;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Net;

namespace Application.Test
{
    public class CurrencyConvertRequestValidatorTest
    {
        GetExchangeRateCommandValidator _validator;
        GetExchangeRateCommand _request;

        private Mock<IExternalVendorRepository> _externalVendorRepository;
        private GetExchangeRateCommand _command;
        CurrencyConvertResponse _expectedResult;
        string _requestJs;

        public void Setup(string testCase)
        {
            _validator = new GetExchangeRateCommandValidator();
            var mediator = new Mock<IMediator>();
            _externalVendorRepository = new Mock<IExternalVendorRepository>();

            SetupRequestAndResult(testCase);

            var mockMessageHandler = new Mock<HttpMessageHandler>();
            _externalVendorRepository
                .Setup<Task<HttpResponseMessage>>(r => r.GetExchangeRate())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(_requestJs)
                });

            _command = new GetExchangeRateCommand() { Amount = 1, InputCurrency = "AUD", OutputCurrancy = "USD" };
        }

        private void SetupRequestAndResult(string testCase)
        {
            ExchangeRate httpResponse;
            switch (testCase)
            {
                case "Success":
                    _request = new GetExchangeRateCommand() { Amount = (float)1, InputCurrency = "AUD", OutputCurrancy = "USD" };
                    _expectedResult = new CurrencyConvertResponse() { Amount = 1, InputCurrency = "AUD", OutputCurrancy = "USD", value = 0.6367 };
                    httpResponse = new ExchangeRate() { Base_code = "AUD", Result = "success", Time_last_update_utc = "Mon, 16 Dec 2024 00:00:02 +0000", Conversion_rates = new Conversion_Rates() { USD = 0.6367 } };
                    _requestJs = JsonConvert.SerializeObject(httpResponse);
                    break;
                case "Amount_Is_Negative":
                    _request = new GetExchangeRateCommand() { Amount = (float)-1, InputCurrency = "AUD", OutputCurrancy = "USD" };
                    httpResponse = new ExchangeRate() { Base_code = "AUD", Result = "success", Time_last_update_utc = "Mon, 16 Dec 2024 00:00:02 +0000", Conversion_rates = new Conversion_Rates() { USD = 0.6367 } };
                    _requestJs = JsonConvert.SerializeObject(httpResponse);
                    break;

                case "InputCurrency_Is_Empty":
                    _request = new GetExchangeRateCommand() { Amount = (float)1, InputCurrency = "", OutputCurrancy = "USD" };
                    httpResponse = new ExchangeRate() { Base_code = "AUD", Result = "success", Time_last_update_utc = "Mon, 16 Dec 2024 00:00:02 +0000", Conversion_rates = new Conversion_Rates() { USD = 0.6367 } };
                    _requestJs = JsonConvert.SerializeObject(httpResponse);
                    break;

                case "InputCurrency_IsNot_3Chars":
                    _request = new GetExchangeRateCommand() { Amount = (float)1, InputCurrency = "FFFED", OutputCurrancy = "USD" };
                    httpResponse = new ExchangeRate() { Base_code = "AUD", Result = "success", Time_last_update_utc = "Mon, 16 Dec 2024 00:00:02 +0000", Conversion_rates = new Conversion_Rates() { USD = 0.6367 } };
                    _requestJs = JsonConvert.SerializeObject(httpResponse);
                    break;

            }


        }

        [Test]
        public async Task CurrencyConvertRequest_Success()
        {
            Setup("Success");
            //Act
            var handler = new GetExchangeRateCommandHandler(_externalVendorRepository.Object);
            var result = await handler.Handle(_command, CancellationToken.None);

            //Assert
            Assert.That(JsonConvert.SerializeObject(_expectedResult), Is.EqualTo(JsonConvert.SerializeObject(result)));
        }

        [Test]
        public async Task CurrencyConvertRequest_Amount_Is_Negative_Fail()
        {
            Setup("Amount_Is_Negative");
            //Act
            var errors = _validator.Validate(_request);

            //Assert
            Assert.IsFalse(errors.IsValid);
            Assert.AreEqual("Amount is negative", errors.Errors.First().ErrorMessage);

        }
        [Test]
        public async Task CurrencyConvertRequest_InputCurrency_Is_Empty_Fail()
        {
            Setup("InputCurrency_Is_Empty");
            //Act
            var errors = _validator.Validate(_request);

            //Assert
            Assert.IsFalse(errors.IsValid);
            Assert.AreEqual("InputCurrency is required", errors.Errors.First().ErrorMessage);

        }
        [Test]
        public async Task CurrencyConvertRequest_InputCurrency_IsNot_3Chars_Fail()
        {
            Setup("InputCurrency_IsNot_3Chars");
            //Act
            var errors = _validator.Validate(_request);

            //Assert
            Assert.IsFalse(errors.IsValid);
            Assert.AreEqual("InputCurrency must be 3 characters long", errors.Errors.First().ErrorMessage);

        }


    }

}