import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

interface Student {
  studentID: string;
  name: string;
  email: string;
  majorID?: number;
}

export default function StudentsByMajor() {
  const [majorName, setMajorName] = useState('');
  const [students, setStudents] = useState<Student[] | null>(null);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const fetchStudents = async () => {
    try {
      const response = await fetch(`https://localhost:7117/api/Student/major/name/${majorName}`);
      if (!response.ok) {
        throw new Error('No students found for this major.');
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

      <h2>Find Students by Major</h2>
      <input
        type="text"
        placeholder="Enter major name (e.g., Computer Science)"
        value={majorName}
        onChange={(e) => setMajorName(e.target.value)}
        style={{ marginRight: '1rem', padding: '0.5rem', width: '300px' }}
      />
      <button onClick={fetchStudents}>Search</button>

      {error && <p style={{ color: 'red', marginTop: '1rem' }}>{error}</p>}

      {students && (
        <div style={{ marginTop: '2rem' }}>
          <h3>Results:</h3>
          <ul>
            {students.map((student) => (
              <li key={student.studentID}>
                <strong>{student.name}</strong> ({student.email})
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}
