# Gym Management System

Gym management system featuring secure JWT authentication, role-based access control, and unit testing.

## **Client-Server Architecture**

```
Gym App (Monorepo)
├── Backend/           # ASP.NET Core Web API + PostgreSQL
├── Frontend/          # React.js (Vite)
└── Database/          # Migrations & Scripts
```

## ✨ **Key Features**

- **Secure Authentication**: JWT tokens with role-based access (Admin, Trainer, Receptionist) + CORS enabled
- **Member Management**: CRUD operations, membership tracking
- **Class Scheduling**: Group classes with member enrollment
- **Payment Processing**: Track payments and pending dues
- **Reporting Dashboard**: Member stats and revenue reports
- **Unit Testing**: xUnit tests
- **Responsive UI**: React + CSS Modules + React Router

## 🛠️ **Tech Stack**

```
Backend:     ASP.NET Core Web API (C#/.NET 8) **+ CORS**
Database:  PostgreSQL + Entity Framework Core
Auth:       **JWT Bearer Authentication**
Testing:    xUnit 
Frontend:   React + Vite + **TypeScript** + **React Router**
Styling:    **CSS Modules** (Pure CSS)
Deployment: Docker Compose
```

## 📁 **Project Structure**

```
Backend/
├── Controllers/     # Business logic 
├── Services/        # External integrations
├── DTOs/            # Data transfer
├── Repositories/    # Data access layer
├── Models/          # Request Response Models
├── Routes/          # Presentation Layer: Endpoints
└── Tests/           # xUnit unit & integration tests

Frontend/src/
├── pages/           # Dashboard, Members, Classes, Payments
├── components/      # Reusable UI components
├── hooks/           # Custom React hooks (useAuth, useMembers)
├── services/        # API clients (Axios + JWT interceptor)
└── types/           # TypeScript interfaces
```

## 🚀 **API Endpoints**

### Authentication
├── POST /api/auth/login     # → { token, roles: ["Admin", "Trainer", "Receptionist"] }
├── POST /api/auth/register
└── POST /api/auth/refresh

### Members (Role-protected)
├── GET    /api/members                    # Paginated + filters
├── GET    /api/members/{id}
├── POST   /api/members
├── PUT    /api/members/{id}
├── DELETE /api/members/{id}
└── GET    /api/members/{id}/membership    # Estado actual de membresía

### Memberships (Admin/Trainer)
├── GET    /api/memberships                # Todos los planes disponibles
├── GET    /api/memberships/{id}
├── POST   /api/memberships
├── PUT    /api/memberships/{id}
└── DELETE /api/memberships/{id}

### Trainers (Admin only)
├── GET    /api/trainers                   # Lista de trainers
├── POST   /api/trainers
├── PUT    /api/trainers/{id}
└── DELETE /api/trainers/{id}

### Classes
├── GET    /api/classes                    # Available classes
├── GET    /api/classes/{id}
├── POST   /api/classes
├── PUT    /api/classes/{id}
├── DELETE /api/classes/{id}
├── POST   /api/classes/{id}/enroll        # Member enroll
└── DELETE /api/classes/{id}/enroll        # Member unenroll

### Attendance (Receptionist/Trainer)
├── POST /api/attendance/{memberId}/checkin  # Marca asistencia diaria
└── GET  /api/attendance/member/{memberId}   # Historial de asistencias

### Payments
├── POST   /api/payments/register-payment
├── GET    /api/members/{id}/payments
├── GET    /api/payments/pending
└── GET    /api/payments/summary            # Resumen general

### Reports (Admin only)
├── GET /api/reports/members-stats
├── GET /api/reports/revenue
├── GET /api/reports/attendance-stats       # Estadísticas de asistencia
└── GET /api/reports/class-usage            # Uso de clases por member

## 🧪 **Testing Strategy**

- **Backend**: xUnit tests (Controllers, Services, Repositories)
- **Integration**: API endpoint testing with TestServer
- **Frontend**: React Testing Library (planned)

## 🚀 **Quick Start**

```bash
# Clone & Install
git clone https://github.com/Lesathch/Gym-app.git
cd Gym-app

# Backend
cd backend/
dotnet restore
dotnet ef database update
dotnet run

# Frontend  
cd frontend/
npm install
npm run dev

# Docker (One command)
docker-compose up -d
```

## 📋 **Status: MVP in process**
🔄  Authentication & Roles  
🔄 Member CRUD  
🔄 Class Management  
🔄 Payments Tracking  
🔄 Adding Advanced Reporting  

***

**👩‍💻 Built by Raquel Bonilla** | Front End Developer  
**[Portfolio](https://lesathch.com)** | **[LinkedIn](https://www.linkedin.com/in/raquel-bonilla-a02959193/)**

***
