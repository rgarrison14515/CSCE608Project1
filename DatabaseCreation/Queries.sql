-- Students who have taken a specific course
SELECT S.StudentID, S.Name, C.CourseID, C.Title
FROM Student S
JOIN StudentCourse SC ON S.StudentID = SC.StudentID
JOIN Course C ON SC.CourseID = C.CourseID
WHERE C.CourseID = 10;  -- Change CourseID as needed


--Students in a specific major
SELECT S.StudentID, S.Name, M.Name AS MajorName
FROM Student S
JOIN Major M ON S.MajorID = M.MajorID
WHERE M.MajorID = 3;  -- Change MajorID as needed


--Retrieve courses taught by specific faculty member
SELECT C.CourseID, C.Title, F.Name AS FacultyName
FROM Course C
JOIN Faculty F ON C.FacultyID = F.FacultyID
WHERE F.FacultyID = 5;  -- Change FacultyID as needed

--Display faculty members associated with a department
SELECT F.FacultyID, F.Name, D.Name AS DepartmentName
FROM Faculty F
JOIN Department D ON F.DepartmentID = D.DepartmentID
WHERE D.DepartmentID = 2;  -- Change DepartmentID as needed


--List all courses offered in a specific academic term
SELECT C.CourseID, C.Title, T.TermCode
FROM Course C
JOIN Term T ON C.TermID = T.TermID
WHERE T.TermCode = 'Spring 2025';  -- Change TermCode as needed
