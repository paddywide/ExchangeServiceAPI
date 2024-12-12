using Application.Features.Money.Commands.GetConvertedMoney;

namespace Application.Test
{
    public class CurrencyConvertRequestValidatorTest
    {
        GetExchangeRateCommandValidator validator = new GetExchangeRateCommandValidator();
        readonly GetExchangeRateCommand request_amtNeg = new GetExchangeRateCommand() { Amount = (float)-1, InputCurrency = "AUD", OutputCurrancy = "USD" };
        readonly GetExchangeRateCommand request_InCurEmt = new GetExchangeRateCommand() { Amount = (float)1, InputCurrency = "", OutputCurrancy = "USD" };

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CurrencyConvertRequest_Amount_Is_Negative_Fail()
        {
            var errors = validator.Validate(request_amtNeg);
            Assert.IsFalse(errors.IsValid);
        }

        [Test]
        public void CurrencyConvertRequest_InputCurrency_Is_Empty_Fail()
        {
            var errors = validator.Validate(request_InCurEmt);
            Assert.IsFalse(errors.IsValid);
        }
    }
}