This solution uses clean Architecture, MediatR and fluentValidation. There is an issue of when I setup FluentValidation, and cause it doesn't work. 

Improvement:
  1. The url should be store in the config, not in the source code. 
  2. The calculation should be seperate into Application/Business/Convert.cs, due to there is an issue, I can't complete that.
  3. Currency country code can be store in DB. In validator we can only allow the country code match our DB record.
  4. Cater for more Exception: when errors happen on accessing public API.
  5. Test case should cater for the following as well: 1. request amount is 0. 2. Input and Ouput Currency is not 3 chars and not in (AUD, USD).
  6. Add logging.
  
