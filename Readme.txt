This solution uses below design and tech. It has been deployed to AWS EC2.

Architecture and pattern: 
clean Architecture, Result Pattern, Domain event, Aggregate, Primitive and Value Object

.Net Core:
Code first, HttpClient

Security:
JWT

Nuget:
Exception middleware, MediatR, fluentValidation, Mapper

DB:
SQLite

To test on AWS, use Postman, verb is Post, url is http://ec2-3-25-219-219.ap-southeast-2.compute.amazonaws.com/api/ExchangeService
request body is below
{
  "amount": 5,
  "inputCurrency": "AUD",
  "outputCurrancy": "USD"
}
Leave other settings as default. Hit "Send".
It will call http to get the real time exchange rage from public API, base on the rate it will return the converted USD amount. Also it will insert into the DB for logging this request.


In /api/login path, you can pass this request to body to get token
{
  "email": "vistor@localhost.com",
  "password": "vistor"
}
