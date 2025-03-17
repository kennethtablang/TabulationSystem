# Tabulation System

![C#](https://upload.wikimedia.org/wikipedia/commons/4/4f/Csharp_Logo.png) ![.NET](https://upload.wikimedia.org/wikipedia/commons/7/7d/Microsoft_.NET_logo.png) ![MVC](https://upload.wikimedia.org/wikipedia/commons/a/ab/ASP.NET_logo.png) ![CSS](https://upload.wikimedia.org/wikipedia/commons/6/62/CSS3_logo.svg) ![Bootstrap](https://upload.wikimedia.org/wikipedia/commons/b/b2/Bootstrap_logo.svg) ![Inspinia](https://your-inspinia-logo-url.com)

# Tabulation System

## Overview
The **Tabulation System** is a web application developed using **ASP.NET Core MVC**. This system is designed to facilitate the management and computation of scores for various events, competitions, or any structured scoring system. It provides an efficient and user-friendly interface for judges, organizers, and participants.

## Features
- **User Authentication & Authorization**  
  - Implemented with ASP.NET Core Identity  
  - Role-based access control (Admin, Manager, and End User)
- **Event Management**  
  - Create, update, and manage events
- **Judge Management**  
  - Assign judges to events
- **Scoring System**  
  - Judges can submit scores for participants
  - Real-time calculation and tabulation of scores
- **Reports & Analytics**  
  - Generate detailed reports
  - View analytics on scores and rankings

## Technologies Used
- **ASP.NET Core MVC** - Framework for building web applications
- **Entity Framework Core** - Database management
- **Fluent API** - Defining Relationship between Entities
- **SQL Server** - Database
- **ASP.NET Core Identity** - User authentication and role management
- **Bootstrap** - UI styling
- **Inspinia** - Template

## Installation
```sh
# Clone the repository
git clone https://github.com/kennethtablang/TabulationSystem.git

# Navigate to the project folder
cd tabulationsystem

# Restore dependencies
dotnet restore

# Apply migrations to the database
Update-Database

# Run the application
dotnet run
```

## Usage
- **Admin** can manage users, events, and scoring criteria.
- **Judges** can log in and submit scores.
- **Manager** can view results.

## Contributing
Contributions are welcome! To contribute:
```sh
# Fork the repository
# Create a new branch
# Make changes and commit
# Submit a pull request
```

## License
This project is licensed under the MIT License.

## Contact
For inquiries or support, contact kennethreytablang@gmail.com(mailto:kennethreytablang.com).
