# Shopping Basket Prototype #

## Requirements ##

The dev team has just completed developing a prototype implementation of a set of services that implement a shopping basket solution, along with a client library that allows you to connect to these services. The deliverable is checked in this repository but does not contain any unit or integration tests. Your task is to create these tests using any framework of your choice. If you feel at any point that the implementation could be modified to improve testability but without affecting functionality then feel free to do so but make sure you document this.

## Features summary ##

The following assumptions were made for this implementation:

* If a registered customer does not have a basket, this will be created when the first call to the API is made.
* A customer can only have one basket at a time. 
* Since this is a single basket implementation, it is assumed that a shopping cart like functionality is required. This means that only the following operations are supported:
	* Fetch all items currently in the basket.
	* Add a single item to the basket (adding multiple items is rare in most websites)
	* Remove a single item from the basket (removing multiple items is rare in most websites, with the exception of clearing the basket as described below)
	* Clear all the items from the basket.
	* Update one or more items in the basket. In this case:
		* If the item does not exist in the basket it will be added.
		* If the quantity is changed to zero it will be removed.
* Once the user completes the purchase then the Clear all items call can be made to start from scratch.
* The backend store is an in memory dictionary so only a single instance of the services can run as of now.

## Services ##

### Authentication service ###

This service is responsible for 2 things:

* Manage user accounts by allowing anonymous users to add an account as well as modify and delete their accounts.
* Authenticate a user and return a token which is required for the basket API.

The implementation of this service is extremely basic and has no real security around it (plain test password etc). It is more of a stub than anything else but the idea is that the authentication is separated from the basket service. If a user has got a (non-expired) token in his session then there is no need to access this service so if it goes down then the disruption to the system is reduced.

When up and running, the service endpoints are:

* API: 
	* http://localhost:8001/api/Users
	* http://localhost:8001/api/Authentication
* Swagger: http://localhost:8001/swagger/

### Basket Service ###

The main API for dealing with user baskets. The API requires authorization so all calls require the user token. The API infers the user details (mainly the user ID) from the claims included in the token so the API calls are simpler (no need to include user details in it).

When up and running, the service endpoints are:

* API: 
	* http://localhost:8002/api/BasketItems
* Swagger: http://localhost:8002/swagger/

### Items Service ###

A simple API to return a list of items that can be added to the basket. It is used by the basket service to gather more information about the item added and could be used for validation (although this ended up not being the case as it complicated my integration test so decided to disable it).

When up and running, the service endpoints are:

* API: 
	* http://localhost:8003/api/Items
* Swagger: http://localhost:8003/swagger/

## Client Library ##

The client library is an abstraction over the REST API and offers the following features over consuming the REST APIs directly:

* Contains endpoint details so user does not need to supply them (this is a bit basic and could be extended to use service location)
* Crude token session handling.
:exclamation: The implementation not at all ideal for web scenarios as it requires the token manually set each time a call to the basket service is made. The token should be stored in the user's session (which is expected and fine) but should be set each time on the **HttpClientProvider** class to ensure that the correct user token is used.

The end state of this library would be to be packaged in Nuget.

## Debugging and deployment ##

The code should build directly from within Visual Studio (2017) by opening the `ItemsBasket.sln` file at the root. Once you compile the solution, right click on the top (solution) node in **Solution Explorer** and select **Set Startup Projects**. From there click on the **Multiple startup projects** radio button and select **Start** in the dropdowns of the following projects:

* ItemsBasket.AuthenticationService
* ItemsBasket.BasketService
* ItemsBasket.ItemsService