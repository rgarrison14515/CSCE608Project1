import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

interface StudentWithMajors {
  studentID: string;
  name: string;
  email: string;
  majors: string[];
}

export default function MultiMajorStudents() {
  const [students, setStudents] = useState<StudentWithMajors[] | null>(null);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const fetchStudents = async () => {
    try {
      const response = await fetch('https://localhost:7117/api/Student/multiple-majors');
      if (!response.ok) {
        throw new Error('No students with multiple majors found.');
      }
      const data = await response.json();
      setStudents(data);
      setError(null);
    } catch (err: any) {
      setError(err.message || 'An error occurred.');
      setStudents(null);
    }
  };

  return (
    <div style={{ padding: '2rem', position: 'relative' }}>
      <button onClick={() => navigate('/')} style={{ position: 'absolute', top: '1rem', right: '1rem' }}>
        Home
      </button>

      <h2>Students with Multiple Majors</h2>
      <button onClick={fetchStudents}>Fetch Students</button>

      {error && <p style={{ color: 'red', marginTop: '1rem' }}>{error}</p>}

      {students && (
        <div style={{ marginTop: '2rem' }}>
          <h3>Results:</h3>
          <ul>
            {students.map((student) => (
              <li key={student.studentID} style={{ marginBottom: '1rem' }}>
                <strong>{student.name}</strong> ({student.email})<br />
                Majors: {student.majors.join(', ')}
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}
