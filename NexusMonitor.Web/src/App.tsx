import React from 'react'
import './App.css'
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import Users from "./Users";
import Devices from "./Devices";
import Device from "./Device";

function App() {

    return (
        <BrowserRouter>
            <h1>Nexus Monitor</h1>
            <Link to="/users">About</Link>
            <Link to="/devices">Devices</Link>
            
            <Routes>
                <Route path="/users" element={<Users />} />
                <Route path="/devices" element={<Devices />} />
                <Route path="/devices/:deviceId" element={<Device />} />
            </Routes>
        </BrowserRouter>
    )
};

export default App
