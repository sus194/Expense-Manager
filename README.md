# Expense-Manager

The Expense Manager Web App is a user-friendly application designed for efficient expense management. This README provides information on how to set up, configure, and use the application.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Expense Tracking**: Easily record expenses with details like name, type, amount, and date.
- **Data Visualization**: Visualize your expenses through bar charts, dot charts, and pie charts.
- **Expense Reports**: Generate reports based on yearly, monthly, and weekly expense trends.
- **Expense Limits**: Set limits for different expense types to manage your budget effectively.
- **Secure Deployment**: The application is securely hosted on Microsoft Azure App Services.

## Getting Started

### Prerequisites

Before you begin, ensure you have met the following requirements:

- **.NET Core SDK**: You need the .NET Core SDK installed on your development machine.
- **Azure Account**: To deploy the app on Azure, you should have an Azure account.

### Installation

Follow these steps to set up and run the Expense Manager Web App locally:

1. Clone the repository:

   ```shell
   git clone https://github.com/yourusername/expense-manager-webapp.git
   cd expense-manager-webapp
   dotnet build
   dotnet run
2. Usage:
  1. Register a new user or log in with your existing credentials.
  2. Start by adding your expenses with relevant details.
  3. Explore the various charts and reports to analyze your financial data.
  4. Set expense limits for different categories.
  5. Deploy the app to Azure to make it accessible online.
Technologies Used
- **ASP.NET Core**: The application is built using the ASP.NET Core framework for web development.
- **Entity Framework Core**: Entity Framework Core is used for database access and management.
- **Microsoft Azure**: The app is hosted on Microsoft Azure App Services for seamless deployment and scalability.
- **Chart.js**: The Chart.js library is used for interactive data visualization.
- **Error Handling**: Robust error handling is implemented to provide helpful error messages and prevent crashes.
- **Data Validation**: The app ensures data validation to maintain data integrity and accuracy.
