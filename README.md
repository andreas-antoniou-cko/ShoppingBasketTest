# Shopping Basket Prototype #

## Requirements ##

The development team has just completed a prototype of a set of services that implement a shopping basket solution, along with a client library that allows you to connect to these services. The deliverable is checked in this repository but does not contain any unit or integration tests. Your task is to create these tests using any framework of your choice. If you feel at any point that the implementation could be modified to improve testability but without affecting functionality then feel free to do so but make sure you document this.

## Acceptance criteria ##

Your tests must cover all of the scenerios below:

* You should be able to get a list of all existing users, create new ones, update and delete existing ones.
	* When creating a new user the username has to be unique and the password valid (longer than 4 characters).
	* When updating or deleting a user that does not exist then an error should be thrown.
* Only registered/authorized users should be able to perform any shopping basket actions.
* If a registered customer does not have a basket, this will be created when the first call to the API is made.
* A customer can only have one basket at a time. 
* Since this is a single basket implementation, it is assumed that a shopping cart like functionality is required. This means that only the following operations are supported:
	* Fetch all items currently in the basket.
	* Add a single item to the basket 
	* Remove a single item from the basket 
	* Clear all the items from the basket.
	* Update one or more items in the basket. In this case:
		* If the item does not exist in the basket it will be added.
		* If the quantity is changed to zero it will be removed.
* Once the user completes the purchase then the Clear all items call can be made to start from scratch.
* Any user should be able to retrieve a list of all items that can be added to a basket without requiring authorization.

## Debugging and deployment ##

The code should build directly from within Visual Studio (2017) by opening the `ItemsBasket.sln` file at the root. Once you compile the solution, right click on the top (solution) node in **Solution Explorer** and select **Set Startup Projects**. From there click on the **Multiple startup projects** radio button and select **Start** in the dropdowns of the following projects:

* ItemsBasket.AuthenticationService
* ItemsBasket.BasketService
* ItemsBasket.ItemsService

## Services ##

### Authentication service ###

This service is responsible for 2 things:

* Manage user accounts by allowing anonymous users to add an account as well as modify and delete their accounts.
* Authenticate a user and return a token which is required for the basket API.

The implementation of this service is extremely basic and has no real security around it (plain test password etc).

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

A simple API to return a list of items that can be added to the basket. It is used by the basket service to gather more information about the item added.

When up and running, the service endpoints are:

* API: 
	* http://localhost:8003/api/Items
* Swagger: http://localhost:8003/swagger/



