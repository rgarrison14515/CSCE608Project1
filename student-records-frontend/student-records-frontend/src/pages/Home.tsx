//Landing Page
import { useNavigate } from 'react-router-dom';

export default function Home() {
    const navigate = useNavigate();

    return (
        <div style={{ padding: '2rem', textAlign: 'center' }}>
            <h1>Student Records Interface</h1>
            <p>Select a query to run:</p>

            <div style={{ display: 'flex', flexDirection: 'column', gap: '1rem', alignItems: 'center', marginTop: '2rem' }}>
                <button onClick={() => navigate('/query1')}>Find students by course</button>
                <button onClick={() => navigate('/query2')}>Find students in a major</button>
                <button onClick={() => navigate('/query3')}>Courses by faculty</button>
                <button onClick={() => navigate('/query4')}>Faculty in a department</button>
                <button onClick={() => navigate('/query5')}>Courses in a term</button>
                <button onClick={() => navigate('/query6')}>Students with multiple majors</button>
                <button onClick={() => navigate('/quit')}>Quit</button>
            </div>
        </div>
    );
}
