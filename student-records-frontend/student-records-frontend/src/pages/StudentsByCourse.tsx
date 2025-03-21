import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

interface Student {
  studentID: string;
  name: string;
  email: string;
  majorID: number | null;
}

export default function StudentsByCourse() {
  const [courseName, setCourseName] = useState('');
  const [students, setStudents] = useState<Student[]>([]);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleSearch = async () => {
    setLoading(true);
    setError('');
    setStudents([]);

    try {
      const response = await fetch(`https://localhost:7117/api/Student/course/name/${encodeURIComponent(courseName)}`);
      if (!response.ok) {
        throw new Error(`No students found for course: ${courseName}`);
      }
      const data = await response.json();
      setStudents(data);
    } catch (err: any) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ padding: '2rem', position: 'relative' }}>
      <button onClick={() => navigate('/')} style={{ position: 'absolute', top: '1rem', right: '1rem' }}>
        Home
      </button>

      <h2>Find Students by Course</h2>

      <div style={{ marginBottom: '1rem' }}>
        <input
          type="text"
          value={courseName}
          placeholder="Enter course name (e.g. Intro to Databases)"
          onChange={(e) => setCourseName(e.target.value)}
          style={{ padding: '0.5rem', width: '300px' }}
        />
        <button onClick={handleSearch} style={{ marginLeft: '1rem', padding: '0.5rem 1rem' }}>
          Search
        </button>
      </div>

      {loading && <p>Loading...</p>}
      {error && <p style={{ color: 'red' }}>{error}</p>}

      {students.length > 0 && (
        <table border={1} cellPadding={8}>
          <thead>
            <tr>
              <th>StudentID</th>
              <th>Name</th>
              <th>Email</th>
              <th>MajorID</th>
            </tr>
          </thead>
          <tbody>
            {students.map((student) => (
              <tr key={student.studentID}>
                <td>{student.studentID}</td>
                <td>{student.name}</td>
                <td>{student.email}</td>
                <td>{student.majorID ?? 'N/A'}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}
