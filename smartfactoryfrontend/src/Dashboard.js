import React from "react";
import Menu from "./menu";
import "./Dashboard.css";

const Dashboard = () => {
    return (
        <div className="dashboard-container">
            <Menu />
            <main className="main-content">
                <h1>Overview</h1>
                
                {/* Cards */}
                <div className="cards">
                    <div className="card">
                        <p>Production Rate</p>
                        <h2>98.5%</h2>
                        <small>2% increase from last week</small>
                    </div>
                    <div className="card">
                        <p>Energy Efficiency</p>
                        <h2>85%</h2>
                        <small>Carbon neutral operations</small>
                    </div>
                    <div className="card">
                        <p>Alerts</p>
                        <h2>3 Active</h2>
                        <small>2 high priority</small>
                    </div>
                    <div className="card">
                        <p>Maintenance</p>
                        <h2>4 Scheduled</h2>
                        <small>Next in 2 days</small>
                    </div>
                </div>

                {/* Recent Activities */}
                <h2>Recent Activities</h2>
                <ul className="recent-activities">
                    <li>
                        <strong>System Update Completed</strong>
                        <p>All machines updated to version 2.4.0</p>
                    </li>
                    <li>
                        <strong>Energy Optimization</strong>
                        <p>AI recommendations implemented</p>
                    </li>
                    <li>
                        <strong>New User Added</strong>
                        <p>Sarah Chen - Production Manager</p>
                    </li>
                </ul>

                {/* System Health */}
                <h2>System Health</h2>
                <table className="system-health">
                    <thead>
                        <tr>
                            <th>Component</th>
                            <th>Status</th>
                            <th>Uptime</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Database</td>
                            <td>Connected</td>
                            <td>99.9%</td>
                        </tr>
                        <tr>
                            <td>API Services</td>
                            <td>Operational</td>
                            <td>99.8%</td>
                        </tr>
                        <tr>
                            <td>Machine Learning</td>
                            <td>Training</td>
                            <td>95%</td>
                        </tr>
                    </tbody>
                </table>
            </main>
        </div>
    );
};

export default Dashboard;
