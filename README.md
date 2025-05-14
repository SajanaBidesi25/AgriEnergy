# AgriEnergy README

## Project Information
- **Project Name:** AgriEnergy
- **Course:** PROG 7311
- **Student:** Sajana Bidesi (ST10249843)

## Links
- **YouTube:** [Watch the Video](https://youtu.be/dhHuiQ4mKWE)
- **GitHub:** [View Repository](https://github.com/SajanaBidesi25/AgriEnergy.git)

## About the Project
AgriEnergy is a feature-rich e-commerce platform developed to support sustainable farming practices. The application facilitates digital empowerment for farmers, enabling them to list environmentally friendly products, participate in discussions, and connect with administrators who oversee the platform.

## Architecture Pattern - Model-View-Controller (MVC)
- **Model:** Manages data, logic, and rules (e.g., `User`, `Product`, `Farmer`, `Employee` classes).
- **View:** User interface layer (Razor views `.cshtml` with HTML and Bootstrap).
- **Controller:** Manages input and data flow (e.g., `ProductController`, `UserController`).

## Design Pattern - Facade
- Simplifies complex systems by providing a unified entry point.
- Abstracts data access, improves code readability, and reduces coupling.

## Database - SQL Server Management Studio (SSMS)
- Database: AgriEnergyDB
- Managed using SSMS for creating, configuring, and maintaining the database.

## Technology Stack
- **Backend:** C# (ASP.NET Core MVC)
- **Frontend:** HTML, CSS, JavaScript, Razor Pages (.cshtml)
- **Database:** SQL Server + SSMS
- **IDE:** Visual Studio 2022

## How to Use the App
1. **Clone the Repository:**
   ```bash
   git clone https://github.com/SajanaBidesi25/AgriEnergy.git
   ```
2. **Open in Visual Studio:**
   - Open the `AgriEnergy.sln` solution file.
3. **Configure Database:**
   - Use SSMS to create `AgriEnergyDB`.
4. **Run the Project:**
   - Build and launch the app in Visual Studio.

## Role-Based Access Control (RBAC)
- **Farmers:** Can list and manage their products.
- **Employees:** Can manage all users and products.

## Connection String
Ensure your `appsettings.json` has the correct connection string:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=lab7L95SR\SQLEXPRESS;Database=AgriEnergyDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

## UI Design
- Green (#006400) and black theme.
- Responsive design using Bootstrap.

## Challenges and Changes
- **RBAC:** Ensured restricted access through thorough testing.
- **UI Consistency:** Maintained a uniform design across views.
- **Database Sync:** Regular updates to match the latest schema.

## How the Application Works
- **Login Page:** User authentication for Farmers and Employees.
- **Home Page:** Accessible by all users.
- **Manage Users (Employee):** View, edit, or delete users.
- **Marketplace (Employee):** View all products by all farmers.
- **Marketplace (Farmer):** Manage own product listings.

## Reference List
- GeeksforGeeks, 2024. [MVC Design Pattern](https://www.geeksforgeeks.org/mvc-design-pattern/).
- Microsoft Learn, 2024. [Add new connections - Visual Studio](https://learn.microsoft.com/en-us/visualstudio/data-tools/add-new-connections).
- Refactoring Guru, 2014. [Facade Pattern](https://refactoring.guru/design-patterns/facade).
- W3Schools, n.d. [CSS Cards](https://www.w3schools.com/howto/howto_css_cards.asp).
