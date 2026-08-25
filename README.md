# 🚗 Drivers and Vehicles License Department (DVLD)

A C# Windows Forms desktop application for managing people, users, driving license applications, tests, drivers, and license records through one connected licensing workflow.

[![Platform](https://img.shields.io/badge/platform-Windows-informational.svg)](#)
[![Framework](https://img.shields.io/badge/.NET%20Framework-4.8-blue.svg)](#)
[![UI](https://img.shields.io/badge/UI-Windows%20Forms-512BD4.svg)](#)
[![Database](https://img.shields.io/badge/database-SQL%20Server-red.svg)](#)
[![Language](https://img.shields.io/badge/language-C%23-239120.svg)](#)

## 📌 Overview :

DVLD is a C# Windows Forms application designed to model the core operations of a driver and vehicle licensing department.

The project focuses on representing the licensing process as a connected business workflow rather than as a collection of unrelated screens.

It manages people, users, driving license applications, license classes, application types, test appointments, test results, drivers, and licenses while keeping the relationships between these entities explicit.

The solution follows a three-tier architecture that separates the presentation layer, business layer, and data-access layer.

The application also uses a database-oriented loading approach designed to handle large record sets by retrieving data gradually instead of loading the entire dataset into memory at once.

This approach helps keep data retrieval controlled and supports a smoother user experience when working with growing database tables.

---

## 🎯 The Problem This Project Solves :

A driving license department does not manage one isolated operation.

A person can move through a sequence of related stages, and every stage depends on information from previous stages.

```text
👤 Person
   │
   ▼
📝 Local Driving License Application
   │
   ▼
📚 License Class
   │
   ▼
📅 Test Appointment
   │
   ▼
🧪 Test Result
   │
   ├── ❌ Not Passed
   │       └── 📅 Retest
   │
   └── ✅ Passed
           │
           ▼
        🚘 Driver
           │
           ▼
        🪪 License
```

The driving license management process involves several challenges that this project is designed to address, including:

* Duplicate applications
* Incorrect applicant information
* Invalid license class selection
* Incorrect test scheduling
* Missing test history
* Inconsistent driver records
* Poor responsiveness when tables contain large numbers of records

The project addresses these problems by combining a connected domain model, centralized business rules, structured data access, and controlled record loading.

---

## ✨ Features :

<details>
<summary>👤 People Management</summary>

* Add, edit, view, search, and remove people.
* Store personal information such as national number, name, date of birth, phone, email, address, and country.
* Reuse person records across applications and user accounts.
* Filter and paginate people records.
* Retrieve records progressively instead of loading large datasets into memory at once.

</details>

<details>
<summary>🔑 User and Access Management</summary>

* User login and logout.
* Support active and inactive user states.
* Add, edit, search, and delete users.
* Change user passwords.
* Maintain user-specific session information.
* Support remember-me login.
* Hash and salt passwords during authentication.

</details>

<details>
<summary>📝 Driving License Applications</summary>

* Create local driving license applications.
* Associate an applicant with a license class.
* Validate age requirements.
* Detect existing applications for the same applicant and license class.
* Track application status.
* Search and filter applications.
* View passed test counts.
* Load application records gradually when retrieving larger datasets.

</details>

<details>
<summary>🧪 Test Management</summary>

* Manage test types.
* Configure test fees.
* Schedule test appointments.
* Check appointment availability.
* Record test results.
* Track passed tests.
* Support retest workflows.
* Review license history.

</details>

<details>
<summary>🪪 Driver and License Management</summary>

* Register drivers.
* Detect whether a person already has a driver record.
* Issue driving licenses.
* Support license issue reasons through the business layer.
* Find license records associated with local driving license applications.
* Preserve the relationships between people, drivers, applications, and licenses.

</details>

<details>
<summary>⚙️ Application and Reference Data</summary>

* Manage application types.
* Manage license classes.
* Manage countries.
* Store application fees and license class fees.
* Store license validity periods.
* Store minimum allowed ages for license classes.

</details>

---

## ⚡ Progressive Database Loading :

The project is designed with large record sets in mind.

Instead of retrieving every available row from the database and keeping the complete result set in memory, the application can load records gradually as they are needed.

This approach can be described as:

```text
🗄️ SQL Server
      │
      ▼
📦 Small Set of Records
      │
      ▼
🖥️ Display Current Data
      │
      ▼
➡️ Request More Records
      │
      ▼
📦 Next Set of Records
      │
      ▼
🖥️ Update the View
```

The purpose of this loading strategy is to control:

* 💾 Memory usage
* 🗄️ Database workload
* 🌐 Data transfer volume
* ⏱️ Initial loading time
* 🖥️ UI responsiveness

Instead of treating a database table as something that must always be loaded in full, the application can treat it as a source of records that are retrieved progressively.

For example, a large `People` table can be accessed through smaller result sets rather than loading every person at the same time.

```text
Instead of:

Database
   ↓
All Records
   ↓
Memory
   ↓
UI

The application can use:

Database
   ↓
Current Batch
   ↓
Memory
   ↓
UI
   ↓
Next Batch
   ↓
Database
```

This design is useful for entities such as:

* People
* Users
* Applications
* Test appointments
* Licenses
* Drivers

The exact loading strategy can be implemented through filtering, pagination, offset-based queries, or other controlled retrieval mechanisms at the data-access level.

The important principle is:

> **Retrieve the data required by the current operation instead of unnecessarily loading the entire dataset.**

---

## 🏗️ Architecture :

The solution uses a three-tier architecture:

```text
🖥️ Presentation Layer
        ↓
🧠 Business Layer
        ↓
🗄️ Data Access Layer
        ↓
💾 SQL Server Database
```

Each layer has a defined responsibility.

### 🖥️ Presentation Layer :

The presentation layer contains the Windows Forms screens, reusable controls, navigation, user interaction, searching, filtering, and presentation logic.

It is responsible for communicating with the user and displaying application results.

It should not contain SQL queries or central business rules.

### 🧠 Business Layer :

The business layer contains the domain objects and business rules that control the licensing workflow.

It is responsible for:

* Validation
* Business rules
* Workflow decisions
* Domain operations
* Coordinating persistence operations

Examples include validating applicant age, checking duplicate applications, verifying test progression, and determining whether an operation can continue.

### 🗄️ Data Access Layer :

The data-access layer contains the SQL Server data-access classes and CRUD operations.

It is responsible for:

* Database communication
* SQL queries
* Data retrieval
* Data insertion
* Data updates
* Data deletion
* Controlled record loading

The data-access layer is also the appropriate place to implement database-side filtering and progressive loading strategies.

---

## 🧰 Tech Stack :

| Area         | Technology              |
| ------------ | ----------------------- |
| Language     | C#                      |
| UI           | Windows Forms           |
| Runtime      | .NET Framework 4.8      |
| IDE          | Visual Studio           |
| Database     | Microsoft SQL Server    |
| Database API | System.Data.SqlClient   |
| Data Model   | Relational database     |
| Architecture | Three-tier architecture |

---

## 🔄 Main Workflow :

A typical local driving license process follows this flow:

```text
👤 Person
   │
   ▼
📝 Local Driving License Application
   │
   ▼
📚 License Class
   │
   ▼
📅 Test Appointment
   │
   ▼
🧪 Test Result
   │
   ├── ❌ Not Passed
   │       └── 📅 Retest
   │
   └── ✅ Passed
           │
           ▼
        🚘 Driver
           │
           ▼
        🪪 License
```

The business layer contains checks for conditions such as:

* Minimum age for a license class
* Duplicate applications
* Test scheduling availability
* Passed test count
* Driver existence
* License lookup

The workflow keeps the major licensing entities connected so that each operation can use information from the previous stages.

---

## 🧪 Testing :

The business layer contains test-related domain and appointment logic, including:

* Test scheduling
* Test result persistence
* Passed-test validation
* Appointment availability
* License history

The business layer is the main area for future automated testing because it contains the core workflow rules and validation logic.

---

## 📁 Repository Notes :

The repository contains the source code, project files, database design assets, and Visual Studio project structure required to work with the application.

The database design files include the DVLD ERD and relational schema.

For a clean Git repository, Visual Studio build and IDE artifacts such as `.vs`, `bin`, and `obj` should be excluded using `.gitignore`.

Database credentials and other sensitive configuration values should not be committed to source control.

---

## 🤝 Contributing :

Contributions should keep the existing layer boundaries clear.

Before opening a pull request:

* Keep database operations in the data-access layer.
* Keep domain rules in the business layer.
* Keep UI concerns in the presentation layer.
* Avoid duplicating business rules inside forms.
* Keep large-record retrieval controlled through database-side filtering or progressive loading where appropriate.
* Document database changes.
* Test workflow changes before merging.

A useful pull request should explain:

```text
What changed?
Why was it needed?
Which layer changed?
How was it tested?
Does the database schema change?
Does the change affect data loading or query performance?
```

---

## ⭐ Project Summary :

DVLD is a layered C# Windows Forms system that models a driver and vehicle licensing workflow from person registration through application processing, testing, driver registration, and license issuance.

The project demonstrates how to combine:

* 🏗️ Three-tier architecture
* 🧩 Object-oriented domain modeling
* 🔄 Multi-step business workflows
* 🗄️ SQL Server persistence
* 🔐 Authentication and access management
* 📊 Relational database design
* 🔎 Search and filtering
* ⚡ Progressive database loading
* 💾 Controlled memory usage
* 🖥️ Windows Forms application development

A key design goal is to keep the application responsive as the amount of stored data grows.

The system therefore does not depend on loading complete database tables into memory for every operation.

Instead, records can be retrieved progressively based on the current screen, query, filter, or requested batch.

This provides a more controlled path from:

```text
🗄️ Database
      ↓
🔎 Query
      ↓
📦 Required Records
      ↓
🧠 Business Layer
      ↓
🖥️ Presentation Layer
```

The result is a licensing application designed around clear separation of responsibilities, connected business workflows, and controlled database access.

Built with C# and .NET Framework 4.8.
