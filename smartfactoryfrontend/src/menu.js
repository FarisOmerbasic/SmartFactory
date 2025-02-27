import React from "react";
import { Link, useNavigate } from "react-router-dom"; 
import "./menu.css";

import logo from "./assets/logo512.png";
import dashboardIcon from "./assets/MenuImgs/dashboard.png";
import machinesIcon from "./assets/MenuImgs/machines.png";
import energyIcon from "./assets/MenuImgs/energy.png";
import maintenanceIcon from "./assets/MenuImgs/maintenance.png";
import productionIcon from "./assets/MenuImgs/production.png";
import roomIcon from "./assets/MenuImgs/room.png";
import adminUserIcon from "./assets/MenuImgs/AdminUser.png";
import logoutIcon from "./assets/MenuImgs/logout.png"; 

const Menu = () => {
    const navigate = useNavigate(); 

    // Get the logged-in user from localStorage
    const user = JSON.parse(localStorage.getItem("user"));

    const handleLogout = () => {
        localStorage.removeItem("user"); // Remove user data
        navigate("/login"); // Redirect to login page
    };

    return (
        <aside className="menu">
            {/* Logo */}
            <div className="logo-container">
                <img src={logo} alt="SmartFactory Logo" className="logo" />
            </div>
            <h2>SmartFactory</h2>
            <nav>
                <h3>Dashboard</h3>
                <ul>
                    <li><Link to="/dashboard"><img src={dashboardIcon} alt="Dashboard" className="menu-icon" /> Dashboard</Link></li>
                </ul>
                <h3>Production</h3>
                <ul>
                    <li><Link to="/machine"><img src={machinesIcon} alt="Machine" className="menu-icon" /> Machine</Link></li>
                    <li><Link to="/energy"><img src={energyIcon} alt="Energy" className="menu-icon" /> Energy</Link></li>
                    <li><Link to="/maintenance"><img src={maintenanceIcon} alt="Maintenance" className="menu-icon" /> Maintenance</Link></li>
                    <li><Link to="/production"><img src={productionIcon} alt="Production" className="menu-icon" /> Production</Link></li>
                    <li><Link to="/room"><img src={roomIcon} alt="Room" className="menu-icon" /> Room</Link></li>
                </ul>
            </nav>
            
            <div className="admin-info">
                <img src={adminUserIcon} alt="Admin User" className="admin-avatar" />
                <p>{user?.firstName} {user?.lastName}</p> {/* user name */}
                <span>{user?.role || "Unknown Role"}</span> {/*  user role */}
            </div>

            <button className="logout-btn" onClick={handleLogout}>
                <img src={logoutIcon} alt="Logout" className="menu-icon" /> Logout
            </button>
        </aside>
    );
};

export default Menu;
