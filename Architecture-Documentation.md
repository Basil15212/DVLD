# DVLD Architecture Documentation

This document explains how the DVLD solution is organized, how its layers communicate, why responsibilities are separated, and what that separation improves. The examples refer to classes and project names found in this repository.

## 1. Solution Map

The solution contains three principal projects:

```text
DVLD_WithoutUC.sln
│
├── DVLD                         Presentation Layer
│   ├── People/
│   ├── Users/
│   ├── Aplications/
│   ├── Tests/
│   ├── Drivers/
│   ├── Licenses/
│   ├── LogIn/
│   └── MainScreen/
│
├── DVLDBussnessLayer             Business Layer
│   ├── People&Countries/
│   ├── User/
│   ├── Applocations/
│   ├── Drivers/
│   ├── Licenses/
│   ├── License Classes/
│   ├── Test_For_License/
│   └── Detained/
│
└── DLVDData_Access               Data Access Layer
    ├── People&Countries/
    ├── Users/
    ├── Applications/
    ├── Drivers/
    ├── Licenses/
    ├── License Classes/
    ├── Tests_ForLicense/
    └── Detained/
```

The names above preserve the spellings used by the project. The WinForms project references the Business Layer, and the Business Layer references the Data Access Layer. The intended dependency direction is therefore:

**Presentation → Business → Data Access → SQL Server**

This is a layered architecture: higher layers call lower layers for the responsibilities they need. Lower layers should not need to know about the forms above them.

## 2. Why Separate the Layers?

Imagine a single form that handles a button click, validates every business rule, builds SQL text, opens a connection, reads database rows, maps them to objects, and updates the screen. It might work for a small feature, but each new feature would make that form harder to understand and change.

Separating layers assigns different reasons for change to different parts of the system:

- A change to the screen layout should mainly affect the Presentation Layer.
- A change to a business rule should mainly affect the Business Layer.
- A change to a query or database mapping should mainly affect the Data Access Layer.

This does not make changes completely independent in every situation. A database schema change can affect queries and possibly business mappings, for example. The goal is to limit unnecessary ripple effects, not to promise that no change will ever cross a boundary.

## 3. Responsibility of Each Layer

### 3.1 Presentation Layer — `DVLD`

**Primary responsibility:** interact with the user and present information.

The project contains WinForms such as `frmLogin`, `frmListPeople`, `frmAddUpdatePerson`, `frmListUsers`, `frmScheduleTest`, and license/application forms. It also contains reusable UserControls such as `ctrlPersonCard`, `ctrlPersonCardWithFilter`, and controls for license/application information.

Typical responsibilities:
- Read values entered by the user.
- Respond to events such as button clicks and selected rows.
- Perform UI-specific checks, such as whether a required textbox is empty or whether a selection has been made.
- Call a Business Layer method to perform an operation.
- Show the result, refresh a list, or display an error/message.
- Coordinate navigation between forms and UserControls.

What it should generally avoid:
- Building SQL queries or opening SQL connections.
- Duplicating domain rules across several forms.
- Being the only place where an important business rule is enforced.

A UI check improves usability, but it should not be the only protection for a rule that must always hold. The Business Layer should enforce the relevant rule when it owns that rule.

### 3.2 Business Layer — `DVLDBussnessLayer`

**Primary responsibility:** represent domain concepts and coordinate business operations.

Examples in this repository include:
- `clsPerson`
- `clsUser`
- `clsApplication`
- `clsApplicationType`
- `clsLocalDrivingLicenseApplication`
- `clsLicense`
- `clsDriver`
- `clsInternationalLicense`
- `clsTestType`
- `clsTestAppointment`
- `clsTest`
- `clsDetaindLicense`

Business objects commonly expose operations such as `Find(...)`, `Save()`, and domain-specific methods. They call corresponding Data Access methods to retrieve or persist information. Some operations combine multiple steps. For example, `clsLocalDrivingLicenseApplication.Save()` first saves the base application through its base class and then saves or updates the local-driving-application-specific record. License and test classes also expose operations for domain workflows, such as checking test progress, issuing a license, renewing or replacing a license, and detaining/releasing a license.

Typical responsibilities:
- Represent domain data and behavior.
- Coordinate multi-step operations.
- Decide whether an operation is valid according to the rules implemented there.
- Call the Data Access Layer to read or write persisted information.
- Translate persistence results into business-level success/failure outcomes or business objects.

What it should generally avoid:
- Showing `MessageBox` dialogs or manipulating WinForms controls.
- Depending on specific forms or screen layouts.
- Owning low-level SQL connection and query details.

**Important distinction:** not every check in the application is automatically a business rule. A textbox being empty is often a UI validation concern; whether an applicant is eligible to progress to the next test or whether a license can be issued is a domain/workflow concern. Exact placement should follow the meaning of the rule.

### 3.3 Data Access Layer — `DLVDData_Access`

**Primary responsibility:** communicate with SQL Server and translate database results into values the caller can use.

Examples include:
- `clsPersonData`
- `clsUserData`
- `clsApplicationData`
- `clsLocalDrivingLicenseApplicationData`
- `clsLicenseData`
- `clsTestAppointmentData`
- `clsTestData`
- `clsInternationalDrivingLicenseData`
- `clsDetaindLicenseData`

These classes use ADO.NET types such as `SqlConnection`, `SqlCommand`, `SqlDataReader`, and `DataTable`. Methods execute SQL statements, supply parameters, read columns, and return IDs, booleans, objects-by-reference, or tabular results, depending on the method.

Typical responsibilities:
- Open and close database connections.
- Execute parameterized SQL commands.
- Read and map database columns.
- Insert, update, delete, and select records.
- Return data or an operation result to the Business Layer.

What it should generally avoid:
- Deciding how a form should display the result.
- Showing UI messages.
- Implementing high-level user workflows that belong in the Business Layer.

Parameterized SQL commands help keep values separate from SQL text and reduce SQL-injection risk when used correctly. The code should continue to use parameters rather than concatenate user input into SQL statements.

## 4. How the Layers Connect

A layer is not connected to the next merely because its files are in a neighboring folder. The connection is created through project references and method calls.

In this solution:
1. The WinForms project references `DVLDBussnessLayer`.
2. The Business Layer project references `DLVDData_Access`.
3. Business classes call static data methods or other data-access methods.
4. Data-access methods execute SQL against the configured database.
5. Returned values move back up the call chain.

A simplified call stack is:

```text
Form event handler
    ↓
Business object method
    ↓
Data Access method
    ↓
SQL Server
    ↑
Database result
    ↑
Business object/result
    ↑
Form displays the result
```

The reverse arrows represent the return of results, not a new dependency from the database back to the UI.

## 5. Example: Finding a Person

The source contains `clsPerson.Find(int PersonID)` in the Business Layer and person lookup methods in `clsPersonData` in the Data Access Layer.

A typical flow is:

1. The user enters or selects a person ID in a form.
2. The form calls the business operation, for example `clsPerson.Find(PersonID)`.
3. The Business Layer prepares the values needed for the lookup and calls a Data Access method.
4. The Data Access method executes a parameterized query and reads the matching row.
5. The result is returned to the Business Layer.
6. If a record is found, the Business Layer creates and returns a `clsPerson` object; if not, it can return `null` or a failure result according to that method's contract.
7. The form checks the result and displays the person's details or informs the user that no matching person was found.

This separation means the form does not need to know the table name, SQL syntax, or how `SqlDataReader` maps columns. The Data Access Layer does not need to know which textbox initiated the lookup or which control will display the person's name.

## 6. Example: Saving a Local Driving License Application

`clsLocalDrivingLicenseApplication` derives from the general `clsApplication` business class and has its own data-access class.

The save flow illustrates both layering and object-oriented reuse:

1. The form collects the information required to create or update an application and passes it to the relevant Business Layer object.
2. `clsLocalDrivingLicenseApplication.Save()` synchronizes its mode with the base application mode and calls `base.Save()` to save the general application portion.
3. If that succeeds, the method performs the local-driving-license-specific insert or update.
4. The Data Access Layer executes the relevant SQL statements.
5. The success/failure result travels back through the Business Layer to the form.

Why this is useful:
- `clsApplication` owns behavior shared by application types.
- `clsLocalDrivingLicenseApplication` adds data and operations specific to local driving-license applications.
- The Data Access Layer contains the SQL operations.
- The form handles interaction and feedback instead of writing SQL itself.

A multi-step save like this must be considered carefully: if the base application save succeeds but the specific record save fails, the database can be left with partial work unless the operation is protected by a transaction or compensating logic. A future improvement could make the multi-table operation transactional where appropriate.

## 7. Example: Scheduling and Taking Tests

The codebase separates test concepts into business classes such as `clsTestType`, `clsTestAppointment`, and `clsTest`, with matching Data Access classes.

The application workflow can be understood as several responsibilities:
- The UI gathers appointment choices and displays available information.
- Business operations coordinate application/test state, including checks related to prior tests, attempts, and passing required tests.
- Data Access methods persist appointments and test outcomes and retrieve related records.

The benefit is that scheduling screens do not have to contain every database query and workflow rule. The workflow can be reasoned about through the Business Layer methods, while query details stay in the Data Access Layer.

## 8. Inheritance and Composition of Responsibilities

The architecture uses more than project separation; the Business Layer also uses object-oriented relationships.

### Inheritance

`clsLocalDrivingLicenseApplication` inherits from `clsApplication`. This represents a specialized application that reuses general application data and behavior while adding local-license-specific information.

Inheritance is useful when the derived concept truly is a specialized form of the base concept. It should not be used only to avoid writing a few lines of code.

### Business objects calling other business objects

Some operations require more than one domain concept. A local license application may need general application information, a driver record, test history, and license information. Having business classes collaborate allows each class to own its domain-specific behavior while a workflow method coordinates the steps.

### Static Data Access methods

Many Data Access operations are static methods. Business classes can call these methods without constructing a separate data-access object for every operation. This is a straightforward pattern for a learning project, although it can make substituting a fake repository in unit tests harder than using interfaces and dependency injection.

## 9. Separation of Responsibilities: A Concrete Comparison

### Without separation

A button handler might:
- Validate all business eligibility rules.
- Write a SQL query.
- Open a connection.
- Read result columns.
- Insert multiple related records.
- Update controls and show messages.

Consequences:
- The form becomes large and difficult to read.
- Similar rules may be copied into other forms.
- SQL changes require editing UI code.
- Testing domain rules without opening the form becomes difficult.
- Reusing the same operation from another screen becomes awkward.

### With the current layered approach

- **Form:** gathers input and presents outcomes.
- **Business object:** coordinates the operation and domain rules.
- **Data Access class:** executes the database interaction.

Consequences:
- Each class has a clearer reason to change.
- SQL details are centralized in data-access methods.
- Business operations can be reused by different forms.
- Code review becomes easier because each layer has a narrower purpose.
- Problems can be investigated layer by layer.

This is not a guarantee that every class is perfectly separated. The architecture provides boundaries; developers still need to keep each class within its intended responsibility.

## 10. Coupling and Cohesion

### Coupling

Coupling describes how strongly one part of a system depends on another part's details.

For example, a form that knows table names and SQL syntax is tightly coupled to the database implementation. A form that calls a business method such as `clsPerson.Find(PersonID)` depends on a more focused business contract instead.

The current project reduces direct coupling between the UI and SQL by placing data operations in the Data Access Layer. The Business Layer still depends on concrete data-access classes, so this is a layered design but not a fully dependency-inverted architecture.

### Cohesion

Cohesion describes how closely the responsibilities inside one module belong together.

A `clsPersonData` class that focuses on person-related database operations has higher cohesion than a single utility class that mixes people queries, login UI, image display, and license issuance. Keeping related operations together makes a class easier to understand and maintain.

## 11. Error Handling and Data Integrity

The Data Access Layer commonly catches exceptions and returns a failure value, and some methods write exception messages to the console. This is simple, but it has trade-offs:
- A caller may not know why an operation failed.
- Console output may not be visible in a GUI application.
- Returning `false`, `null`, or `-1` can make different failure causes difficult to distinguish.

Potential improvements:
1. Use a consistent error-handling strategy across the DAL.
2. Log technical details in a controlled way.
3. Return clear operation results or allow exceptions to propagate to a layer that can handle them appropriately.
4. Use database transactions for operations that must succeed or fail as a unit.
5. Preserve parameterized SQL and review data types/null handling.
6. Avoid exposing raw database exception text directly to end users.

The right choice depends on the scale and requirements of the application; consistency is more important than adding complexity without a need.

## 12. Configuration and Database Dependency

The Data Access Layer centralizes its connection string through `clsDataSittings.ConnectionString`. That keeps connection configuration in one place rather than scattering it throughout every query class.

The repository snapshot does not include a SQL script or database backup. Therefore, the exact database setup must be supplied separately, and the connection string must point to a database with the expected tables and views.

For a future improvement, connection settings could be stored in application configuration rather than hard-coded in a source file, and secrets should not be committed to a public repository.

## 13. Current Design Strengths

Based on the source structure, the project demonstrates several useful practices:
- Separate Visual Studio projects for the UI, Business Layer, and Data Access Layer.
- Domain-focused business classes.
- Dedicated data-access classes for database operations.
- Parameterized SQL commands in the inspected data-access methods.
- Reuse of base application behavior through inheritance.
- Reusable WinForms UserControls for person and license/application information.
- Explicit save/find/workflow methods rather than putting all operations into UI event handlers.

## 14. Possible Next Improvements

These are improvement opportunities, not claims that the current project is missing every item:

- **Interfaces for persistence:** introduce interfaces such as `IPersonRepository` where they provide value, making Business Layer tests less dependent on concrete static methods.
- **Dependency injection:** pass dependencies into classes rather than letting every business object directly choose its data-access implementation.
- **Transactions:** make multi-table operations atomic where partial completion would leave inconsistent data.
- **Consistent result types:** distinguish not-found, validation failure, and database failure rather than representing all outcomes with a single boolean or sentinel value.
- **Centralized logging:** replace console-only exception handling with an application-appropriate logging approach.
- **Configuration management:** keep connection strings and environment-specific settings out of source code.
- **Automated tests:** add tests for domain rules and key workflows, with database dependencies isolated where practical.
- **Naming consistency:** gradually correct inconsistent spellings in project/folder/class names when doing so will not break references.

## 15. Summary

The central idea is not simply to create three folders. The goal is to give each layer a clear responsibility and make the direction of dependencies understandable:

- The **Presentation Layer** handles user interaction.
- The **Business Layer** coordinates domain operations and workflow rules.
- The **Data Access Layer** handles persistence and SQL Server communication.

When a form calls a business operation, and the business operation calls a data-access method, the system becomes easier to reason about than a design in which every screen performs every task. This separation supports maintainability, reuse, and testing while still requiring careful error handling, transactions, and consistent design decisions as the project grows.
