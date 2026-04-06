import React, { useEffect, useState } from 'react';
import { Link, useParams } from 'react-router-dom';
import styles from './Device.module.css';

interface Measurement {
    timestamp: string;
    value: number;
}

interface DeviceData {
    deviceId: number;
    deviceName: string;
    dateRegistered: string;
    measurements: Measurement[];
    measurementsUnit: string | null;
    deviceType: string;
    highThreshold: number;
    lowThreshold: number;
}

function Device() {
    const [device, setDevice] = useState<DeviceData | null>(null);
    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    
    // States for editing
    const [isEditing, setIsEditing] = useState<boolean>(false);
    const [editForm, setEditForm] = useState<Partial<DeviceData>>({});

    const { deviceId } = useParams<{ deviceId?: string }>();

    useEffect(() => {
        setIsLoading(true);
        fetch(`/api/device/${deviceId}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then((json: DeviceData) => {
                setDevice(json);
                setEditForm({
                    deviceName: json.deviceName,
                    highThreshold: json.highThreshold,
                    lowThreshold: json.lowThreshold,
                    measurementsUnit: json.measurementsUnit
                });
                setIsLoading(false);
            })
            .catch(error => {
                console.error('Error fetching device:', error);
                setError(error.message);
                setIsLoading(false);
            });
    }, [deviceId]);

    const handleEdit = () => {
        setIsEditing(true);
    };

    const handleCancel = () => {
        // Reset the form values to the current device values
        if (device) {
            setEditForm({
                deviceName: device.deviceName,
                highThreshold: device.highThreshold,
                lowThreshold: device.lowThreshold,
                measurementsUnit: device.measurementsUnit
            });
        }
        setIsEditing(false);
    };

    const handleSave = () => {
        // TODO: Implement the save functionality here
        // e.g., fetch(`/api/device/${deviceId}`, { method: 'PUT', body: JSON.stringify(editForm) })
        
        console.log('Saving new values:', editForm);
        
        // After saving, you would typically update the local device state and exit edit mode:
        setIsEditing(false);
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setEditForm(prev => ({
            ...prev,
            [name]: name === 'highThreshold' || name === 'lowThreshold' ? Number(value) : value
        }));
    };

    if (isLoading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;
    if (!device) return <div>No device found.</div>;

    return (
        <div className={styles.container}>
            <h2>Device ID: {deviceId}</h2>

            <div className={styles.deviceParameters}>
                {isEditing ? (
                    <div className={styles.editForm}>
                        <p>
                            <label>Device Name: </label>
                            <input 
                                type="text" 
                                name="deviceName" 
                                value={editForm.deviceName || ''} 
                                onChange={handleChange} 
                            />
                        </p>
                        <p>
                            <label>High Threshold: </label>
                            <input 
                                type="number" 
                                name="highThreshold" 
                                value={editForm.highThreshold ?? ''} 
                                onChange={handleChange} 
                            />
                        </p>
                        <p>
                            <label>Low Threshold: </label>
                            <input 
                                type="number" 
                                name="lowThreshold" 
                                value={editForm.lowThreshold ?? ''} 
                                onChange={handleChange} 
                            />
                        </p>
                        <p>
                            <label>Unit: </label>
                            <input 
                                type="text" 
                                name="measurementsUnit" 
                                value={editForm.measurementsUnit || ''} 
                                onChange={handleChange} 
                            />
                        </p>
                        <div>
                            <button onClick={handleSave}>Save</button>
                            <button onClick={handleCancel}>Cancel</button>
                        </div>
                    </div>
                ) : (
                    <div className={styles.deviceInfo}>
                        <p><strong>Name:</strong> {device.deviceName}</p>
                        <p><strong>Type:</strong> {device.deviceType}</p>
                        <p><strong>Registered:</strong> {new Date(device.dateRegistered).toLocaleString()}</p>
                        <p><strong>High Threshold:</strong> {device.highThreshold}</p>
                        <p><strong>Low Threshold:</strong> {device.lowThreshold}</p>
                        <p><strong>Unit:</strong> {device.measurementsUnit}</p>
                        <button onClick={handleEdit}>Edit</button>
                    </div>
                )}
            </div>

            <hr />

            <h3>Measurements</h3>
            <table className={styles.measurementsTable} border={1} cellPadding={8} style={{ borderCollapse: 'collapse' }}>
                <thead>
                    <tr>
                        <th>Timestamp</th>
                        <th>Value</th>
                    </tr>
                </thead>
                <tbody>
                    {device.measurements && device.measurements.length > 0 ? (
                        device.measurements.map((measurement, index) => (
                            <tr key={index}>
                                <td>{new Date(measurement.timestamp).toLocaleString()}</td>
                                <td>{measurement.value}</td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan={2} style={{ textAlign: 'center' }}>No measurements recorded</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
};

export default Device;