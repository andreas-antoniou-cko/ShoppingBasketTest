# Shopping Basket Prototype #

## Requirements ##

The development team has just completed a prototype of a set of services that implement a shopping basket solution, along with a client library that allows you to connect to these services. The deliverable is checked in this repository but does not contain any integration tests. Your task is to create these tests using any framework of your choice. A PowerShell script has been provided which starts all 3 services so feel free to focus writing your tests against these running instances - no need to bootstrap or start them within your tests.  The tests can be part of the same solution as the services, or could be in their own separate repository.

## Quick Start ##

All code in this solution is .NET Core 2.0 compatible.  If you do not have the .NET Core 2.0 framework, you should [download and install](https://www.microsoft.com/net/download/windows) it first.  The project can be opened, edited, and debugged in either VS Code or Visual Studio 2017, but neither of those should be a requirement in running the services.

To start all three of the services, open a PowerShell session and run the `RunAll.ps1` script at the root of the solution.  Each service will begin running in a new console window.  If you are on a platform that does not support PowerShell, you can also start each service using `dotnet run` from a console.

## Acceptance criteria ##

Your tests should cover as many of the scenarios below as you have time for:

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

### Visual Studio 2017

The code should build directly from within Visual Studio (2017) by opening the `ItemsBasket.sln` file at the root. Once you compile the solution, right click on the top (solution) node in **Solution Explorer** and select **Set Startup Projects**. From there click on the **Multiple startup projects** radio button and select **Start** in the dropdowns of the following projects:

* ItemsBasket.AuthenticationService
* ItemsBasket.BasketService
* ItemsBasket.ItemsService

### VS Code

The project can also be opened using [VS Code](https://code.visualstudio.com/download).  Debugging all services can be done from the `All Services` option in the debug window.

### From The Console

The services can also be run using the `dotnet run` command.  Open a console window, navigate to the project folder (e.g. ItemsBasket.AuthenticationService) and type `dotnet run`.  However, this will only start one service at a time.  You can run all the services individually through three different console windows.

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