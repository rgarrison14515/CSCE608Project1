-- Ensure you are using the correct database
USE StudentRecordsDB;
GO

-- Create the Student table
CREATE TABLE Student (
    StudentID VARCHAR(10) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    MajorID INT,
    FOREIGN KEY (MajorID) REFERENCES Major(MajorID)
);
GO

-- Create the Faculty table
CREATE TABLE Faculty (
    FacultyID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    DepartmentID INT,
    FOREIGN KEY (DepartmentID) REFERENCES Department(DepartmentID)
);
GO

-- Create the Department table
CREATE TABLE Department (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(255) NOT NULL,
    Abbreviation VARCHAR(10) UNIQUE NOT NULL
);
GO

-- Create the Term table
CREATE TABLE Term (
    TermID INT PRIMARY KEY IDENTITY(1,1),
    TermCode VARCHAR(20) UNIQUE NOT NULL
);
GO

-- Create the Major table
CREATE TABLE Major (
    MajorID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(255) NOT NULL,
    DepartmentID INT,
    FOREIGN KEY (DepartmentID) REFERENCES Department(DepartmentID)
);
GO

-- Create the Course table
CREATE TABLE Course (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    Title VARCHAR(255) NOT NULL,
    Credits INT NOT NULL,
    DepartmentID INT,
    FacultyID INT,
    TermID INT,
    FOREIGN KEY (DepartmentID) REFERENCES Department(DepartmentID),
    FOREIGN KEY (FacultyID) REFERENCES Faculty(FacultyID),
    FOREIGN KEY (TermID) REFERENCES Term(TermID)
);
GO

-- Create the StudentCourse junction table (Many-to-Many: Student ↔ Course)
CREATE TABLE StudentCourse (
    StudentID VARCHAR(10),
    CourseID INT,
    PRIMARY KEY (StudentID, CourseID),
    FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID)
);
GO

-- Create the StudentMajor junction table (Many-to-Many: Student ↔ Major)
CREATE TABLE StudentMajor (
    StudentID VARCHAR(10),
    MajorID INT,
    PRIMARY KEY (StudentID, MajorID),
    FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    FOREIGN KEY (MajorID) REFERENCES Major(MajorID)
);
GO


SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE';

SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'dbo';