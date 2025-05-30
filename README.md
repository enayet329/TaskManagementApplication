# Task Management Web Application

A comprehensive task management web application built with .NET Core 9, Entity Framework Core, and Bootstrap 5.

## Features

- **User Authentication**: Secure registration and login using ASP.NET Core Identity
- **Task Management**: Create, read, update, and delete tasks
- **Task Status**: Mark tasks as completed or pending
- **Filtering**: Filter tasks by completion status (All, Pending, Completed)
- **Sorting**: Sort tasks by due date, title, or creation date
- **Due Date Tracking**: Visual indicators for overdue and due-soon tasks
- **Responsive Design**: Mobile-friendly interface using Bootstrap 5
- **User Isolation**: Each user can only see and manage their own tasks

## Technology Stack

- **Backend**: .NET Core 9.0
- **Database**: SQL Server LocalDB with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Frontend**: Razor Views with Bootstrap 5
- **Icons**: Font Awesome 6
- **Validation**: Client-side and server-side validation

## Setup Instructions

1. **Prerequisites**
   - .NET 9.0 SDK or later
   - Visual Studio 2022 or VS Code
   - SQL Server LocalDB (included with Visual Studio)

2. **Clone/Download the Project**
   ```bash
   git clone <repository-url>
   cd TaskManagementApp
   ```

3. **Install Dependencies**
   ```bash
   dotnet restore
   ```

4. **Database Setup**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```

6. **Access the Application**
   - Open browser and navigate to `https://localhost:5001` or `http://localhost:5000`
   - Register a new account or use existing credentials

## Project Structure

```
TaskManagementApp/
├── Controllers/
│   ├── HomeController.cs
│   └── TasksController.cs
├── Models/
│   ├── TaskItem.cs
│   └── TaskViewModel.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Views/
│   ├── Home/
│   ├── Tasks/
│   └── Shared/
├── Areas/Identity/Pages/Account/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── lib/
└── Program.cs
```

## Database Schema

### TaskItem Table
- `Id` (Primary Key)
- `UserId` (Foreign Key to AspNetUsers)
- `Title` (Required, Max 200 chars)
- `Description` (Optional, Max 1000 chars)
- `IsCompleted` (Boolean)
- `DueDate` (Optional DateTime)
- `CreatedAt` (DateTime)

## Key Features Implementation

### Authentication & Authorization
- Uses ASP.NET Core Identity for user management
- Password requirements: minimum 6 characters, at least 1 digit
- Secure user isolation using UserId filtering

### Task Management
- Full CRUD operations for tasks
- Real-time task status toggling
- Bulk operations support
- Form validation on both client and server side

### Filtering & Sorting
- Status-based filtering (All, Pending, Completed)
- Multiple sorting options (Due Date, Title, Created Date)
- Auto-submit filters for better UX

### UI/UX Features
- Responsive design for mobile and desktop
- Visual indicators for task status and due dates
- Statistics dashboard showing task counts
- Confirmation dialogs for destructive actions
- Auto-dismissing success/error messages

## Security Features

- CSRF protection on all forms
- User session management
- SQL injection prevention via EF Core
- XSS protection via Razor encoding
- Secure password hashing via Identity

## Known Issues & Limitations

- No email confirmation for registration (disabled for simplicity)
- No forgot password email functionality (requires SMTP configuration)
- No task categories or tags
- No task priority levels
- No file attachments for tasks

