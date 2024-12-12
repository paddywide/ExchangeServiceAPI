using Core.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Money.Commands.GetConvertedMoney
{
    public class GetExchangeRateCommand : IRequest<CurrencyConvertResponse>
    {
        public float Amount { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrancy { get; set; }
    }

}
