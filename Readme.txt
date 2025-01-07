This solution uses below design and tech.

Architecture and pattern: 
clean Architecture, Result Pattern, Domain event, Aggregate, Primitive and Value Object

.Net Core:
Code first, HttpClient

Nuget:
Exception middleware, MediatR, fluentValidation, Mapper

DB:
SQLite


There is one post method in swagger, if you post the below body 
{
  "amount": 5,
  "inputCurrency": "AUD",
  "outputCurrancy": "USD"
}

it will call http to get the real time exchange rage from public API, base on the rate it will return the converted USD amount. Also it will insert into the DB for logging this request.
