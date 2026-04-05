import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import styles from './Devices.module.css'; 

function Devices() {
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

    const [devices, setData] = useState<Device[]>([]);
    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        setIsLoading(true);
        fetch('/api/device/')
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then(json => {
                setData(json);
                setIsLoading(false);
            })
            .catch(error => {
                console.error('Error fetching devices:', error);
                setError(error.message);
                setIsLoading(false);
            });
    }, []);

    return (
        <div className={styles.container}>
            <div className={styles.card}>
                <h2 className={styles.header}>Device Registry</h2>

                {error && (
                    <div className={styles.error}>
                        <strong>Error loading devices:</strong> {error}
                    </div>
                )}

                {!error && isLoading && (
                    <div className={styles.message}>Loading devices...</div>
                )}

                {!error && !isLoading && devices.length === 0 && (
                    <div className={styles.message}>No devices found.</div>
                )}

                {!error && !isLoading && devices.length > 0 && (
                    <table className={styles.table}>
                        <thead>
                            <tr>
                                <th className={styles.th}>ID</th>
                                <th className={styles.th}>Name</th>
                                <th className={styles.th}>Type</th>
                                <th className={styles.th}>Date Registered</th>
                                <th className={styles.th}>Thresholds (H/L)</th>
                                <th className={styles.th}>Unit</th>
                            </tr>
                        </thead>
                        <tbody>
                            {devices.map((device) => (
                                <tr key={device.deviceId}>
                                    <td className={styles.td}>
                                        <span className={styles.textMuted}>#{device.deviceId}</span>
                                    </td>
                                    <td className={styles.tdBold}>
                                        <Link to={`/devices/${device.deviceId}`} className={styles.link}>
                                            {device.deviceName}
                                        </Link>
                                    </td>
                                    <td className={styles.td}>
                                        <span className={styles.badge}>{device.deviceType}</span>
                                    </td>
                                    <td className={styles.td}>
                                        {new Date(device.dateRegistered).toLocaleDateString()}
                                    </td>
                                    <td className={styles.td}>
                                        <span className={styles.textRed}>{device.highThreshold}</span>
                                        <span className={styles.slash}>/</span>
                                        <span className={styles.textBlue}>{device.lowThreshold}</span>
                                    </td>
                                    <td className={styles.td}>
                                        {device.measurementsUnit ? (
                                            <span className={styles.unitBadge}>
                                                {device.measurementsUnit}
                                            </span>
                                        ) : (
                                            <i className={styles.textMuted}>N/A</i>
                                        )}
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                )}
            </div>
        </div>
    );
}

export default Devices;