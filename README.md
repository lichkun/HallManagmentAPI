Project Overview

This project provides a RESTful API for managing conference hall bookings and calculating rental costs. The system allows clients to search for available rooms based on their capacity and booking times, make bookings with additional services, and manage room data.
Technical Solutions

Multi-Layer Architecture: The project is structured using a multi-layered approach, ensuring separation of concerns and scalability.
        API Layer: Handles HTTP requests and responses.
        Service Layer: Contains business logic.
        Data Access Layer: Uses Entity Framework Core for database interactions.

Room Availability Logic: Implemented to check existing bookings for overlapping times and return available rooms based on client input.

Price Calculation: Based on predefined time slots, the price calculation logic adjusts the base rental cost according to discounts or surcharges applied during certain hours.

Dockerized Environment: The solution is containerized using Docker to ensure consistent environments for development and production.

How to Deploy
Prerequisites
    Docker installed on your machine.
    .NET 8.0.8 SDK installed.
    
Steps to Deploy:
Clone the repository:
git clone (https://github.com/lichkun/HallManagmentAPI)
cd your-project-folder

Build and run the Docker containers:
docker-compose up --build

Apply the Entity Framework Core migrations: From the root directory, run:
dotnet ef database update -s .\BackendTZ -p .\BackendTZ.DataAccess

The API should now be running at http://localhost:<your-port>.
