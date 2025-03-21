import React, { useState } from 'react';

interface Faculty {
  facultyID: number;
  name: string;
  email: string;
  departmentID: number | null;
}

export default function FacultyByDepartment() {
  const [departmentName, setDepartmentName] = useState('');
  const [facultyList, setFacultyList] = useState<Faculty[] | null>(null);
  const [error, setError] = useState<string | null>(null);

  const handleSearch = async () => {
    try {
      const response = await fetch(`https://localhost:7117/api/Faculty/department/name/${encodeURIComponent(departmentName)}`);
      if (!response.ok) {
        throw new Error(`No faculty members found for department: ${departmentName}`);
      }
      const data = await response.json();
      setFacultyList(data);
      setError(null);
    } catch (err: any) {
      setFacultyList(null);
      setError(err.message || 'An error occurred');
    }
  };

  return (
    <div style={{ padding: '2rem' }}>
      <h2>Faculty in a Department</h2>

      <input
        type="text"
        value={departmentName}
        onChange={(e) => setDepartmentName(e.target.value)}
        placeholder="Enter department name"
        style={{ padding: '0.5rem', marginRight: '1rem' }}
      />
      <button onClick={handleSearch}>Search</button>

      {error && <p style={{ color: 'red', marginTop: '1rem' }}>{error}</p>}

      {facultyList && (
        <div style={{ marginTop: '2rem' }}>
          <h3>Faculty Members:</h3>
          <ul>
            {facultyList.map((faculty) => (
              <li key={faculty.facultyID}>
                {faculty.name} ({faculty.email})
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}
