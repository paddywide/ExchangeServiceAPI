using Application.Features.Money.Commands.GetConvertedMoney;
using Core.Interfaces;
using Core.Models.Response;
using ExchangeRate.Domain.Models;
using ExchangeRate.Donmain.Contract;
using MediatR;
using Moq;
using Newtonsoft.Json;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Test
{
    public class CurrencyConvertRequestValidatorTest
    {
        GetExchangeRateCommandValidator _validator;
        GetExchangeRateCommand _request;

        private Mock<IExternalVendorRepository> _externalVendorRepository;
        private Mock<IQueryHistoryRepository> _queryHistoryRepository;
        private Mock<ICurrencyCodeRepository> _currencyCodeRepository;
        private GetExchangeRateCommand _command;
        CurrencyConvertResponse _expectedResult;
        string _requestJs;

        public void Setup(string testCase)
        {
            var mediator = new Mock<IMediator>();
            _externalVendorRepository = new Mock<IExternalVendorRepository>();
            _queryHistoryRepository = new Mock<IQueryHistoryRepository>();
            _currencyCodeRepository = new Mock<ICurrencyCodeRepository>();
            _validator = new GetExchangeRateCommandValidator(_currencyCodeRepository.Object);

            SetupRequestAndResult(testCase);

            _command = new GetExchangeRateCommand() { Amount = 1, InputCurrency = "AUD", OutputCurrancy = "USD" };
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            _currencyCodeRepository
                .Setup<Task<bool>>(r => r.IsCurrencyCodeExisted("AUD"))
                .ReturnsAsync(true);
            _currencyCodeRepository
                .Setup<Task<bool>>(r => r.IsCurrencyCodeExisted("USD"))
                .ReturnsAsync(true);
            _currencyCodeRepository
                .Setup<Task<bool>>(r => r.IsCurrencyCodeExisted("JPY"))
                .ReturnsAsync(false);

        }

        private void SetupRequestAndResult(string testCase)
        {
            ExchangeRateData httpResponse;
            switch (testCase)
            {
                case "Success":
                    _request = new GetExchangeRateCommand() { Amount = (float)1, InputCurrency = "AUD", OutputCurrancy = "USD" };
                    _expectedResult = new CurrencyConvertResponse() { Amount = 1, InputCurrency = "AUD", OutputCurrancy = "USD", value = 0.6367 };
                    httpResponse = new ExchangeRateData() { Base_code = "AUD", Result = "success", Time_last_update_utc = "Mon, 16 Dec 2024 00:00:02 +0000", Conversion_rates = new Conversion_Rates() { USD = 0.6367 } };
                    _requestJs = JsonConvert.SerializeObject(httpResponse);
                    break;

                case "Amount_Is_Negative":
                    _request = new GetExchangeRateCommand() { Amount = (float)-1, InputCurrency = "AUD", OutputCurrancy = "USD" };
                    httpResponse = new ExchangeRateData() { Base_code = "AUD", Result = "success", Time_last_update_utc = "Mon, 16 Dec 2024 00:00:02 +0000", Conversion_rates = new Conversion_Rates() { USD = 0.6367 } };
                    _requestJs = JsonConvert.SerializeObject(httpResponse);
                    break;

                case "InputCurrency_Is_Empty":
                    _request = new GetExchangeRateCommand() { Amount = (float)1, InputCurrency = "", OutputCurrancy = "USD" };
                    httpResponse = new ExchangeRateData() { Base_code = "AUD", Result = "success", Time_last_update_utc = "Mon, 16 Dec 2024 00:00:02 +0000", Conversion_rates = new Conversion_Rates() { USD = 0.6367 } };
                    _requestJs = JsonConvert.SerializeObject(httpResponse);
                    break;

                case "InputCurrency_IsNot_3Chars":
                    _request = new GetExchangeRateCommand() { Amount = (float)1, InputCurrency = "FFFED", OutputCurrancy = "USD" };
                    httpResponse = new ExchangeRateData() { Base_code = "AUD", Result = "success", Time_last_update_utc = "Mon, 16 Dec 2024 00:00:02 +0000", Conversion_rates = new Conversion_Rates() { USD = 0.6367 } };
                    _requestJs = JsonConvert.SerializeObject(httpResponse);
                    break;

                case "InCurrency_IsNotIn_DB":
                    _request = new GetExchangeRateCommand() { Amount = (float)1, InputCurrency = "JPY", OutputCurrancy = "USD" };
                    _expectedResult = new CurrencyConvertResponse() { Amount = 1, InputCurrency = "RMB", OutputCurrancy = "USD", value = 0.6367 };
                    httpResponse = new ExchangeRateData() { Base_code = "AUD", Result = "success", Time_last_update_utc = "Mon, 16 Dec 2024 00:00:02 +0000", Conversion_rates = new Conversion_Rates() { USD = 0.6367 } };
                    _requestJs = JsonConvert.SerializeObject(httpResponse);
                    break;

            }


        }

        [Test]
        public async Task CurrencyConvertRequest_Success()
        {
            Setup("Success");
            //Act
            var errors = await _validator.ValidateAsync(_request);

            //var handler = new GetExchangeRateCommandHandler(_externalVendorRepository.Object, _queryHistoryRepository.Object);
            //var result = await handler.Handle(_command, CancellationToken.None);

            //Assert
            Assert.IsTrue(errors.IsValid);
            //Assert.That(JsonConvert.SerializeObject(_expectedResult), Is.EqualTo(JsonConvert.SerializeObject(result)));
        }

        [Test]
        public async Task CurrencyConvertRequest_Amount_Is_Negative_Fail()
        {
            Setup("Amount_Is_Negative");
            //Act
            var errors = await _validator.ValidateAsync(_request);

            //Assert
            Assert.IsFalse(errors.IsValid);
            Assert.AreEqual("Amount is negative", errors.Errors.First().ErrorMessage);

        }
        [Test]
        public async Task CurrencyConvertRequest_InputCurrency_Is_Empty_Fail()
        {
            Setup("InputCurrency_Is_Empty");
            //Act
            var errors = await _validator.ValidateAsync(_request);

            //Assert
            Assert.IsFalse(errors.IsValid);
            Assert.AreEqual("InputCurrency is required", errors.Errors.First().ErrorMessage);

        }
        [Test]
        public async Task CurrencyConvertRequest_InputCurrency_IsNot_3Chars_Fail()
        {
            Setup("InputCurrency_IsNot_3Chars");
            //Act
            var errors = await _validator.ValidateAsync(_request);

            //Assert
            Assert.IsFalse(errors.IsValid);
            Assert.AreEqual("InputCurrency must be 3 characters long", errors.Errors.First().ErrorMessage);

        }
        [Test]
        public async Task CurrencyConvertRequest_InCurrency_IsNotIn_DB_Fail()
        {
            Setup("InCurrency_IsNotIn_DB");
            //Act
            var errors = await _validator.ValidateAsync(_request);

            //Assert
            Assert.IsFalse(errors.IsValid);
            Assert.AreEqual("Input Currency Code doesn't exists", errors.Errors.First().ErrorMessage);

        }


    }

}