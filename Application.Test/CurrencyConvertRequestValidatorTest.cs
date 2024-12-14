using Application.Features.Money.Commands.GetConvertedMoney;
using Core.Interfaces;
using MediatR;
using Moq;

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
            var mediator = new Mock<IMediator>();
            var externalVendorRepository = new Mock<IExternalVendorRepository>();
            _mockMediator = new Mock<IMediator>();

            GetExchangeRateCommand command = new GetExchangeRateCommand() { Amount = -1, InputCurrency = "AUD", OutputCurrancy = "USD" };
            GetExchangeRateCommandHandler handler = new GetExchangeRateCommandHandler(externalVendorRepository);

            //Act
            Unit x = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            //Do the assertion

            //something like:
            mediator.Verify(x => x.Publish(It.IsAny<CustomersChanged>()));
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