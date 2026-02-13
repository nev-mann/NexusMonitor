import { useEffect, useState } from 'react'
import './App.css'

function App() {

    const [data, setData] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetch('/api/device/')
            .then(response => response.json())
            .then(json => {
                setData(json);
            })
            .catch(error => {
                console.error('B³¹d:', error);
                setError(error.message);
            });
    }, []);

    return (
        <>
            <h1>Nexus Monitor</h1>
            <div className="card">
                {error && <p>Error: {error}</p>}
                {!error && (
                    <ul>
                        {Array.isArray(data) && data.map((item, index) => (
                            <li key={index}>
                                {JSON.stringify(item)}
                            </li>
                        ))}
                    </ul>
                )}
            </div>
        </>
    )
}

export default App