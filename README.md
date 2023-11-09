# `Melody Mine`

## Introduction

Discover and manage a diverse collection of vinyl records across genres in our user-friendly online store with an integrated administrative panel for seamless inventory and order management.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

Before you begin, ensure you have the following installed:
- Git (for cloning the repository)
- .NET SDK (required to build and run the project)

### Installing

Follow these steps to get your development environment set up:

1. **Clone the repository:**
   ```bash
   $ https://github.com/sneeekie/MelodyMine.git
   ```
2. **Build the project:**
   Navigate to the root of the project directory and run:
   ```bash
   MelodyMine/ $ dotnet build
   ```
   This command will compile the project and its dependencies.

3. **Run the application:**
   After building the project, move to the MelodyMine directory and start the application with the following command:
   ```bash
   MelodyMine/MelodyMine/ $ dotnet watch
   ```
    This will start the server and begin watching for any changes in the source code, automatically reloading the server when changes are detected.

## Running the tests

To ensure the quality and functionality of MelodyMine application, we employ a suite of tests written with xUnit. Follow these steps to execute the tests:

### Running xUnit tests

1. **Navigate to the test directory:**
   From the root of your project, change directories to the xUnit test folder:

   ```bash
   MelodyMine/ $ cd xUnitTest
   ```
2. **Execute the tests:**
   Run the following command to execute the tests:
   ```bash
   MelodyMine/xUnitTest/ $ dotnet test
   ```
This command will discover and run all the tests in the project. Output will be provided in the terminal indicating whether the tests passed or failed, along with any other relevant information.

## Built With

* [Entity Framework](https://docs.microsoft.com/en-us/ef/) - The object-relational mapper (ORM) used for data access
* [Npgsql](http://www.npgsql.org/) - A .NET data provider for PostgreSQL used in conjunction with Entity Framework
* [.NET](https://dotnet.microsoft.com/) - The framework used for building the application
* [xUnit](https://xunit.net/) - The testing framework used for running unit tests
* [Git](https://git-scm.com/) - Used for version control

## Contributing

We welcome contributions to the MelodyMine project. For details on how to contribute, please read our [contributing guidelines](docs/CONTRIBUTING.md).

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
