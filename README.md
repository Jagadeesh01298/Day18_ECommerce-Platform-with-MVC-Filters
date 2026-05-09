# E-Commerce MVC Filters Application

This is an ASP.NET Core MVC project created for the **Wipro NGA .NET Daily Coding Assignment - Assignment 1**.

The project demonstrates how to use **advanced MVC filters** for logging, authentication, and global error handling in an e-commerce application.

## Project Objective

The main objective of this project is to build a simple e-commerce MVC application and implement custom filters to handle cross-cutting concerns such as:

- Request and response logging
- User authentication
- Global exception handling
- Dependency injection in filters
- Unit testing of filters

## Features

- View product list
- View product details
- Add new products
- User login and logout
- Place orders
- View user orders
- Custom authentication filter
- Custom logging filter
- Global exception handling filter
- Unit tests using xUnit and Moq

## Technologies Used

- ASP.NET Core MVC
- C#
- Razor Views
- Dependency Injection
- Session Management
- xUnit
- Moq
- Visual Studio

## Filters Implemented

### 1. RequestResponseLoggingFilter

This filter logs request and response details such as:

- HTTP method
- Request URL
- Response status code

Logs are stored in:

```text
Logs/app-log.txt
