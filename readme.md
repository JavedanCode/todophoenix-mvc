# TodoPhoenix

TodoPhoenix is a modern task management web application built with ASP.NET Core MVC.  
The project allows users to create projects, manage tasks, organize priorities, and track progress through a responsive dashboard interface.

This project was developed as a university ASP.NET Core MVC course project and demonstrates authentication, CRUD operations, Entity Framework Core, SQLite integration, MVC architecture, validation, responsive UI design, and secure user workflows.

---

## Features

### Authentication
- User registration
- User login
- Secure logout
- Protected routes using ASP.NET Identity
- Anti-forgery token protection

### Project Management
- Create projects
- Delete projects
- Prevent duplicate project names per user
- User-specific project ownership

### Task Management
- Create tasks
- Edit tasks
- Delete tasks
- Toggle task completion
- Priority system (Low / Medium / High)
- Due dates
- Task descriptions

### Dashboard
- Dynamic dashboard interface
- Sidebar navigation
- Project-based filtering
- Task filtering:
  - All Tasks
  - Today's Tasks
  - Completed Tasks
- Active page highlighting
- Dynamic page titles

### Profile System
- User profile page
- Task statistics
- Project statistics
- Account overview

### UI / UX
- Responsive Bootstrap layout
- Modern dark theme
- Styled modal system
- Priority indicators
- Sidebar navigation
- GitHub footer integration

---

## Technologies Used

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- SQLite
- ASP.NET Identity
- Bootstrap 5
- Razor Views
- LINQ
- JavaScript Fetch API

---

## Architecture

The project follows the MVC (Model-View-Controller) architecture:

### Models
- `Project`
- `TaskItem`
- `ApplicationDbContext`

### Views
- Razor views for authentication, dashboard, profile, and task management

### Controllers
- `AccountController`
- `DashboardController`
- `ProjectsController`
- `TasksController`
- `ProfileController`
- `HomeController`

---

## Database Relationships

### One-to-Many Relationships

- One user → many projects
- One project → many tasks

Each authenticated user only has access to their own projects and tasks.

---

## Security Features

- ASP.NET Identity authentication
- Authorization protection using `[Authorize]`
- Anti-forgery token validation
- User ownership validation for all CRUD operations
- Duplicate prevention for:
  - Project names
  - User emails

---

## Screenshots
<img width="1917" height="915" alt="Login" src="https://github.com/user-attachments/assets/c56753fe-e3bf-46de-bf34-28722eacd97f" />

---
<img width="1919" height="915" alt="Register" src="https://github.com/user-attachments/assets/241546cc-6262-4157-b3be-f528dd3eef0b" />

---
<img width="1902" height="916" alt="Dashboard" src="https://github.com/user-attachments/assets/492c4d22-18b2-43de-a1c9-f0fca1d36b4a" />

---
<img width="1919" height="915" alt="Profile" src="https://github.com/user-attachments/assets/32537eda-8fe6-4022-8f4f-b915b3b84988" />

---
<img width="1917" height="913" alt="About" src="https://github.com/user-attachments/assets/1131d24d-0ec7-4f1c-9b9e-4a89d28a6e0b" />

---
<img width="1919" height="911" alt="Edit" src="https://github.com/user-attachments/assets/5acfaf2f-e580-4f4a-bcae-b406aed1423f" />

---
### Dashboard
- Dynamic task dashboard
- Priority badges
- Filtering system
- Responsive sidebar

### Profile Page
- User statistics
- Account information

### Authentication
- Login page
- Register page

---

## Installation

### 1. Clone the Repository

```bash
git clone https://github.com/JavedanCode/todophoenix-mvc.git
```

---

### 2. Navigate to the Project

```bash
cd todophoenix-mvc
```

---

### 3. Restore Packages

```bash
dotnet restore
```

---

### 4. Apply Migrations

```bash
dotnet ef database update
```

---

### 5. Run the Project

```bash
dotnet run
```

---

## Default Database

The project uses SQLite with the following database file:

```plaintext
TodoPhoenix.db
```

Connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=TodoPhoenix.db"
}
```

---

## Course Requirements Covered

This project demonstrates:

- ASP.NET Core MVC
- Entity Framework Core
- SQLite Database
- Code First Migration
- Authentication & Authorization
- CRUD Operations
- Model Relationships
- ViewModels
- Validation
- Responsive Bootstrap UI
- Secure Routing
- Dashboard Interface
- Dynamic Filtering
- Modern Web Application Structure

---

## Future Improvements

Potential future improvements include:

- Task search functionality
- Drag-and-drop task organization
- Task categories/tags
- Notifications/reminders
- Profile image uploads
- Deployment support

---

## Author

Developed by:

**JavedanCode**

GitHub:  
https://github.com/JavedanCode

---

## License

This project was created for educational purposes.
