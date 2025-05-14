# AgriEnergy

**A Sustainable Farming E-Commerce Platform**

---

## Table of Contents

1. [About the Project](#about-the-project)  
2. [Architecture Pattern](#architecture-pattern)  
3. [Database](#database)  
4. [Design Pattern](#design-pattern)  
5. [How to Use the App](#how-to-use-the-app)  
6. [How the App Works](#how-the-app-works)  
7. [Technology Stack](#technology-stack)  
8. [UI Design](#ui-design)  
9. [Dependencies](#dependencies)  
10. [Challenges and Changes](#challenges-and-changes)  
11. [Connection String](#connection-string)  
12. [GitHub Repository](#github-repository)  
13. [YouTube Demo](#youtube-demo)

---

## About the Project

AgriEnergy is a sustainable farming e-commerce web application designed to promote green energy products and empower local farmers. The platform allows farmers to add products related to sustainable agriculture, while employees (administrators) oversee system management, including user registration and full access to all product data.

The system is built using ASP.NET Core MVC, offering a clean separation of concerns and scalability.

---

## Architecture Pattern

### Model-View-Controller (MVC)

MVC is a software architectural pattern that divides the application into three main interconnected components:

- **Model**: Manages the data and business logic. In AgriEnergy, this includes models like `User`, `Farmer`, `Employee`, and `Product`. These represent the core entities used throughout the app.
- **View**: The presentation layer. These are the `.cshtml` Razor pages that render the user interface. Views are dynamically updated based on the model and controller.
- **Controller**: Acts as an intermediary between model and view. It handles user input, communicates with models, and returns the appropriate views.

![1_y8Z4MgBS_s8d4o26arDJ4w](https://github.com/user-attachments/assets/279786d4-8a5f-4c2f-ad0a-993fd19a0b60)


Benefits of using MVC include:
- Improved code maintenance and scalability.
- Facilitates team collaboration (front-end and back-end).
- Enhances unit testing due to clear separation of responsibilities.

---

## Database

### SQL Server Management Studio (SSMS)

**SQL Server Management Studio (SSMS)** is a robust IDE for managing Microsoft SQL Server databases. It was used to:
- Create the `AgriEnergyDB` database schema.
- Insert seed data, including an initial employee for testing.
- Maintain referential integrity with foreign keys (e.g., Products linked to Farmers).
![images](https://github.com/user-attachments/assets/7a9c3d52-6942-4a4c-9dbb-6d9b8ee0faea)

SSMS simplifies query execution, schema design, and data visualization.

---

## Design Pattern

### Facade Pattern

The **Facade design pattern** is used to provide a unified interface to a set of interfaces in a subsystem. In AgriEnergy, it simplifies complex interactions with the data layer by offering a single access point for controllers.

![facade_pattern_uml_diagram](https://github.com/user-attachments/assets/5eae93d0-ac14-43c0-8698-157049d0b2de)

This pattern helps:
- Reduce coupling between components.
- Make the system easier to use and test.
- Enhance readability and abstraction for the data access layer.

A service layer or helper class may serve as the facade, especially when retrieving or filtering product data.

---

## How to Use the App

### Prerequisites
- Visual Studio 2022 or later.
- SQL Server Express or LocalDB.
- .NET 9.0 SDK.

### Steps to Run the Project

1. **Clone the Repository**
   ```bash
   git clone https://github.com/SajanaBidesi25/AgriEnergy.git
   ```

2. **Open the Solution in Visual Studio**
   - Launch Visual Studio.
   - Open `AgriEnergy.sln`.

3. **Set Up the Database**
   - Ensure your SQL Server instance is running.
   - Use SSMS to restore the database or allow EF Core to create it on the first run.
   - Update the `appsettings.json` connection string if needed.

4. **Build and Run the Project**
   - Click "Build > Build Solution" or press `Ctrl + Shift + B`.
   - Press `F5` or click the green "Start" button.

### Default Employee Credentials (for testing)
- Username: `admin@agrienergy.com`
- Password: `Admin123!`

---

## How the App Works

### Role-Based Functionality

#### Farmer:
- Can log in to view and manage their own product listings.
- Can create new green energy product entries.
- Sees only their own data.

#### Employee:
- Can register new farmers.
- Can view and manage all product listings.
- Can filter products by category and date range.

This role separation ensures data privacy and proper access control.

---

## Technology Stack

- **Frontend**: Razor, Bootstrap, HTML5, CSS3
- **Backend**: ASP.NET Core MVC (C#)
- **Database**: Microsoft SQL Server
- **IDE**: Visual Studio 2022
- **ORM**: Entity Framework Core

---

## UI Design

- Uses a green and black theme to reinforce environmental values.

  ![images](https://github.com/user-attachments/assets/b9b0b951-21a3-40c6-8767-5b6041f516e6)

- Pages like the Product Index feature image-based backgrounds to reflect agricultural relevance.
- Cards and tables have been styled for consistent sizing, readability, and responsiveness.

---

## Dependencies

Key NuGet packages include:
- `Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation`
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`
- `Microsoft.VisualStudio.Web.CodeGeneration.Design`

---

## Challenges and Changes

- **Role Management**: RBAC implementation required thorough testing to ensure access was limited appropriately.
- **UI Consistency**: Designing uniform cards across views to display user and product information clearly.
- **Database Sync**: Ensuring the local database reflected the latest schema during development.

---

## Connection String

```
Server=lab7L95SR\SQLEXPRESS;Database=AgriEnergyDB;Trusted_Connection=True;TrustServerCertificate=True;
```

This must be placed in `appsettings.json` under the `ConnectionStrings` section.

---

## GitHub Repository

[AgriEnergy GitHub Repo](https://github.com/SajanaBidesi25/AgriEnergy.git)

---

## YouTube Demo

[Youtube Video Walkthrough](https://youtu.be/dhHuiQ4mKWE)


