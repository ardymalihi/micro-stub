# micro-stub
Micro Stub is a tiny Micro Servcie which helps you to mock a response coming from a request.

Benefit:
* Integration Test: You can mock expected reponse coming from a micro service.
* Productivity: You start implementing the UI and rely on a consistant data before writing the real service for it
* 

# How it works
Tjere is a file named microstub.json inside the root folder in which you can specifiy the expcected behavour of your requests and responses

{
  "Subscribers": [
    {
      "Key": "CompnayKey",
      "Secret": "CompanySecret",
      "Projects": [
        // Cart Service Project
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
        // Tax Service Project
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

# How to Test?

Base Url is CompanyKey:CompanySecret@Project

As an example:
Based on configuration above:

http://localhost:1479/CompanyKey:CompanySecret@CartService/api/v1/cart/1
returns: {
  cartId: 123,
  total: 12.49,
  items: [{price: 10.29},{price: 2.2}]
}


http://localhost:1479/CompanyKey:CompanySecret@CartService/api/v1/cart/1
returns: { eror: "id should be between 1-1000" }

