import React from "react";
import "./menu.css";
import { Link } from "react-router-dom";

import logo from "./assets/logo512.png";
import dashboardIcon from "./assets/MenuImgs/dashboard.png";
import machinesIcon from "./assets/MenuImgs/machines.png";
import energyIcon from "./assets/MenuImgs/energy.png";
import maintenanceIcon from "./assets/MenuImgs/maintenance.png";
import productionIcon from "./assets/MenuImgs/production.png";
import adminUserIcon from "./assets/MenuImgs/AdminUser.png";

const Menu = () => {
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
                </ul>
            </nav>
            <div className="admin-info">
                <img src={adminUserIcon} alt="Admin User" className="admin-avatar" />
                <p>Admin User</p>
                <span>System Administrator</span>
            </div>
        </aside>
    );
};

export default Menu;
