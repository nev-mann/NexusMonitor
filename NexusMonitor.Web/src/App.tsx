import React, { useEffect, useState } from 'react'
import './App.css'
interface Device {
    deviceId: number;
    deviceName: string;
    dateRegistered: string;
    measurements: any[];
    measurementsUnit: string | null;
    deviceType: string;
    highThreshold: number;
    lowThreshold: number;
}
function App() {

    const [devices, setData] = useState<Device[]>([]);
    const [error, setError] = useState(null);    

    useEffect(() => {
        fetch('http://localhost:5210/api/device/')
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
                    <div style={{ padding: '20px', fontFamily: 'sans-serif' }}>
                        <h2>Device Registry</h2>
                        <table style={{ width: '100%', borderCollapse: 'collapse', marginTop: '10px' }}>
                            <thead>
                                <tr style={{ textAlign: 'left' }}>
                                    <th>ID</th>
                                    <th>Name</th>
                                    <th>Type</th>
                                    <th>Date Registered</th>
                                    <th>Thresholds (H/L)</th>
                                    <th>Unit</th>
                                </tr>
                            </thead>
                            <tbody>
                                {devices.map((device) => (
                                            <tr key={device.deviceId} style={{ borderBottom: '1px solid #ddd' }}>
                                                <td>{device.deviceId}</td>
                                                <td>**{device.deviceName}**</td>
                                                <td>{device.deviceType}</td>
                                                <td>{device.dateRegistered}</td>
                                                <td>
                                                    {device.highThreshold} / {device.lowThreshold}
                                                </td>
                                                <td>
                                                    {device.measurementsUnit || <i style={{ color: '#999' }}>N/A</i>}
                                                </td>
                                            </tr>
                                        ))
                                    }
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </>
    )
};

export default App
