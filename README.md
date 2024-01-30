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
![image](https://github.com/rapontual/auctionmanager/assets/8179423/0d13f4d1-50cf-4a70-8a08-2308592447f5)

## Notes
To simplify the challenge test, I decided to use an API and some endpoints as a user interface. With Swagger it is possible to test the application without additional tools, such as Postman for example

I didn't add tests to the API to speed up development. As all the logic is concentrated in the Service layer, I added tests covering this logic

I used the same internal model to be exposed in the API. I always separate the models, in this case I shared the same model to simplify development

I decided to use a repository layer because I wasn't sure if there would be time to implement a database. As I preferred the "code first" approach, it would be easy to add a database to this layer later, isolating its logic

I tried to focus on the requirements, logic and tests, as these were the most important points I noticed in the challenge description







