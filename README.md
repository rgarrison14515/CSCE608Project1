# Student Academic Records System

This is a full-stack database application for managing student academic records, including student information, courses, majors, departments, faculty, and academic terms. It features a Microsoft SQL Server backend, an ASP.NET Core Web API for data access, and a React + TypeScript frontend for user interaction.

## Technologies Used

- Backend: ASP.NET Core Web API (C#), Entity Framework Core
- Frontend: React with TypeScript
- Database: Microsoft SQL Server
- Data Generation: Python with Faker
- Testing Tools: Swagger, Postman

## Prerequisites

- .NET SDK 6.0 or higher
- Node.js (v18 or higher)
- Microsoft SQL Server
- SQL Server Management Studio (recommended)

## Setup Instructions

### Step 1: Database Setup

1. Open SQL Server Management Studio (SSMS) and create a database named `StudentRecordsDB`.
2. Run the `schema.sql` file to create the tables.
3. Run the `insert_data.sql` script to bulk load data using the provided CSV files.
4. Optionally, run `generate_data.py` to regenerate new sample data.

### Step 2: Backend Setup

1. Navigate to the backend project folder:
   ```
   cd StudentRecordsSolution/StudentRecordsAPI
   ```
2. In `appsettings.json`, configure your database connection string:
   ```
   "DefaultConnection": "Server=localhost;Database=StudentRecordsDB;Trusted_Connection=True;"
   ```
3. Start the backend:
   ```
   dotnet restore
   dotnet run
   ```
4. The backend will be available at `https://localhost:7117`, and Swagger UI will be at `/swagger/index.html`.

### Step 3: Frontend Setup

1. Navigate to the frontend project directory:
   ```
   cd student-records-frontend/student-records-frontend
   ```
2. Install dependencies and start the development server:
   ```
   npm install
   npm start
   ```
3. The app will run on `http://localhost:3000` and communicate with the backend.

## Features

The system provides a simple UI with six main query functions:

- Find students by course name
- Find students by major name
- View courses taught by a faculty member
- View faculty by department
- View courses by academic term
- View students with multiple majors

Each feature allows user input, communicates with the backend API, and displays results cleanly. The Quit option leads to a final screen prompting users to close the tab manually.

## Testing

To test backend endpoints directly, use:

- Swagger UI: `https://localhost:7117/swagger/index.html`
- Postman or any REST client

## Notes

- Primary and foreign keys are enforced in the schema.
- Generated data maintains referential integrity.
- Query implementation includes joins and aggregates in SQL.
