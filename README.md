# 🚗 Driving & Vehicle License Department (DVLD)

> A desktop application for managing driving license and vehicle-related services, built with **C# WinForms**, **SQL Server**, and a **3-Layer Architecture**.




\

---

## 📌 About The Project

**DVLD (Driving & Vehicle License Department)** is a Windows desktop application designed to manage different operations related to people, users, applications, tests, and driving licenses.

The project is currently **under development**. I am building it step by step while focusing on applying **Object-Oriented Programming, clean architecture, database design, and real-world WinForms development**.

The goal is to build a complete system that can manage the different stages of the driving license process.

---

## 🛠️ Technologies & Tools

* **C#**
* **.NET Framework**
* **Windows Forms**
* **SQL Server**
* **ADO.NET**
* **Visual Studio**
* **Object-Oriented Programming (OOP)**
* **3-Layer Architecture**

---

## 🏗️ Project Architecture

The application is organized into separate layers to keep the user interface, business logic, and database operations independent.

```text
┌─────────────────────────────┐
│       Presentation Layer    │
│          WinForms           │
│                             │
│  Forms + UserControls       │
└──────────────┬──────────────┘
               │
               ▼
┌─────────────────────────────┐
│       Business Layer        │
│                             │
│  Business Objects           │
│  Validation                 │
│  Application Logic          │
└──────────────┬──────────────┘
               │
               ▼
┌─────────────────────────────┐
│       Data Access Layer     │
│                             │
│  ADO.NET                    │
│  SQL Queries                │
│  Database Communication     │
└──────────────┬──────────────┘
               │
               ▼
┌─────────────────────────────┐
│         SQL Server          │
│           Database          │
└─────────────────────────────┘
```

### Layers

#### 🖥️ Presentation Layer

Contains the WinForms interface, forms, and reusable UserControls.

Examples:

* People Management Forms
* User Management Forms
* Login Form
* Main Screen
* Application Management
* Test Type Management
* UserControls for displaying and filtering people/users

#### ⚙️ Business Layer

Contains the main business objects and application logic.

Examples:

* `clsPerson`
* `clsCountry`
* `clsUser`
* `clsApplication`
* `clsApplicationType`
* `clsLocalDrivingLicenseApplication`
* `clsTestType`

The Business Layer communicates with the Data Access Layer instead of directly executing SQL from the UI.

#### 🗄️ Data Access Layer

Responsible for communicating with SQL Server using **ADO.NET**.

Examples:

* `clsPersonData`
* `clsCountryData`
* `clsUserData`
* `clsApplicationData`
* `clsApplicationTypeData`
* `clsLocalDrivingLicenseApplicationData`
* `clsTestTypeData`

---

# ✅ Current Features

The project is still being developed, but several important modules have already been implemented.

### 👤 People Management

* Add new person
* Update person
* Find person
* List people
* Display person information
* Country/nationality management
* Person image handling
* Person filtering
* Reusable Person UserControls

### 🔐 User Management

* Add users
* Update users
* Find users
* List users
* Activate/deactivate users
* Username/password authentication
* Login system
* Change password
* Remember Me functionality
* Link users with existing people

### 📝 Application Types

* List application types
* Edit application type
* Update application fees
* Manage application types

### 🧪 Test Types

* Manage test types
* Edit test information
* Test descriptions
* Test fees

Current test types include:

* Vision Test
* Written Test
* Street Test

### 🚘 Local Driving License Applications

The initial structure for local driving license applications has been implemented.

This part is **still under development** and will be expanded as more license-related functionality is added.

---

# 🧠 Concepts I'm Practicing

This project is also a learning project where I am applying C# and software development concepts in a real application.

### Object-Oriented Programming

* Classes & Objects
* Encapsulation
* Constructors
* Inheritance
* Constructor Inheritance
* Upcasting & Downcasting
* Method Overriding
* Method Hiding
* Abstract Classes
* Interfaces
* Multiple Interfaces
* Composition
* Sealed Classes
* Nested Classes

### WinForms

* Forms
* UserControls
* DataGridView
* ListView
* ComboBox filtering
* ErrorProvider
* OpenFileDialog
* ContextMenuStrip
* Events
* Delegates
* Form-to-Form communication

### Database & ADO.NET

* SQL Server
* Relational database concepts
* CRUD operations
* SQL constraints
* Primary & Foreign Keys
* Joins
* Views
* DQL
* Subqueries
* `SqlConnection`
* `SqlCommand`
* `SqlDataReader`
* `DataTable`
* `SCOPE_IDENTITY()`

---

# 📂 Project Structure

```text
DVLD
│
├── DVLD
│   ├── People
│   │   ├── Controls
│   │   ├── frmAddUpdatePerson
│   │   ├── frmFindPerson
│   │   ├── frmListPeople
│   │   └── frmShowPersonInfo
│   │
│   ├── Users
│   │   ├── Controls
│   │   ├── frmAddUpdateUser
│   │   ├── frmChangePassword
│   │   ├── frmListUsers
│   │   └── frmUserInfo
│   │
│   ├── Applications
│   │   ├── Application Types
│   │   └── Local Driving License Application
│   │
│   ├── Test Types
│   │
│   ├── Login
│   └── MainScreen
│
├── DVLDBussnessLayer
│   ├── People & Countries
│   ├── Users
│   ├── Applications
│   └── Test Types
│
└── DLVDData_Access
    ├── People & Countries
    ├── Users
    ├── Applications
    ├── Local Driving License
    └── Test Types
```

---

# 🚧 Current Development

This project is **not finished yet**.

I am currently continuing development of the driving license management functionality and gradually adding more modules to the system.

### Planned / Upcoming Features

* [ ] License Classes Management
* [ ] Driving License Management
* [ ] Tests Management
* [ ] Test Appointments
* [ ] Local Driving License workflow
* [ ] International Driving License
* [ ] License Renewal
* [ ] License Replacement
* [ ] License Detention / Release
* [ ] Application cancellation
* [ ] More advanced search and filtering
* [ ] Reports
* [ ] Additional validation
* [ ] Further improvements to the architecture and UI

> This list will be updated as the project evolves.

---

# 🎯 Project Goals

The main goals of this project are:

1. Build a complete real-world desktop application.
2. Apply OOP concepts in a practical project.
3. Understand and implement 3-layer architecture.
4. Practice SQL Server and ADO.NET.
5. Improve WinForms development skills.
6. Learn how different application modules communicate with each other.
7. Write more maintainable and organized C# code.
8. Continue improving the architecture as the project grows.

---

# 📈 Development Status

| Module                             | Status         |
| ---------------------------------- | -------------- |
| Project Architecture               | 🟢 Implemented |
| People Management                  | 🟢 Implemented |
| Users Management                   | 🟢 Implemented |
| Login System                       | 🟢 Implemented |
| Application Types                  | 🟢 Implemented |
| Test Types                         | 🟢 Implemented |
| Local Driving License Applications | 🟡 In Progress |
| License Classes                    | 🔴 Not Started |
| Tests & Appointments               | 🔴 Not Started |
| Driving Licenses                   | 🔴 Not Started |
| Reports                            | 🔴 Not Started |

---

# 📚 Learning Approach

This project is being developed incrementally rather than as one large finished application.

Each module is used as an opportunity to learn and apply new concepts in:

**C# → OOP → WinForms → SQL Server → ADO.NET → Architecture → Real-world application development**

The project will continue to evolve as new concepts and requirements are implemented.

---

# 🚀 Getting Started

### Requirements

* Visual Studio
* .NET Framework
* SQL Server
* SQL Server Management Studio (SSMS)

### Run the project

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Configure the SQL Server connection string.
4. Make sure the required database is available.
5. Build the solution.
6. Run the `DVLD` project.

---

# 👨‍💻 Author

**Basil Ali**

This project is part of my journey in learning **C#, OOP, WinForms, SQL Server, ADO.NET, and software architecture**.

---

⭐ If you find this project interesting, feel free to explore the code and follow its development as more features are added.
