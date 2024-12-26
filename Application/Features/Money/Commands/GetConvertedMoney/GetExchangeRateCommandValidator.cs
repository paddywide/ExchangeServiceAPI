using ExchangeRate.Donmain.Contract;
using FluentValidation;


namespace Application.Features.Money.Commands.GetConvertedMoney
{
    public class GetExchangeRateCommandValidator : AbstractValidator<GetExchangeRateCommand>
    {
        private readonly ICurrencyCodeRepository _currencyCodeRepository;
        private readonly IQueryHistoryRepository _queryHistoryRepository;

        public GetExchangeRateCommandValidator(ICurrencyCodeRepository currencyCodeRepository, IQueryHistoryRepository queryHistoryRepository)
        {


            this._currencyCodeRepository = currencyCodeRepository;
            this._queryHistoryRepository = queryHistoryRepository;

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is required")
                .Must(x => x > 0).WithMessage("Amount is negative"); ;

            RuleFor(x => x.InputCurrency)
                .NotEmpty().WithMessage("InputCurrency is required")
                .Must(x => x.Length == 3).WithMessage("InputCurrency must be 3 characters long");

            RuleFor(x => x.OutputCurrancy)
                .NotEmpty().WithMessage("OutputCurrancy is required")
                .Must(x => x.Length == 3).WithMessage("OutputCurrancy must be 3 characters long");

            RuleFor(q => q)
                .MustAsync(IsInputCurrencyCodeExisted)
                .WithMessage("Input Currency Code doesn't exists");

            RuleFor(q => q)
                .MustAsync(IsOutputCurrencyCodeExisted)
                .WithMessage("Output Currency Code doesn't exists");

        }

        private Task<bool> IsInputCurrencyCodeExisted(GetExchangeRateCommand command, CancellationToken token)
        {
            return _currencyCodeRepository.IsCurrencyCodeExisted(command.InputCurrency);
        }

        private Task<bool> IsOutputCurrencyCodeExisted(GetExchangeRateCommand command, CancellationToken token)
        {
            return _currencyCodeRepository.IsCurrencyCodeExisted(command.OutputCurrancy);
        }
    }
}
