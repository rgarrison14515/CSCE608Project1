import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

// Import your pages using the actual filenames
import Home from './pages/Home';
import StudentsByCourse from './pages/StudentsByCourse';
import StudentsByMajor from './pages/StudentsByMajor';
import CoursesByFaculty from './pages/CoursesByFaculty';
import FacultyByDepartment from './pages/FacultyByDepartment';
import CoursesByTerm from './pages/CoursesByTerm';
import MultiMajorStudents from './pages/MultiMajorStudents';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/query1" element={<StudentsByCourse />} />
        <Route path="/query2" element={<StudentsByMajor />} />
        <Route path="/query3" element={<CoursesByFaculty />} />
        <Route path="/query4" element={<FacultyByDepartment />} />
        <Route path="/query5" element={<CoursesByTerm />} />
        <Route path="/query6" element={<MultiMajorStudents />} />
        <Route path="/quit" element={<div style={{ textAlign: 'center', padding: '2rem' }}><h2>You can now close the tab.</h2></div>} />
      </Routes>
    </Router>
  );
}

export default App;
