import React, { useState } from 'react';

interface Course {
  courseID: number;
  title: string;
  credits: number;
  departmentID: number | null;
  facultyID: number | null;
  termID: number | null;
}

export default function CoursesByFaculty() {
  const [facultyName, setFacultyName] = useState('');
  const [courses, setCourses] = useState<Course[] | null>(null);
  const [error, setError] = useState<string | null>(null);

  const handleSearch = async () => {
    try {
      const response = await fetch(`https://localhost:7117/api/Course/faculty/name/${encodeURIComponent(facultyName)}`);
      if (!response.ok) {
        throw new Error(`No courses found for faculty: ${facultyName}`);
      }
      const data = await response.json();
      setCourses(data);
      setError(null);
    } catch (err: any) {
      setCourses(null);
      setError(err.message || 'An error occurred');
    }
  };

  return (
    <div style={{ padding: '2rem' }}>
      <h2>Courses Taught by Faculty</h2>

      <input
        type="text"
        value={facultyName}
        onChange={(e) => setFacultyName(e.target.value)}
        placeholder="Enter faculty name"
        style={{ padding: '0.5rem', marginRight: '1rem' }}
      />
      <button onClick={handleSearch}>Search</button>

      {error && <p style={{ color: 'red', marginTop: '1rem' }}>{error}</p>}

      {courses && (
        <div style={{ marginTop: '2rem' }}>
          <h3>Courses:</h3>
          <ul>
            {courses.map((course) => (
              <li key={course.courseID}>
                {course.title} (Credits: {course.credits})
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}
