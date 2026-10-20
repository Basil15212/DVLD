# 🚗 Driving & Vehicle License Department (DVLD)

<p align="center">
  <strong>A Desktop Management System built with C#, Windows Forms, SQL Server, and a 3-Layer Architecture.</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Language-C%23-68217A?style=for-the-badge&logo=csharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/Framework-.NET%20Framework%204.7.2-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET Framework" />
  <img src="https://img.shields.io/badge/UI-Windows%20Forms-0078D4?style=for-the-badge&logo=windows&logoColor=white" alt="Windows Forms" />
  <img src="https://img.shields.io/badge/Database-SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
</p>

---

## 📌 Overview

**DVLD** is a desktop application developed to manage the main operations of a Driving and Vehicle License Department.

The system covers people and user management, driving license applications, test appointments, license issuance, renewal, replacement, and other license-related operations.

This project is also an opportunity to apply **Object-Oriented Programming (OOP)**, **C#**, **ADO.NET**, and **layered software architecture** in a practical Windows Forms application.

## ✨ Features

### 👥 People & User Management
- Manage people and their personal information.
- Manage system users and their associated people records.
- User login and account status handling.
- Search, filter, and display records through Windows Forms interfaces.

### 📝 Application Management
- Manage application types and their fees.
- Create and manage local driving license applications.
- Track applications through the licensing workflow.

### 🧪 Tests & Appointments
- Manage test types, including Vision, Written, and Street tests.
- Schedule test appointments.
- Manage test-related records as part of the licensing process.

### 🪪 Driving License Operations
- Manage drivers and issued licenses.
- Issue a license for a first-time local driving license application.
- Renew existing licenses.
- Replace lost or damaged licenses.
- Manage international driving licenses.
- Manage detained licenses, including detention and release operations.
- View license history.

## 🏗️ Architecture

The solution is organized into three main layers to separate responsibilities and make the code easier to maintain.

```text
┌───────────────────────────────────────┐
│          Presentation Layer            │
│             DVLD                       │
│       Windows Forms UI                 │
└───────────────────┬───────────────────┘
                    │
                    ▼
┌───────────────────────────────────────┐
│           Business Layer               │
│         DVLDBussnessLayer               │
│    Business Rules & Application Logic  │
└───────────────────┬───────────────────┘
                    │
                    ▼
┌───────────────────────────────────────┐
│             Data Layer                 │
│          DLVDData_Access                │
│       SQL Server & ADO.NET              │
└───────────────────┬───────────────────┘
                    │
                    ▼
             SQL Server Database
```

### Layer Responsibilities

| Layer | Responsibility |
|---|---|
| **Presentation Layer** | Displays forms and controls, collects user input, and presents results. |
| **Business Layer** | Implements business operations and coordinates between the UI and data access. |
| **Data Access Layer** | Executes database operations and retrieves or updates stored information. |

This separation helps reduce direct dependencies between the user interface and database operations.

For more details, see **[Architecture Documentation](Architecture-Documentation.md)**.

## 🧰 Technologies & Concepts

### Technologies
- **C#**
- **.NET Framework 4.7.2**
- **Windows Forms (WinForms)**
- **Microsoft SQL Server**
- **ADO.NET**

### Programming Concepts
- Object-Oriented Programming (OOP)
- Encapsulation, Inheritance, and Polymorphism
- Classes, Objects, and Interfaces
- Separation of Concerns
- 3-Layer Architecture
- Events and Delegates
- Data validation and error handling
- Database queries and relational data access

## 📂 Solution Structure

The solution contains three main projects:

```text
DVLD_WithoutUC.sln
│
├── DVLD
│   └── Presentation Layer
│
├── DVLDBussnessLayer
│   └── Business Layer
│
└── DLVDData_Access
    └── Data Access Layer
```

> The project names above match the names used in the solution.

## ⚙️ Getting Started

### Prerequisites

Before running the application, make sure you have:

- Windows.
- Visual Studio with .NET desktop development support.
- .NET Framework 4.7.2.
- Microsoft SQL Server.
- A configured database and a valid connection string.

### Setup

1. Clone or download this repository.
2. Open `DVLD_WithoutUC.sln` in Visual Studio.
3. Configure the database connection string to match your SQL Server environment.
4. Ensure that the required database and tables are available.
5. Build the solution.
6. Run the `DVLD` presentation project.

**Note:** Database setup may require additional configuration depending on your environment.

## 📚 Documentation

- **[Architecture Documentation](Architecture-Documentation.md)** — explains the layers, their responsibilities, and how they work together.

## 🎯 Project Goals

This project focuses on applying programming concepts to a practical desktop application, including:

- Building a multi-layered C# application.
- Separating presentation, business logic, and data access.
- Working with SQL Server through ADO.NET.
- Applying OOP principles to real application workflows.
- Improving code organization, readability, and maintainability.

## 🚧 Project Status

This project is part of my ongoing C# and Windows Forms development journey. Its features and implementation may continue to evolve as I learn and improve the design.

## 👨‍💻 Author

**Basil Ali**

- GitHub: [@Basil15212](https://github.com/Basil15212)

---

<p align="center">
  <sub>Built with C# • Windows Forms • SQL Server</sub>
</p>
