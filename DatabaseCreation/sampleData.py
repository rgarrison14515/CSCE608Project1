import csv
import random
from faker import Faker

# Initialize Faker
fake = Faker()

# Number of records for each table
NUM_STUDENTS = 5000
NUM_FACULTY = 100
NUM_COURSES = 500
NUM_DEPARTMENTS = 10
NUM_TERMS = 5
NUM_MAJORS = 15
NUM_ENROLLMENTS = 10000  # StudentCourse (Many-to-Many)
NUM_STUDENT_MAJORS = 5000  # StudentMajor (Many-to-Many)

# Generate Departments
departments = [{"DepartmentID": i + 1, "Name": fake.company(), "Abbreviation": fake.lexify(text="???")} for i in range(NUM_DEPARTMENTS)]
with open("departments.csv", "w", newline="") as f:
    writer = csv.DictWriter(f, fieldnames=["DepartmentID", "Name", "Abbreviation"])
    writer.writeheader()
    writer.writerows(departments)

# Generate Terms
terms = [{"TermID": i + 1, "TermCode": f"{season} {2025 + (i % 3)}"} for i, season in enumerate(["Spring", "Summer", "Fall", "Winter", "Autumn"])]
with open("terms.csv", "w", newline="") as f:
    writer = csv.DictWriter(f, fieldnames=["TermID", "TermCode"])
    writer.writeheader()
    writer.writerows(terms)

# Generate Majors
majors = [{"MajorID": i + 1, "Name": fake.job(), "DepartmentID": random.randint(1, NUM_DEPARTMENTS)} for i in range(NUM_MAJORS)]
with open("majors.csv", "w", newline="") as f:
    writer = csv.DictWriter(f, fieldnames=["MajorID", "Name", "DepartmentID"])
    writer.writeheader()
    writer.writerows(majors)

# Generate Students (Ensure Unique Emails)
emails = set()
students = []
for i in range(NUM_STUDENTS):
    email = fake.email()
    while email in emails:  # Ensure unique email
        email = fake.email()
    emails.add(email)

    students.append({
        "StudentID": f"S{i+1}",
        "Name": fake.name(),
        "DateOfBirth": fake.date_of_birth(minimum_age=18, maximum_age=30),
        "Email": email,
        "MajorID": random.randint(1, NUM_MAJORS)
    })

with open("students.csv", "w", newline="") as f:
    writer = csv.DictWriter(f, fieldnames=["StudentID", "Name", "DateOfBirth", "Email", "MajorID"])
    writer.writeheader()
    writer.writerows(students)

# Generate Faculty (Ensure Unique Emails)
faculty_emails = set()
faculty = []
for i in range(NUM_FACULTY):
    email = fake.email()
    while email in faculty_emails:  # Ensure unique faculty emails
        email = fake.email()
    faculty_emails.add(email)

    faculty.append({
        "FacultyID": i + 1,
        "Name": fake.name(),
        "Email": email,
        "DepartmentID": random.randint(1, NUM_DEPARTMENTS)
    })

with open("faculty.csv", "w", newline="") as f:
    writer = csv.DictWriter(f, fieldnames=["FacultyID", "Name", "Email", "DepartmentID"])
    writer.writeheader()
    writer.writerows(faculty)

# Generate Courses
courses = [{"CourseID": i + 1, "Title": fake.catch_phrase(), "Credits": random.randint(1, 4),
            "DepartmentID": random.randint(1, NUM_DEPARTMENTS), "FacultyID": random.randint(1, NUM_FACULTY),
            "TermID": random.randint(1, NUM_TERMS)} for i in range(NUM_COURSES)]
with open("courses.csv", "w", newline="") as f:
    writer = csv.DictWriter(f, fieldnames=["CourseID", "Title", "Credits", "DepartmentID", "FacultyID", "TermID"])
    writer.writeheader()
    writer.writerows(courses)

# Generate StudentCourse (Ensure Unique Enrollments)
student_courses = set()
enrollments = []
while len(student_courses) < NUM_ENROLLMENTS:
    sid = f"S{random.randint(1, NUM_STUDENTS)}"
    cid = random.randint(1, NUM_COURSES)

    if (sid, cid) not in student_courses:  # Ensure unique student-course pair
        student_courses.add((sid, cid))
        enrollments.append({"StudentID": sid, "CourseID": cid})

with open("studentcourse.csv", "w", newline="") as f:
    writer = csv.DictWriter(f, fieldnames=["StudentID", "CourseID"])
    writer.writeheader()
    writer.writerows(enrollments)

# Generate StudentMajor (Ensure Unique Assignments)
student_majors_set = set()
student_majors = []
while len(student_majors_set) < NUM_STUDENT_MAJORS:
    sid = f"S{random.randint(1, NUM_STUDENTS)}"
    mid = random.randint(1, NUM_MAJORS)

    if (sid, mid) not in student_majors_set:  # Ensure unique student-major pair
        student_majors_set.add((sid, mid))
        student_majors.append({"StudentID": sid, "MajorID": mid})

with open("studentmajor.csv", "w", newline="") as f:
    writer = csv.DictWriter(f, fieldnames=["StudentID", "MajorID"])
    writer.writeheader()
    writer.writerows(student_majors)

print("✅ CSV files generated successfully!")
