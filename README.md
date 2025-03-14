# ProductManagementAPI

## Problem

A client is looking for a backend API to connect their stock managent app to. 

Fundamentally the client wants a table for products that can be ordered as well as the components that make up the product.  Note that some components are also needed in other components.

For example a toy car needs a body and 2 wheels sets. A wheel set needs 2 wheels, a rod and 2 bolts.

The RESTful API needs a get method that returns a list of products available to be made.

Then a method to get a product by id which also returns the list of components needed to build the product. Bonus points the stock system contains the amount of items in stock for each component and the returned result can tell you how much of each component you dont have in stock.

The backend should have 3 layers.

The controller which is responsible for handling incoming request that via an interface communicates to business logic.

The business logic should be injected via dependency injection as a scoped instance and should do the process of data from the database converting the raw database data into the result objects. Then via another interface injected as a scoped instance as well you will have a data access layer.

The data access layer will be a code first ef core ORM. 

All methods in the business logic should be unit tested for positive and negative cases and the controller should be integration tested with the database mocked.

A middleware error handling class should also be created to deal with any throw exceptions gracefully and respond with the correct error response. 

The products should be cached in a memory cache in the business logic to reduce the load on the db with an absolute expiration of 5 min.

Finally annotations should be used to proper document your swagger document when the application is launched.


## TODO:
- Move DB calls from Product service into database layer. 
- Implement available component quantity into models.
- Update get product endpoint to only return products with available components.