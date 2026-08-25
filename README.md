# 🚗 Drivers and Vehicles License Department (DVLD)

A Windows desktop application for managing people, users, driving license applications, tests, drivers, and license records.

[![Platform](https://img.shields.io/badge/platform-Windows-informational.svg)](#)
[![Framework](https://img.shields.io/badge/.NET%20Framework-4.8-blue.svg)](#)
[![UI](https://img.shields.io/badge/UI-Windows%20Forms-512BD4.svg)](#)
[![Database](https://img.shields.io/badge/database-SQL%20Server-red.svg)](#)
[![Language](https://img.shields.io/badge/language-C%23-239120.svg)](#)

## 🧭 Contents :

* [📌 Overview](#-overview)
* [✨ Features](#-features)
* [🏗️ Architecture](#️-architecture)
* [🧰 Tech Stack](#-tech-stack)
* [🔄 Main Workflow](#-main-workflow)
* [🧪 Testing](#-testing)
* [📁 Repository Notes](#-repository-notes)
* [🤝 Contributing](#-contributing)

---

## 📌 Overview :

DVLD is a C# Windows Forms application that models the workflow of a driving and vehicle licensing department.

The system separates the presentation, business, and data-access responsibilities into independent projects.

The application covers the lifecycle of local driving license applications from applicant registration through test scheduling and test results to license issuance.

The repository also includes database design files for the DVLD relational model.

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

* User login and logout flow.
* Active and inactive user states.
* Add, edit, search, and delete users.
* Change user passwords.
* User-specific session information.
* Remember-me login support.
* Password hashing with salt in the authentication flow.

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

The solution is built with 3-tier architecture:

🖥️ Presentation Layer
        ↓
🧠 Business Layer
        ↓
🗄️ Data Access Layer

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

The solution contains three C# projects and uses project references to connect the presentation layer, business layer, and data-access layer.

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
        🪪 Driver / License
```

The business layer contains checks for conditions such as:

* minimum age for a license class
* duplicate applications
* test scheduling availability
* passed test count
* driver existence
* license lookup

---

## 🧪 Testing :

The business layer contains test-related domain and appointment logic, including:

* test scheduling
* test result persistence
* passed-test validation
* appointment availability
* license history

## 🤝 Contributing :

Contributions should keep the existing layer boundaries clear.

Before opening a pull request:

* keep database operations in the data-access layer
* keep domain rules in the business layer
* keep UI concerns in the presentation layer
* avoid duplicating business rules inside forms
* document database changes
* test workflow changes before merging

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

DVLD is a layered C# WinForms system that demonstrates how a desktop licensing application can organize domain logic, SQL Server persistence, and user interaction.

It is a useful reference project for studying:

* layered architecture
* object-oriented design
* CRUD workflows
* SQL Server data access
* WinForms application design
* authentication flows
* relational database modeling
* multi-step business processes

Built with C# and .NET Framework 4.8.
