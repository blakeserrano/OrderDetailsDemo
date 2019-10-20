# OrderDetailsDemo

This application uses LiteDb.
Please update the LiteDbLocation key in the web config with the file path to where you would like the database to be stored.
To test this application and pass the unit tests please submit a post request (http://localhost:63910/api/Order/Post) using the following json data:

{
"customerId": "12345",
"items": [
{
"productId": "8ed0e6f7",
"quantity": 1
},
{
"productId": "c0258525",
"quantity": 3
},
{
"productId": "0a207870",
"quantity": 2
}
]
}

Once you send the post request an order number (guid) should be returned. 
Pass the order number in the get method (localhost:63910/api/Order/Get?id={your guid}) to retrieve your order.
If you have any problems or question please reply to serranoblake@gmail.com Thanks.
