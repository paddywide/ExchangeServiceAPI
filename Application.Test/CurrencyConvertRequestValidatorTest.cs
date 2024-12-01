using Application.Validators;
using Core.Models.Request;

namespace Application.Test
{
    public class CurrencyConvertRequestValidatorTest
    {
        CurrencyConvertRequestValidator validator = new CurrencyConvertRequestValidator();
        readonly CurrencyConvertRequest request_amtNeg = new CurrencyConvertRequest() { Amount = (float)-1, InputCurrency = "AUD", OutputCurrancy = "USD" };
        readonly CurrencyConvertRequest request_InCurEmt = new CurrencyConvertRequest() { Amount = (float)1, InputCurrency = "", OutputCurrancy = "USD" };

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