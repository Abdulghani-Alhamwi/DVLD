# 🚗 Drivers and Vehicles License Department (DVLD)

A Windows desktop application for managing people, users, driving license applications, tests, drivers, and license records.

[![Platform](https://img.shields.io/badge/platform-Windows-informational.svg)](#)
[![Framework](https://img.shields.io/badge/.NET%20Framework-4.8-blue.svg)](#)
[![UI](https://img.shields.io/badge/UI-Windows%20Forms-512BD4.svg)](#)
[![Database](https://img.shields.io/badge/database-SQL%20Server-red.svg)](#)
[![Language](https://img.shields.io/badge/language-C%23-239120.svg)](#)

## 📌 Overview :

DVLD is a C# Windows Forms application that models the workflow of a driver and vehicle licensing department.

The system separates presentation, business, and data-access responsibilities into independent projects.

The application covers the lifecycle of local driving license applications, from applicant registration through test scheduling and test results to license issuance.

The repository also includes database design files for the DVLD relational model.

---

## 🎯 The Problem This Project Solves :

Managing a driving license process involves more than issuing a license.

A person can move through several connected stages:

```text
👤 Person
   ↓
📝 Local Driving License Application
   ↓
📅 Test Appointment
   ↓
🧪 Test Result
   ↓
🚘 Driver
   ↓
🪪 License
```

Each stage depends on information from the previous stages.

Without a connected system, a licensing department can face problems such as:

* Duplicate applications
* Incorrect applicant information
* Invalid license class selection
* Incorrect test scheduling
* Missing test history
* Inconsistent driver records
* Difficulty tracking application progress
* Difficulty finding license records
* Repeated business rules across different screens
* Weak traceability between applications, tests, drivers, and licenses

The core problem is:

> **How can a licensing department manage a multi-step licensing process while keeping people, applications, tests, drivers, and licenses connected and consistent?**

---

## ✨ Features :

<details>
<summary>👤 People Management</summary>

* Add, edit, view, search, and remove people.
* Store personal information such as national number, name, date of birth, phone, email, address, and country.
* Reuse person records across applications and user accounts.
* Filter and paginate people records.

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

</details>

<details>
<summary>🧪 Test Management</summary>

* Manage test types.
* Configure test fees.
* Schedule test appointments.
* Check appointment availability.
* Record test results.
* Track passed tests.
* Review license history.

</details>

<details>
<summary>🪪 Driver and License Management</summary>

* Register drivers.
* Detect whether a person already has a driver record.
* Issue driving licenses.
* Support license issue reasons through the business layer.
* Find license records associated with local driving license applications.

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

## 🏗️ Architecture :

The solution is built using a three-tier architecture:

```text
🖥️ Presentation Layer
        ↓
🧠 Business Layer
        ↓
🗄️ Data Access Layer
        ↓
💾 SQL Server Database
```

The solution contains three C# projects and uses project references to connect the presentation layer, business layer, and data-access layer.

### 🖥️ Presentation Layer :

The presentation layer contains the Windows Forms screens, reusable controls, navigation, user interaction, and presentation logic.

### 🧠 Business Layer :

The business layer contains the domain objects and business rules that control the licensing workflow.

It is responsible for validation, workflow decisions, and coordinating persistence operations.

### 🗄️ Data Access Layer :

The data-access layer contains the SQL Server data-access classes and CRUD operations.

It is responsible for database communication and persistence.

---

## 🧰 Tech Stack :

| Area         | Technology            |
| ------------ | --------------------- |
| Language     | C#                    |
| UI           | Windows Forms         |
| Runtime      | .NET Framework 4.8    |
| IDE          | Visual Studio         |
| Database     | Microsoft SQL Server  |
| Database API | System.Data.SqlClient |
| Data Model   | Relational database   |

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
📅 Test Appointment
   │
   ▼
🧪 Test Result
   │
   ├── ❌ Not passed
   │       └── Retest flow
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

---

## 🧪 Testing :

The business layer contains test-related domain and appointment logic, including:

* Test scheduling
* Test result persistence
* Passed-test validation
* Appointment availability
* License history

---

## 📁 Repository Notes :

The repository contains the source code, project files, database design assets, and Visual Studio project structure required to work with the application.

The database design files include the DVLD ERD and relational schema.

For a clean Git repository, Visual Studio build and IDE artifacts such as `.vs`, `bin`, and `obj` should be excluded using `.gitignore`.

---

## 🤝 Contributing :

Contributions should keep the existing layer boundaries clear.

Before opening a pull request:

* Keep database operations in the data-access layer.
* Keep domain rules in the business layer.
* Keep UI concerns in the presentation layer.
* Avoid duplicating business rules inside forms.
* Document database changes.
* Test workflow changes before merging.

A useful pull request should explain:

```text
What changed?
Why was it needed?
Which layer changed?
How was it tested?
Does the database schema change?
```

---

## ⭐ Project Summary :

DVLD is a layered C# Windows Forms system that demonstrates how a desktop licensing application can organize domain logic, SQL Server persistence, and user interaction.

It is a useful reference project for studying:

* Layered architecture
* Object-oriented design
* CRUD workflows
* SQL Server data access
* Windows Forms application development
* Authentication flows
* Relational database modeling
* Multi-step business processes

Built with C# and .NET Framework 4.8.
