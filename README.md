# Auction Manager
This project goal is to implement the "Car Auction Management System"

## Technology
- Microsoft .NET 8
- Swagger to test API endpoints
- No Database used (in memory objects)

## Setup
There are no dependencies, no Docker containers, just clone the repository and run the application.
For testing, the application adds 5 vehicles (ids 100-104), which can be queried by the /vehicle/serch endpoint

## Architecture diagram
![image](https://github.com/rapontual/auctionmanager/assets/8179423/a9097cc0-3dfa-48c2-82f2-9112f364e098)

- API: used an API with the swagger to interact with the application
  No security was added
- Domain: basic domain model. Due to development time, I used the same model for the API. I usually separate the models and create separate models to meet different needs.
- Service: It aggregates the application logic and separates the implementation from the interface
- Repository: Isolates the implementation of persistence, although it is using data in memory, it can easily implement connection to a database

This is like a simple 3 layers model. 

## Models
Basically, 


![image](https://github.com/rapontual/auctionmanager/assets/8179423/0d13f4d1-50cf-4a70-8a08-2308592447f5)







