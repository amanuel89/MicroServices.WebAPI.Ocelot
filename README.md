# Getnet Project

This repository contains a .NET Core Web API microservice implementing a Clean Architecture. This README provides an overview of the project, coding standards, variable naming rules, and best practices for maintaining a clean and maintainable codebase.

## Table of Contents

1. [Introduction](#introduction)
2. [Prerequisites](#prerequisites)
3. [Installation](#installation)
4. [Usage](#usage)
5. [Folder Structure](#folder-structure)
6. [Coding Standards](#coding-standards)
7. [Variable Naming Rules](#variable-naming-rules)
8. [Best Practices](#best-practices)
9. [Contributing](#contributing)
10. [License](#license)

## Introduction

This microservice is built using .NET Core Web API and follows the principles of Clean Architecture to ensure scalability, maintainability, and testability. 

## Prerequisites

- .NET Core SDK
- Docker (optional, for containerization)
- Visual Studio or Visual Studio Code

## Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/your-repo.git
   ```
2. Navigate to the project directory:
   ```sh
   cd your-repo
   ```
3. Restore dependencies:
   ```sh
   dotnet restore
   ```

## Usage

1. Build the project:
   ```sh
   dotnet build
   ```
2. Run the application:
   ```sh
   dotnet run
   ```

## Folder Structure

The folder structure is organized to support Clean Architecture principles:

```
src
├── Api
│   ├── Controllers
│   ├── Models
│   └── Startup.cs
├── Application
│   ├── Interfaces
│   ├── Services
│   └── DTOs
├── Domain
│   ├── Entities
│   ├── Interfaces
│   └── ValueObjects
├── Infrastructure
│   ├── Data
│   ├── Repositories
│   └── Services
├── Tests
│   ├── UnitTests
│   └── IntegrationTests
```

- **Api**: Contains the Web API controllers and models.
- **Application**: Contains business logic, service interfaces, and Data Transfer Objects (DTOs).
- **Domain**: Contains core entities, interfaces, and value objects.
- **Infrastructure**: Contains data access implementations, external service implementations, and infrastructure-specific services.
- **Tests**: Contains unit and integration tests.

## Coding Standards

1. **General Guidelines**
   - Follow the .NET coding conventions as specified in the [Microsoft .NET documentation](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions).
   - Use meaningful and descriptive names.
   - Keep methods small and focused on a single task.
   - Write XML documentation for public members.

2. **File Organization**
   - One class per file.
   - File names should match the class name.

3. **Indentation**
   - Use 4 spaces for indentation.

## Variable Naming Rules

1. **General Naming Rules**
   - Use `camelCase` for local variables and parameters.
   - Use `PascalCase` for public members, methods, properties, and classes.
   - Use `ALL_CAPS` for constants.

2. **Specific Conventions**
   - Prefix boolean variables with `is`, `has`, `can`, etc.
     ```csharp
     bool isAvailable;
     ```
   - Use meaningful names that convey the purpose of the variable.
     ```csharp
     int customerCount;
     ```

## Best Practices

1. **Error Handling**
   - Use exception handling to manage unexpected errors.
   - Avoid using exceptions for control flow.

2. **Logging**
   - Use a logging framework (e.g., Serilog, NLog) to log information, warnings, and errors.

3. **Dependency Injection**
   - Leverage the built-in dependency injection in .NET Core for better testability and maintainability.

4. **Asynchronous Programming**
   - Use asynchronous programming (async/await) for I/O-bound operations to improve scalability.

5. **Unit Testing**
   - Write unit tests for all business logic.
   - Aim for high code coverage without sacrificing code quality.

## Contributing

Contributions are welcome! Please follow the guidelines below to contribute:

1. Fork the repository.
2. Create a feature branch (`git checkout -b feature/your-feature`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Create a new Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

Feel free to customize this template to better fit your project's needs.
