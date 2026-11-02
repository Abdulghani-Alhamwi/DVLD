# 🚗 Drivers and Vehicles License Department (DVLD)

A C# Windows Forms desktop application for managing the complete driving license lifecycle. The system connects people management, user administration, license applications, examination procedures, driver registration, and license operations through a structured business workflow.

The project emphasizes separation of concerns, business-rule enforcement, database integrity, security, reusable components, and efficient data retrieval.

| Platform | Framework | UI | Database | Language |
|---|---|---|---|---|
| Desktop | .NET Framework 4.8 | Windows Forms | Microsoft SQL Server | C# |

---

## 📌 Overview:

DVLD is a desktop application developed to represent the core administrative and operational activities of a driving license department.

The application models the licensing process as a sequence of related business operations rather than treating each feature as an independent module.

It maintains the relationships between people, users, applications, license classes, test appointments, examination results, drivers, and issued licenses. These relationships allow the system to enforce business rules and maintain consistency throughout the licensing lifecycle.

The solution follows a three-tier architecture that separates the presentation layer, business layer, and data-access layer. Each layer has a defined responsibility, reducing dependencies and making the application easier to maintain and extend.

The project also includes a separate Utility Library developed as a C# class library targeting .NET Framework 4.8. It centralizes reusable operations such as cryptographic processing, SQL query construction, DataGridView management, input validation, formatting, file management, and Windows Forms customization.

Another important aspect of the application is its approach to database performance. Instead of retrieving entire database tables whenever a form is opened, the application uses server-side filtering and progressive data retrieval to load records in controlled batches.

Together, these design decisions provide an organized foundation for managing interconnected licensing operations while maintaining control over application complexity, database communication, and resource consumption.

## 🎯 The Problem This Project Solves:

A driving license department manages a sequence of administrative operations in which each operation depends on the information and outcomes of previous operations.

For example, issuing a driving license requires more than registering an applicant. The system must identify the applicant, validate the selected license class, verify eligibility, schedule the required examinations, record their results, and confirm that all licensing requirements have been satisfied.

Each stage must remain connected to the original applicant and application to preserve the integrity of the overall process.

DVLD represents this workflow through connected business entities and controlled state transitions.

### Licensing Workflow:

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
          ┌──────┴──────┐
          │             │
      ❌ Not Passed  ✅ Passed
          │             │
          ▼             │
       📅 Retest        │
          │             │
          └──► Repeat   │
               Test     │
                        ▼
                    🚘 Driver
                        │
                        ▼
                     🪪 License
```

The application addresses several challenges associated with this type of system:

- Duplicate applications and inconsistent applicant records.
- Invalid license class selection and eligibility violations.
- Incorrect examination scheduling.
- Incomplete examination and retest history.
- Inconsistent relationships between applicants, drivers, and licenses.
- Incorrect license lifecycle operations.
- Excessive database requests and unnecessary memory consumption when handling large record sets.

The solution combines a structured domain model, centralized business rules, relational database design, reusable supporting components, and controlled data retrieval to manage these requirements.

---

## 🗄️ Database Design:

The database follows a relational model designed to represent the relationships between people, users, applications, drivers, licenses, and other licensing entities.

The schema follows Third Normal Form (3NF) to reduce unnecessary data duplication and improve consistency across related records.

Relationships between entities are maintained through appropriate keys, allowing the application to connect related information and preserve the dependencies required by different business operations.

The design also considers query performance. Where appropriate, selective denormalization can reduce unnecessary joins for frequently executed operations while maintaining the intended data-integrity requirements.

The database design supports the application's main business operations, including:

- Managing people and their personal information.
- Associating user accounts with existing people records.
- Connecting applicants with their driving license applications.
- Associating applications with license classes.
- Maintaining test appointments and examination results.
- Connecting drivers with their issued licenses.
- Maintaining relationships between original licenses and renewal or replacement records.
- Recording license detention and release operations.

The repository includes the DVLD Entity Relationship Diagram (ERD) and relational database schema as part of its database design assets.

## 🏗️ Architecture:

The solution follows a three-tier architecture to separate user interaction, business processing, and database communication.

Each layer is responsible for a specific part of the application, allowing changes to one area to have fewer dependencies on the others.

### Architecture Overview:

```text
          🖥️ Presentation Layer
                    ↓
             🧠 Business Layer
                    ↓
            🗄️ Data Access Layer
                    ↓
           💾 SQL Server Database
```

### 🖥️ Presentation Layer:

The Presentation Layer contains the Windows Forms interface through which users interact with the application.

It manages the visual representation of the system and communicates user actions to the business layer.

Its responsibilities include:

- Displaying application forms and controls.
- Handling user interactions.
- Managing navigation between application modules.
- Collecting and presenting user input.
- Displaying retrieved records.
- Presenting validation results.
- Communicating with business objects.
- Managing interface-level searching, filtering, and sorting interactions.

The presentation layer focuses on user interaction. Business rules and SQL operations are handled by the appropriate layers.

### 🧠 Business Layer:

The Business Layer contains the domain objects and business rules that govern the licensing workflow.

It acts as the intermediary between the presentation and data-access layers.

Its responsibilities include:

- Implementing business rules.
- Validating application requirements.
- Managing licensing workflows.
- Enforcing domain constraints.
- Coordinating application operations.
- Managing domain-level decisions.
- Communicating with the data-access layer.

Centralizing business logic prevents the same rules from being independently implemented across multiple forms.

It also provides a consistent location for maintaining and extending the application's operational requirements.

### 🗄️ Data Access Layer:

The Data Access Layer handles communication with Microsoft SQL Server.

It contains the database operations used by the business layer to retrieve and modify application data.

Its responsibilities include:

- Executing SQL queries.
- Retrieving database records.
- Inserting new records.
- Updating existing records.
- Deleting records.
- Managing database interactions.
- Implementing database-side filtering and pagination.
- Returning query results to the business layer.

Keeping persistence operations within this layer separates database concerns from business processing and user interface logic.

---

## ✨ Services:

The application is organized into several services, each responsible for a specific area of the licensing management process.

These services work together through shared business entities and database relationships. This organization allows each administrative operation to maintain its own responsibilities while participating in the overall licensing workflow.

### 👤 People Management:

The People Management service provides centralized management of personal information used throughout the application.

People records act as a common source of personal information for applicants, users, and other entities that require identification details.

Supported operations:

- Create, update, delete, and search for people records.
- Maintain personal information and contact details.
- Associate people with nationality information.
- Retrieve and display records through searchable and filterable interfaces.

Centralizing personal information reduces duplication and allows other application modules to reference the same person record.

### 🔑 User and Access Management:

The User Management service handles application accounts and their relationships with existing people records.

It also provides the mechanisms required to manage account status, credentials, and access permissions.

Supported operations:

- Create and manage user accounts.
- Associate user accounts with existing people records.
- Manage usernames and passwords.
- Support active and inactive accounts.
- Assign permissions to control access to application features.

Separating user accounts from personal records allows the application to maintain account-related information independently while preserving the relationship between each account and its corresponding person.

### 📝 Driving License Applications:

The Driving License Applications service manages the initial stage of the licensing process.

It connects an applicant with a selected license class and maintains the application status throughout the examination and issuance workflow.

Supported operations:

- Create local driving license applications.
- Associate applicants with specific license classes.
- Validate minimum age requirements.
- Detect existing applications for the same applicant and license class.
- Track application status.
- Search and filter application records.
- Display the number of successfully completed tests.
- Retrieve application records incrementally when working with large result sets.

Business validations ensure that applications satisfy the required conditions before proceeding to subsequent stages.

### 🧪 Test Management:

The Test Management service handles the examinations required to qualify an applicant for a driving license.

The application supports three examination categories:

- Vision Test.
- Written Test.
- Street Test.

Each examination is associated with its corresponding driving license application, allowing the system to maintain the applicant's examination history and progress.

Supported operations:

- Manage vision, written, and street tests.
- Schedule test appointments for eligible applications.
- Record examination results.
- Maintain test and retest history.
- Track passed and failed attempts.
- Validate examination eligibility and appointment requirements.
- Schedule retest appointments following unsuccessful attempts.
- Calculate the number of successfully completed tests for each application.

Maintaining examination history allows the system to track the applicant's progress and determine whether the required tests have been completed before license issuance.

### 🪪 Driver and License Management:

This service manages the transition from an eligible applicant to a registered driver and handles the operations associated with issued driving licenses.

It also maintains the relationships between drivers, license classes, original applications, and subsequent license operations.

#### 🚘 Drivers List:

Provides access to registered driver records and their associated information.

Supported operations:

- View registered drivers.
- Search and filter driver records.
- View driver details and associated personal information.
- Access driving licenses associated with each driver.

#### 🪪 Local Driving License Issuance:

Handles the creation of local driving licenses after the applicant has successfully completed the required examinations.

Supported operations:

- Issue local driving licenses after successful completion of all required tests.
- Associate licenses with their corresponding drivers and license classes.
- Record license issue dates and expiration dates.
- Maintain the relationship between issued licenses and their original applications.

#### 🔄 Local Driving License Renewal:

Manages the renewal of existing local driving licenses when their validity needs to be extended.

The renewal process maintains the connection between the original license, the renewal application, and the newly issued license.

Supported operations:

- Identify licenses eligible for renewal.
- Validate renewal requirements.
- Create renewal applications.
- Apply the appropriate renewal fees.
- Issue renewed licenses with updated validity periods.
- Deactivate previous licenses after successful renewal.
- Maintain the relationships between original licenses, renewal applications, and renewed licenses.

#### ♻️ Local Driving License Replacement:

Handles replacement operations for licenses that have been lost or damaged.

The replacement process preserves the relationship between the original license and its replacement while recording the application and associated fees.

Supported operations:

- Replace lost or damaged local driving licenses.
- Support different replacement application types.
- Validate driver eligibility.
- Require an active local license of the corresponding class.
- Create replacement applications with the appropriate fees.
- Issue replacement licenses.
- Deactivate previous licenses after successful replacement.
- Maintain the relationships between original and replacement licenses.

#### 🌍 International Driving License Issuance:

Provides functionality for issuing international driving licenses to eligible drivers based on their existing local license records.

Supported operations:

- Validate the driver's existing local driving license.
- Verify eligibility for international license issuance.
- Prevent multiple active international licenses for the same driver.
- Associate international licenses with their corresponding drivers and local licenses.
- Record license information and validity dates.

#### 🌍 International Driving License Management:

Provides administrative access to issued international driving licenses and their associated information.

Supported operations:

- View issued international driving licenses.
- Search and filter international license records.
- Display international license details.
- Access associated driver and local license information.
- Track issued international license records.

#### 🚔 License Detention Management:

Manages the detention of active local driving licenses and maintains the records associated with each detention operation.

Supported operations:

- Detain active local driving licenses.
- Record detention dates and applicable fines.
- Associate detention records with their corresponding licenses.
- Search and display detained license records.
- Track detention status and identify licenses that have not been released.
- Access detention details and associated license information.

#### 🔓 Detained License Release:

Handles the release of previously detained driving licenses.

Each release operation is connected to its original detention record to preserve the complete history of the detention and release process.

Supported operations:

- Release previously detained driving licenses.
- Create release applications.
- Apply the appropriate release fees.
- Record license release dates.
- Update detention records to reflect their release status.
- Maintain the relationship between detention records and release applications.
- Preserve detention and release history.

### ⚙️ Application and Reference Data:

The Application and Reference Data service manages the configuration data used by different licensing operations.

Instead of duplicating configuration values across application modules, the system maintains reference information that can be shared by related business processes.

Supported operations:

- Manage license classes and their associated requirements.
- Manage application types and their fees.
- Manage test types and their associated fees.
- Configure license validity periods.
- Maintain minimum age requirements.
- Provide centralized reference information for licensing operations.

Centralizing reference data helps maintain consistency when application rules and configuration values are used by multiple modules.

---

## 📄 Pagination and Data Retrieval:

The application uses database-side pagination to manage large result sets without retrieving unnecessary records.

Instead of loading all available records into memory, the system retrieves a limited number of records for each request and loads additional records when required.

### Offset-Based Pagination:

Offset-based pagination retrieves records from a specified position in an ordered result set.

It uses SQL Server's `OFFSET ... FETCH NEXT` functionality to skip previously retrieved records and return the requested batch.

This approach supports navigation through different positions in a result set and is useful when implementing page-based navigation.

### Cursor-Based Pagination:

Cursor-based pagination uses the primary-key value of the last retrieved record to determine where the next batch should begin.

Instead of relying on the number of previously skipped records, the next query retrieves records relative to the last processed identifier.

This approach supports progressive loading and avoids relying on increasingly large offsets.

### Progressive Loading:

The application uses pagination state to determine when and how to request additional records.

The general process is:

1. Retrieve an initial batch from SQL Server.
2. Display the retrieved records in the DataGridView.
3. Maintain the current pagination and sorting state.
4. Detect when additional records are required.
5. Request the next batch using the selected pagination strategy.
6. Append the returned records to the existing result set.

This allows users to work with large database tables without requiring the application to load every record at once.

## 🔎 Server-Side Filtering:

Searching and filtering large database tables directly within the Windows Forms application can require transferring more data than necessary.

DVLD applies filtering and sorting at the database level so that SQL Server processes the requested conditions before returning records to the application.

The process provides several benefits:

- Search conditions are executed in SQL Server.
- Only matching records are retrieved.
- Filtering can be combined with pagination.
- Sorting is performed before the requested batch is returned.
- Unnecessary data transfer is reduced.
- Application memory consumption is controlled.
- Database indexes can support frequently executed filtering and sorting operations.

The Utility Library also provides DataView filtering functionality for operations involving records that are already available in memory. This complements database-side filtering rather than replacing it for large database result sets.

## 🔐 Security and Credential Protection:

The application provides dedicated mechanisms for handling usernames, passwords, and encryption keys.

These operations are centralized through `clsCryptography` to avoid spreading credential-processing logic across different application classes.

### Username Protection:

Usernames are encrypted before being stored in the database.

The encryption process uses AES and a separately generated Initialization Vector (IV) for each encrypted value.

The IV is stored alongside the corresponding encrypted username so that the original value can be recovered when required.

A username hash is also maintained to support exact username lookup without requiring the system to decrypt every stored username during the lookup process.

### Password Protection:

Passwords are hashed before being stored.

The application uses PBKDF2 with SHA-256 to derive a password hash using a generated salt.

The configured implementation uses:

- A 32-byte generated salt.
- 50,000 PBKDF2 iterations.
- A 32-byte derived hash.

This allows password verification without storing the original password.

### Encryption Key Management:

The encryption key is managed through Windows Credential Manager.

Instead of placing the key directly in the source code or storing it in the application database, the system provides operations for storing, retrieving, and deleting it through the Windows credential-management facility.

Keeping encryption and key-management operations centralized makes credential handling easier to maintain and keeps the corresponding implementation separate from business workflows.

## 🛡️ Authorization:

The application uses user permissions to control access to different areas of the system.

Each user can be assigned permissions that determine which features are available to their account.

The supported permission areas include:

- Users Management.
- Drivers View.
- People Management.
- Applications Management.

The application uses these permissions to restrict access to features that a user is not authorized to use.

This provides a structured approach to managing application access rather than giving every account unrestricted access to all operations.

---

## 🧰 Utility Library:

The repository includes a separate class-library project named `Utility Library`, targeting .NET Framework 4.8.

The library was introduced to centralize frequently used operations that support the main DVLD application.

Without a shared library, common operations such as encryption, input validation, formatting, SQL construction, and DataGridView management may need to be implemented repeatedly across different classes and forms.

The Utility Library groups these operations into dedicated classes, each responsible for a particular area of functionality.

This separation allows the main application to focus on licensing workflows and business operations while delegating shared technical tasks to reusable helper classes.

### Library Components:

| Class | Responsibilities |
|---|---|
| `clsCryptography` | Handles password hashing, AES encryption and decryption, initialization vector generation, encryption key management, username lookup hashing, and Windows Credential Manager operations. |
| `clsDataGridViewUtilityLib` | Provides DataGridView management, DataView filtering, row operations, column handling, sorting, pagination state management, and selected column-value handling. |
| `clsSqlQueryBuilder` | Constructs reusable SQL conditions, search expressions, sorting clauses, and pagination fragments. It also provides functionality for executing supplied queries and commands into a DataTable. |
| `clsValidation` | Centralizes input validation, ErrorProvider handling, email validation, numeric validation, name-part validation, AM/PM conversion, and appointment-time validation. |
| `clsFormat` | Provides consistent date, time, decimal, and floating-point formatting, along with DateTimePicker age-range configuration. |
| `clsFileManager` | Handles directory creation, GUID-based file naming, extension preservation, and image copying into the project image directory. |
| `clsUtility` | Provides general-purpose helpers for GUID generation, ComboBox drawing customization, control positioning, and MDI form customization. |

### 🔐 `clsCryptography` — Cryptographic Operations:

`clsCryptography` centralizes the cryptographic operations required by the application.

Its main purpose is to separate encryption, hashing, and encryption-key management from the application forms and business operations.

The class provides different approaches depending on the type of information being protected.

**Password Hashing:**

Passwords are not stored as plaintext. Instead, the application derives a hash using PBKDF2 with SHA-256 and a generated salt.

The implementation uses:

- PBKDF2 with SHA-256.
- A generated 32-byte salt.
- 50,000 iterations.
- A 32-byte derived hash.

The salt allows identical passwords to produce different stored hash values, while the derivation process supports password verification without storing the original password.

**Username Encryption:**

Unlike passwords, usernames need to be recovered during certain authentication operations.

The class therefore provides AES encryption and decryption functionality.

Each encrypted username uses its own generated Initialization Vector (IV), which is stored alongside the encrypted value.

The implementation also supports generating a username hash for exact lookup without requiring the database to decrypt every stored username.

**Encryption Key Management:**

The class provides functionality to:

- Generate encryption keys.
- Encrypt and decrypt strings using AES.
- Generate initialization vectors.
- Create username lookup hashes.
- Store encryption keys in Windows Credential Manager.
- Retrieve previously stored encryption keys.
- Delete stored encryption keys when required.

Keeping these operations in one dedicated class avoids spreading cryptographic implementation details across the application.

### 📊 `clsDataGridViewUtilityLib` — DataGridView Management:

`clsDataGridViewUtilityLib` groups common operations used when displaying and managing tabular data in Windows Forms.

The class reduces the amount of repeated DataGridView-related code required by different application screens.

It also supports the application's progressive loading approach by maintaining information about the current pagination and sorting state.

Its responsibilities include:

**Data Management:**

- Add new rows.
- Update existing rows.
- Remove rows.
- Retrieve column names.
- Work with selected column values.

**Filtering and Sorting:**

- Filter records through DataView.
- Manage column sorting.
- Track sorting information.
- Support consistent table interactions.

**Pagination Management:**

- Track offset pagination values.
- Maintain cursor pagination state.
- Store the last retrieved primary-key value.
- Track the current sorting state.
- Detect when the user reaches the last displayed row.

By centralizing these operations, the class provides shared functionality for forms that display and progressively retrieve database records.

### 🗄️ `clsSqlQueryBuilder` — SQL Query Construction:

`clsSqlQueryBuilder` provides reusable methods for constructing SQL query fragments used by the application's data-access operations.

The purpose of this class is to reduce repeated string construction and provide a common implementation for frequently used SQL conditions.

It supports the construction of:

- `WHERE` conditions for filtering records.
- `LIKE` expressions for text searching.
- `ORDER BY` clauses for sorting.
- Primary-key conditions for cursor-based pagination.
- SQL Server `OFFSET ... FETCH NEXT` clauses for offset-based pagination.

The class also provides functionality for executing supplied queries and commands and returning the results through a `DataTable`.

This component is particularly useful when implementing searchable and paginated database operations.

The data-access layer remains responsible for defining and organizing the database operations used by the main application. The query builder serves as a supporting component for constructing and executing supplied SQL statements.

### ✅ `clsValidation` — Input Validation:

`clsValidation` centralizes common validation operations used throughout the Windows Forms application.

Its purpose is to avoid repeating validation logic across different forms and to provide consistent input-handling behavior.

The class provides functionality for:

- Managing ErrorProvider messages.
- Validating email addresses.
- Validating integer input.
- Validating decimal input.
- Validating name parts.
- Converting AM/PM input.
- Checking whether a selected appointment time is still valid.

Centralizing these methods makes validation easier to reuse and maintain while keeping individual forms focused on their interaction responsibilities.

### 📅 `clsFormat` — Data Formatting:

`clsFormat` provides common formatting methods for values displayed or processed by the application.

Different forms may need to display dates, times, and numeric values using consistent formats.

Instead of defining these formats independently across multiple screens, the class provides shared formatting functionality.

Its responsibilities include:

- Formatting decimal values.
- Formatting floating-point values.
- Providing date format strings.
- Providing time format strings.
- Formatting date values.
- Configuring DateTimePicker minimum and maximum age limits.

This helps maintain consistent data presentation and date-selection behavior throughout the application.

### 📁 `clsFileManager` — File and Image Management:

`clsFileManager` groups the file-system operations required by the application, particularly those related to image storage.

It provides functionality for managing directories and generating unique file names before copying images into the project image folder.

Its responsibilities include:

- Creating directories when they do not exist.
- Generating unique GUID-based file names.
- Preserving original file extensions.
- Copying images into the designated project image directory.

Using GUID-based names helps avoid file-name collisions when multiple images are stored.

### 🖥️ `clsUtility` — General Windows Forms Utilities:

`clsUtility` contains general-purpose helper methods for operations that do not belong to a specific business module.

It provides reusable functionality for customizing and managing Windows Forms controls and interface elements.

Its responsibilities include:

- Generating GUID values.
- Customizing ComboBox drawing.
- Centering controls horizontally.
- Changing MDI background colors.
- Removing the MDI client border.

Keeping these operations in a shared class avoids repeating low-level interface customization code throughout the application.

### Benefits of the Utility Library:

The separation of common operations into a dedicated class library provides several benefits:

- **Code Reusability:** Common operations can be called from different application classes without rewriting their implementations.
- **Separation of Concerns:** Technical helper operations remain separate from licensing business logic and form-specific behavior.
- **Maintainability:** Changes to shared functionality can be managed in one place.
- **Consistency:** Common operations follow the same implementation across the application.
- **Reduced Duplication:** Repeated code is replaced by reusable methods.
- **Extensibility:** Additional helper classes and methods can be introduced without placing unrelated functionality inside the main application.

The Utility Library complements the three-tier architecture. It acts as a supporting component and does not replace the responsibilities of the presentation, business, or data-access layers.

## 🧩 Supporting Utility Library:

The Utility Library is a supporting component of the overall application architecture. It is developed as a separate class library and referenced by the main DVLD application wherever shared functionality is required.

Its purpose is to provide reusable technical operations without introducing additional responsibilities into the three primary application layers.

Rather than representing another layer in the application's processing flow, the library acts as a shared collection of helper methods that can be used by different parts of the solution.

Its relationship with the main architecture can be understood through the following examples:

- **Presentation Layer:** Uses shared validation, formatting, DataGridView, and Windows Forms utility methods to manage user input and interface behavior.
- **Business Layer:** Can use cryptographic and validation functionality when handling authentication-related and domain operations.
- **Data Access Layer:** Can use SQL query construction methods to support filtering, sorting, searching, and pagination operations.

This arrangement keeps shared technical functionality separate from individual application modules while allowing each layer to use the utilities relevant to its responsibilities.

The separation also makes it easier to maintain common operations independently of the main licensing workflow.

---

## 🧰 Tech Stack:

| Area | Technology |
|---|---|
| Programming Language | C# |
| User Interface | Windows Forms |
| Runtime | .NET Framework 4.8 |
| IDE | Visual Studio |
| Database | Microsoft SQL Server |
| Database API | System.Data.SqlClient |
| Data Model | Relational Database |
| Architecture | Three-Tier Architecture |
| Shared Components | C# Class Library |
| Cryptography | AES, PBKDF2, SHA-256 |
| Credential Management | Windows Credential Manager |

---

## 📁 Repository Notes:

The repository contains the source code, project files, database design assets, and Visual Studio project structure required to develop and maintain the application.

The main project components include:

- Windows Forms application files.
- Presentation-layer classes.
- Business-layer classes.
- Data-access classes.
- Utility Library project.
- SQL Server database design assets.
- DVLD Entity Relationship Diagram.
- Relational database schema.
- Visual Studio solution and project files.

### Source Control:

Generated development and build artifacts should be excluded from source control.

Examples include:

```gitignore
.vs/
bin/
obj/
```

The source files, solution files, and project files should remain under version control.

Database credentials, encryption keys, and other sensitive configuration values should not be committed to the repository.

---

## 🤝 Contributing:

Contributions should preserve the existing architectural organization and business rules of the application.

The following practices should be maintained when extending or modifying the system:

- Keep user interface responsibilities within the presentation layer.
- Implement business rules within the business layer.
- Keep SQL queries and persistence operations within the data-access layer.
- Reuse existing utility methods where appropriate.
- Avoid duplicating validation and business logic.
- Maintain database relationships and data integrity.
- Use server-side filtering and pagination for large result sets.
- Document database schema changes.
- Test modifications across the affected licensing workflows.
- Keep shared functionality reusable and independent of individual forms.

### Pull Request Guidelines:

A useful contribution should clearly describe:

- What functionality or component was changed?
- Why was the change necessary?
- Which application modules are affected?
- Were any database or architectural changes introduced?
- What tests were performed to verify the changes?

Contributions should maintain consistency with the existing architecture and avoid introducing unnecessary dependencies between application layers.

---

## 🎯 Project Objectives:

DVLD demonstrates the development of a business-oriented desktop application using C#, Windows Forms, and Microsoft SQL Server.

The project brings together several software engineering concepts within one integrated application:

- Object-oriented programming.
- Three-tier architecture.
- Relational database design.
- Third Normal Form (3NF).
- Business-rule enforcement.
- Authentication and authorization.
- Cryptographic operations and credential management.
- Database-side filtering and pagination.
- Progressive data loading.
- Reusable class libraries.
- Separation of concerns.
- Maintainable application design.

The overall objective is to build a licensing management system in which business operations, database relationships, security mechanisms, and reusable technical components work together within a structured and maintainable architecture.
