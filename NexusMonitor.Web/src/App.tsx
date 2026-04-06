import React from 'react'
import './App.css'
import { BrowserRouter, Routes, Route, NavLink } from 'react-router-dom';
import Users from "./Users";
import Devices from "./Devices";
import Device from "./Device";

function App() {
    return (
        <BrowserRouter>
            <div className="app-container">
                <header className="app-header">
                    <h1>Nexus Monitor</h1>
                    <nav className="app-nav">
                        <NavLink 
                            to="/users" 
                            className={({ isActive }) => (isActive ? "nav-link active" : "nav-link")}
                        >
                            Users
                        </NavLink>
                        <NavLink 
                            to="/devices" 
                            className={({ isActive }) => (isActive ? "nav-link active" : "nav-link")}
                        >
                            Devices
                        </NavLink>
                    </nav>
                </header>
                
                <main className="app-content">
                    <Routes>
                        <Route path="/users" element={<Users />} />
                        <Route path="/devices" element={<Devices />} />
                        <Route path="/devices/:deviceId" element={<Device />} />
                    </Routes>
                </main>
            </div>
        </BrowserRouter>
    )
};

export default App
