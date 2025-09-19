# Finance API

## 📖 Overview

**Finance API** is a secure and robust RESTful API designed for personal finance management. It provides a complete backend solution for tracking income and expenses, with a focus on clean architecture, modern security practices, and clear, maintainable code. This project serves as a practical demonstration of building a modern .NET web application from the ground up.

## ✨ Features

* **User Authentication**: Secure user registration and login system using **JSON Web Tokens (JWT)**. Passwords are never stored in plain text; they are hashed and salted using the industry-standard PBKDF2 algorithm.
* **CRUD Operations**: Full Create, Read, Update, and Delete functionality for both income and expense records.
* **Data Validation**: Utilizes Data Transfer Objects (DTOs) to ensure that all incoming data is properly structured and validated.
* **Secure Endpoints**: All data-related endpoints are protected, requiring a valid JWT for access.
* **API Documentation**: Integrated **Swagger (OpenAPI)** provides interactive and comprehensive API documentation, allowing for easy testing and exploration of the endpoints.
* **"Who Am I" Endpoint**: A utility endpoint (`/api/auth/me`) that allows a client application to verify the identity of the currently authenticated user.

## 🏗️ Architecture

This API is built using a clean, layered architecture that promotes separation of concerns, making the codebase easy to understand, maintain, and extend.

* **Controllers**: The entry point for all HTTP requests. Controllers are responsible for handling incoming requests, validating them, and returning the appropriate HTTP responses. They delegate the core business logic to the service layer.
* **Services**: This layer contains the application's business logic. For example, the `TokenService` is responsible for generating JWTs, while the `PasswordService` handles secure password hashing and verification. This separation ensures that the core logic is independent of the web framework.
* **Data Layer**: Built on **Entity Framework Core**, this layer manages all interactions with the database. It uses separate `DbContext`s for each major entity (`User`, `Income`, `Expense`), promoting a clean and organized data access strategy. For development and demonstration purposes, it is configured to use an in-memory database.
* **Models (Entities & DTOs)**:
    * **Entities**: These are the C# classes that map directly to the database tables.
    * **DTOs (Data Transfer Objects)**: These objects are used to shape the data that is sent to and from the API. Using DTOs prevents under-posting or over-posting and helps to create a clear contract for the API.

## 🛠️ Technologies Used

This project leverages a modern, powerful tech stack to deliver a high-performance and secure API.

* **Framework**: .NET 9 / ASP.NET Core
* **Database**: Entity Framework Core 9 with an In-Memory Provider
* **Authentication**: JSON Web Tokens (JWT)
* **API Documentation**: Swashbuckle / Swagger (OpenAPI)
* **Language**: C#

## 🚀 Getting Started

### Prerequisites

* [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* An IDE like Visual Studio or VS Code

### Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone <your-repo-url>
    ```
2.  **Navigate to the project directory:**
    ```bash
    cd FinanceApi
    ```
3.  **Restore dependencies:**
    ```bash
    dotnet restore
    ```
4.  **Run the application:**
    ```bash
    dotnet run
    ```
5.  **Access the API documentation** by navigating to `https://localhost:<your-port>/swagger` in your web browser.

## Endpoints

All endpoints are available for testing via the Swagger UI.

* **Authentication**
    * `POST /api/auth/register` - Register a new user.
    * `POST /api/auth/login` - Log in and receive a JWT.
    * `GET /api/auth/me` - Get the details of the currently authenticated user.
* **Income & Expenses**
    * Full CRUD operations available at `/api/income` and `/api/expense`.