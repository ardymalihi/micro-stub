# micro-stub

Micro Stub is a tiny Micro Service which helps you to mock a response coming from a request.

Benefit:

* Integration Test: You can mock expected response coming from a micro service.
* Productivity: You start implementing the UI and rely on a consistent data before writing the real service for it

# How it works

There is a file named microstub.json inside the root folder in which you can specify the expected behaviour of your requests and responses

```json
{
  "Subscribers": [
    {
      "Key": "CompnayKey",
      "Secret": "CompanySecret",
      "Projects": [
        {
          "Name": "CartService",
          "EndPoints": [
            {
              "Address": "/api/v1/cart/1",
              "Methods": [
                {
                  "HttpMethodName": "GET",
                  "QueryString": null,
                  "RequestBody": null,
                  "ContentType": "application/json",
                  "Response": "{ \"cartId\": 123, \"total\": 12.49, \"items\": [ { \"price\": 10.29 }, { \"price\": 2.20} ] }",
                  "HttpStatusCode": 200
                }
              ]
            },
            {
              "Address": "/api/v1/cart/2000000",
              "Methods": [
                {
                  "HttpMethodName": "GET",
                  "QueryString": null,
                  "RequestBody": null,
                  "ContentType": "application/json",
                  "Response": "{ \"eror\": \"id should be between 1-1000\" }",
                  "HttpStatusCode": 400
                }
              ]
            },
            {
              "Address": "/api/v1/carts",
              "Methods": [
                {
                  "HttpMethodName": "POST",
                  "QueryString": null,
                  "RequestBody": "{ memberId: 1, orderId: 1234 }",
                  "ContentType": "application/json",
                  "Response": "{ \"updated\": true }",
                  "HttpStatusCode": 200
                },
                {
                  "HttpMethodName": "POST",
                  "QueryString": null,
                  "RequestBody": "{ memberId: 2, orderId: 1234 }",
                  "ContentType": "application/json",
                  "Response": "{ \"eror\": \"invalid order id\" }",
                  "HttpStatusCode": 400
                }
              ]
            }
          ]
        },
        {
          "Name": "TaxService",
          "EndPoints": [
            {
              "Address": "/api/v1/taxes/1",
              "Methods": [
                {
                  "HttpMethodName": "GET",
                  "QueryString": null,
                  "RequestBody": null,
                  "ContentType": "application/json",
                  "Response": "{ \"gst\": 1.23, \"pst\": 7.6 }",
                  "HttpStatusCode": 200
                }
              ]
            }
          ]
        }
      ]
    }
  ]
}
```

# How to Test?

Base Url is CompanyKey:CompanySecret@Project

As an example:
Based on configuration above: (Note: Key and Secret are case sensitive)

http://localhost:1479/CompanyKey:CompanySecret@CartService/api/v1/cart/1

```json
{
  "cartId": 123,
  "total": 12.49,
  "items": [
    { "price": 10.29 },
    { "price": 12.25 }
  ]
}
```

http://localhost:1479/CompanyKey:CompanySecret@CartService/api/v1/cart/1
 
```json
{ 
  "eror": "id should be between 1-1000" 
}
```

