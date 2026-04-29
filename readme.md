# 🌓 TodoPhoenix

A modern, full-stack task management application built with **ASP.NET Core MVC**, designed with a clean dark UI and a focus on simplicity, performance, and real-world architecture.

> Organize your work. Rebuild your focus. Rise again.

---

## ✨ Features

### 🔐 Authentication
- User registration & login (ASP.NET Identity)
- Secure password validation
- Session-based authentication

### 📁 Project Management
- Create and delete projects
- Project-based task organization
- Isolated data per user

### ✅ Task Management
- Create, edit, delete tasks
- Toggle completion status
- Assign:
  - Title
  - Description
  - Due date
  - Priority (Low / Medium / High)

### 🔍 Filtering
- View all tasks
- View today’s tasks
- View completed tasks
- Filter tasks by project

### ⚡ UX Enhancements
- Modal-based create/edit (no page reload flow)
- Dynamic updates with Fetch API
- Validation feedback inside modals
- Clean dark-themed UI

---

## 🧱 Tech Stack

| Layer        | Technology |
|-------------|-----------|
| Backend      | ASP.NET Core MVC |
| Frontend     | Razor Views + Bootstrap |
| Database     | PostgreSQL |
| ORM          | Entity Framework Core |
| Auth         | ASP.NET Identity |
| Client-side  | Vanilla JS (Fetch API) |

---

## 🧠 Architecture Overview

- MVC pattern (Controllers / Views / Models)
- Entity Framework for data access
- Identity for authentication & user isolation
- Modular controllers:
  - `ProjectsController`
  - `TasksController`
  - `DashboardController`
- Partial rendering for modal workflows

---

## 🚀 Getting Started

### 1. Clone the repo

```bash
git clone https://github.com/YOUR_USERNAME/TodoPhoenix.git
cd TodoPhoenix
```

---

### 2. Configure Database

Update your connection string in:

`appsettings.json`

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=todophoenix;Username=postgres;Password=yourpassword"
}
```

---

### 3. Run Migrations

```bash
dotnet ef database update
```

---

### 4. Run the app

```bash
dotnet run
```

Visit:

```
http://localhost:xxxx
```

---

## 🔐 Validation & Security

- Required fields enforced via DataAnnotations
- Server-side validation with ModelState
- Identity handles:
  - Duplicate emails
  - Password hashing
  - Login security

---

## 🎨 UI Philosophy

- Minimalist, distraction-free design
- Dark mode for long usage sessions
- Fast interactions (no unnecessary reloads)
- Focus on usability over visual clutter

---

## 📸 Screenshots
<img width="1918" height="917" alt="image" src="https://github.com/user-attachments/assets/217a19f6-fbf4-4dbc-8a36-a83aa1ae69a2" />

<img width="1913" height="919" alt="image" src="https://github.com/user-attachments/assets/07d7f66e-dc7a-4406-98a4-2ad3ed3a5d5f" />

<img width="1900" height="915" alt="image" src="https://github.com/user-attachments/assets/797ac975-a0ee-40df-a54a-5b38de954be3" />

<img width="1906" height="916" alt="image" src="https://github.com/user-attachments/assets/18ad4154-a763-43aa-8da2-db09170fcdfb" />

---

## ⚠️ Deployment Notes

This is a **full-stack ASP.NET application**, which means:

❌ GitHub Pages is NOT supported (static hosting only)

### ✅ Recommended deployment platforms:
- Render
- Railway
- Azure App Service

---

## 📌 Future Improvements

- Drag & drop task ordering
- Notifications / reminders
- API layer (REST or GraphQL)
- Mobile-friendly UI enhancements
- Tagging system

---

## 👤 Author

**Soren Persian**  
GitHub: https://github.com/JavedanCode

---

## 🏁 Final Thoughts

This project started as a learning exercise but evolved into a complete, production-style application covering:

- Authentication
- Database design
- Full CRUD operations
- Client-server interaction
- UX refinement

---

> Built with focus. Refined through debugging. Finished with intention.
