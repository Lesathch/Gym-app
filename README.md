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
- **Payment Processing**: Track payments and pending dues (Not in this version, I will be updating it.)
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
## 🚀 API Endpoints

### 🔐 Authentication

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/auth/login` | Works for Admin, Receptionist and Trainer — returns `{ token, role }` |
| `POST` | `/api/auth/refresh` | Refresh access token |

> ⚠️ The initial Admin account is seeded directly in the database. No public register endpoint is exposed.

---

### 👥 Members *(Admin / Receptionist)*

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/members` | Get all members — paginated + filters |
| `GET` | `/api/members/{id}` | Get member by ID |
| `POST` | `/api/members` | Admin / Receptionist creates a member account |
| `PUT` | `/api/members/{id}` | Update member |
| `DELETE` | `/api/members/{id}` | Delete member |
| `GET` | `/api/members/{id}/membership` | Current membership status |

---

### 💳 Memberships *(Admin / Receptionist)*

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/memberships` | Get all available plans |
| `GET` | `/api/memberships/{id}` | Get membership by ID |
| `POST` | `/api/memberships` | Admin / Receptionist creates a membership plan |
| `PUT` | `/api/memberships/{id}` | Update membership plan |
| `DELETE` | `/api/memberships/{id}` | Delete membership plan |

---

### 🏋️ Trainers *(Admin only)*

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/trainers` | Get all trainers |
| `POST` | `/api/trainers` | Admin creates a trainer account |
| `PUT` | `/api/trainers/{id}` | Update trainer |
| `DELETE` | `/api/trainers/{id}` | Delete trainer |

---

### 🗂️ Receptionists *(Admin only)*

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/receptionists` | Get all receptionists |
| `POST` | `/api/receptionists` | Admin creates a receptionist account |
| `PUT` | `/api/receptionists/{id}` | Update receptionist |
| `DELETE` | `/api/receptionists/{id}` | Delete receptionist |

---

### 📅 Classes *(Admin / Receptionist)*

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/classes` | Get all available classes |
| `GET` | `/api/classes/{id}` | Get class by ID |
| `POST` | `/api/classes` | Create a new class |
| `PUT` | `/api/classes/{id}` | Update class |
| `DELETE` | `/api/classes/{id}` | Delete class |
| `POST` | `/api/classes/{id}/enroll` | Enroll a member |
| `DELETE` | `/api/classes/{id}/enroll` | Unenroll a member |

---

### 📋 Attendance *(Receptionist / Trainer)*

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/attendance/{memberId}/checkin` | Mark daily attendance |
| `GET` | `/api/attendance/member/{memberId}` | Get attendance history |

---

### 💰 Payments *(Not implemented in this version)*

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/payments/register-payment` | Register a payment |
| `GET` | `/api/members/{id}/payments` | Get payments by member |
| `GET` | `/api/payments/pending` | Get pending payments |
| `GET` | `/api/payments/summary` | General payments summary |

---

### 📊 Reports *(Admin only)*

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/reports/members-stats` | Member statistics |
| `GET` | `/api/reports/revenue` | Revenue report |
| `GET` | `/api/reports/attendance-stats` | Attendance statistics |
| `GET` | `/api/reports/class-usage` | Class usage per member |


## 🧪 Testing Strategy

| Layer | Tool | Scope |
|-------|------|-------|
| **Backend** | xUnit | Controllers, Services, Repositories |
| **Integration** | TestServer | API endpoint testing |
| **Frontend** | React Testing Library | *(planned)* |


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
