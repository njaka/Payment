
## Required
- ASP.NET Core 3.1

## Assumptions:

- Unsuccessful card_number: 4111111111111111

- Only Visa card accepted,  RegexValidation : ^4[0-9]{12}(?:[0-9]{3})?$ (ref https://www.regular-expressions.info/creditcard.html)

- Payment Status: Succeed=1 , Failed=2

## Endpoints
### Process Payment

Method: POST

URI: /api/v1.0/payment

e.g of valid request:

```json
{
  "card": {
    "cardNumber": "4111111111111122",
    "expirationDate": "06/21",
    "cvv": "123"
  },
  "amount": 100,
  "currency": "USD",
  "beneficiaryAlias": "merchant-njaka"
}

```

### Retrieve Payment detail

Method: GET

URI: /api/v1.0/payment/{paymentId}

e.g payment details

```json
{
  "paymentId": "00f15e68-c88a-4074-8f0b-2cc02b3d6a64",
  "card": {
    "cardNumber": "411111XXXXXX1122",
    "expirationDate": "2021-06-30T23:59:59",
    "cvv": "123"
  },
  "amount": 100,
  "currency": "USD",
  "beneficiaryAlias": "merchant-njaka",
  "status": 1,
  "paymentDate": "2020-06-18T00:05:20.0491589+02:00"
}
```
## Containerization

Build image 

```
docker build -t njakaraz/paymentgateway:latest
```

Image pushed to Registry

```
docker push njakaraz/paymentgateway:latest
```

Run

```
docker run -it --rm -p 5053:80 njakaraz/paymentgateway --name paymentgateway
```
Access: http://localhost:5053/index.html

## Monitoring
- HealthCheck : {baseUrl}/health
- Metrics : {baseUrl}/metrics

## Architecture
- Onion Architecture

<p align="center">
<img src="docs/onion-architecture.png" width="400" align="center">
</p>

## Technologies used:

- FluentMediator for handling events (https://github.com/ivanpaulovich/FluentMediator)
- Metrics using prometheus
- InMemory Database
- Native .Net Core DI
- Api documentation with Swagger
