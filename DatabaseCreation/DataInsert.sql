-- Import Departments
BULK INSERT Department
FROM 'C:\Users\rgarr\CSCE608\departments.csv'
WITH (FORMAT='CSV', FIRSTROW=2, FIELDTERMINATOR=',', ROWTERMINATOR='\n', TABLOCK);
GO

-- Import Terms
BULK INSERT Term
FROM 'C:\Users\rgarr\CSCE608\terms.csv'
WITH (FORMAT='CSV', FIRSTROW=2, FIELDTERMINATOR=',', ROWTERMINATOR='\n', TABLOCK);
GO

-- Import Majors
BULK INSERT Major
FROM 'C:\Users\rgarr\CSCE608\majors.csv'
WITH (FORMAT='CSV', FIRSTROW=2, FIELDTERMINATOR=',', ROWTERMINATOR='\n', TABLOCK);
GO

-- Import Students
BULK INSERT Student
FROM 'C:\Users\rgarr\CSCE608\students.csv'
WITH (FORMAT='CSV', FIRSTROW=2, FIELDTERMINATOR=',', ROWTERMINATOR='\n', TABLOCK);
GO

-- Import Faculty
BULK INSERT Faculty
FROM 'C:\Users\rgarr\CSCE608\faculty.csv'
WITH (FORMAT='CSV', FIRSTROW=2, FIELDTERMINATOR=',', ROWTERMINATOR='\n', TABLOCK);
GO

-- Import Courses
BULK INSERT Course
FROM 'C:\Users\rgarr\CSCE608\courses.csv'
WITH (FORMAT='CSV', FIRSTROW=2, FIELDTERMINATOR=',', ROWTERMINATOR='\n', TABLOCK);
GO

-- Import StudentCourse (Enrollments)
BULK INSERT StudentCourse
FROM 'C:\Users\rgarr\CSCE608\studentcourse.csv'
WITH (FORMAT='CSV', FIRSTROW=2, FIELDTERMINATOR=',', ROWTERMINATOR='\n', TABLOCK);
GO

-- Import StudentMajor (Students assigned to Majors)
BULK INSERT StudentMajor
FROM 'C:\Users\rgarr\CSCE608\studentmajor.csv'
WITH (FORMAT='CSV', FIRSTROW=2, FIELDTERMINATOR=',', ROWTERMINATOR='\n', TABLOCK);
GO
