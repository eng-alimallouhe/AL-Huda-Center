# Library Management System

The **Library Management System** is a fully integrated platform designed to facilitate product sales, file printing services, and managed delivery operations between customers and the center.
It offers a wide range of advanced features, including human resources management, inventory control, user administration, and comprehensive reporting tools.
The system aims to optimize operational workflows, enhance service delivery, and provide an efficient and seamless user experience.

## Main Features:

- **Product Sales Management**: Manage product listings, pricing, discounts, and sales transactions with ease.
- **File Printing Services**: Allow customers to upload and print files efficiently through the system.
- **Delivery Management**: Handle the delivery of products to and from the center with status tracking and notifications.
- **Human Resources Management**: Manage employee information, salaries, attendance records, performance evaluations, and training programs.
- **Inventory Management**: Track stock levels, receive low-stock alerts, manage suppliers, and maintain inventory logs.
- **User and Role Management**: Administer user accounts, roles, permissions, and access control throughout the system.
- **Reporting and Analytics**: Generate detailed reports on sales, inventory, HR performance, and customer activities.
- **Notification and Communication System**: Send automated alerts and updates to users regarding orders, deliveries, and internal events.

## Technologies Used

### Backend:

- **.NET 9**
- **ASP.NET Core Web API**
- **Entity FrameWork Core** (EF Core)
- **SQL Server** (Database)

### Frontend:

- Angular 19
- Typescript
- HTML5
- CSS3

## How to Run the Project

- Follow these steps to set up and run the Library Management System locally:

### 1. Clone the repository:

- First, clone the project to your local machine:

```bash
git clone https://github.com/eng-alimallouhe/Huda-Center.git
```

### 2. Set Up the Database:

- Before running the project, you must create the database and insert the initial data.
- Open SQL Server Management Studio (SSMS) or your preferred SQL tool.
- Run the provided SQL scripts located in the Database/ folder

### 3. Backend Setup:

- Navigate to the backend project (LMS Folder)

```bash
cd LMS/
```

- Install the required dependencies:

```bash
dotnet restore
```

- Configure your appsettings.json file if necessary (e.g., connection string to your SQL Server).
- then run the backend:

```bash
dotnet run
```

- By defulat the API will be avaliable at:

```bash
https://localhost:5001
```

- or:

```bash
http://localhost:5001
```

### Frontend Set up:

- Navigate to the UI project folder:

```bash
cd LMS/LMS.UI
```

- Install the Angular project dependencies:

```bash
npm install
```

- Run the Angular development server:

```bash
ng serve
```

or:

```bash
npm start
```

- The frontend will usually be available at:

```bash
http://localhost:4200
```

### Important Notes:

- Ensure that your SQL Server is running and accessible.
- Make sure your backend and frontend are using matching URLs and ports for API communication
- Ensure you have .NET 9 and Angular 19.x.x or higher
- Update the environment configurations in Angular if your API URL is different.

## Project Structure:

- The project is organized into three main directories and follows a clean, layered architecture:

### Root Directory Structure:

```bash
/Huda_Center
│
├── LMS/                   # Complete solution (Backend & Frontend)
├── Database/              # SQL scripts for database creation and seeding
│
├── .gitignore             # Git ignored files configuration
├── CONVENTION.md          # Code conventions and naming guidelines for developers
├── LICENSE                # Project license (e.g., MIT)
├── README.md              # Project documentation
```

### Layered Architecture (Inside LMS/):

- The LMS/ directory follows the Clean Architecture pattern and includes four main layers:

```bash
LMS/
│
├── Domain/                # Core business logic and entities
├── Infrastructure/        # EF Core, data access, external service integrations
├── API/                   # API controllers
├── Application /          # application logic, DTOs, services, validators
└── UI/                    # Angular frontend (user interface)
```

#### Layer Descriptions:

- Domain Layer
  - Core domain models, interfaces, and business rules.
- Infrastructure Layer
  - Database context, repositories, and external service implementations.
- Application Layer
  - Handles API endpoints, service logic, data transfer objects, and request validation.
- API Layer
  - API controllers
- UI Layer
  - Developed with Angular 19, handles all client-side logic and API interactions.

## License

This project is licensed under the [MIT License](./LICENSE).  
You are free to use, modify, and distribute this software for personal or commercial purposes, as long as you include the original copyright and license.
