import React from "react";
import "./Machine.css";
import { AlertTriangle, CircleCheck, CircleAlert, PauseCircle } from "lucide-react";

const machines = [
  { name: "CNC Machine #1", status: "Running", efficiency: "98%", uptime: "8.5 hrs", temp: "Normal temp" },
  { name: "CNC Machine #2", status: "Running", efficiency: "95%", uptime: "12.2 hrs", temp: "Normal temp" },
  { name: "CNC Machine #3", status: "Idle", efficiency: "0%", uptime: "0 hrs", temp: "Standby" },
  { name: "CNC Machine #4", status: "Warning", efficiency: "45%", uptime: "2.3 hrs", temp: "High temp" }
];

const Machine = () => {
  return (
    <div className="dashboard-container">
      {/* Sidebar */}
      <aside className="sidebar">
        <h2>SmartFactory</h2>
        <nav>
          <h3>Dashboard</h3>
          <ul>
            <li>Users</li>
            <li>Analytics</li>
            <li>Settings</li>
          </ul>
          <h3>Production</h3>
          <ul>
            <li>Machines</li>
            <li>Energy</li>
            <li>Maintenance</li>
          </ul>
        </nav>
        <div className="admin-info">
          <p>Admin User</p>
          <span>System Administrator</span>
        </div>
      </aside>

      {/* Main content */}
      <main className="main-content">
        <h2 className="title">Machine Status Overview</h2>
        <div className="status-cards">
          <div className="card">
            <CircleCheck className="icon green" size={24} />
            <h3>Running</h3>
            <p>18</p>
          </div>
          <div className="card">
            <CircleAlert className="icon yellow" size={24} />
            <h3>Warning</h3>
            <p>3</p>
          </div>
          <div className="card">
            <AlertTriangle className="icon red" size={24} />
            <h3>Critical</h3>
            <p>1</p>
          </div>
          <div className="card">
            <PauseCircle className="icon gray" size={24} />
            <h3>Idle</h3>
            <p>4</p>
          </div>
        </div>
        
        <h3 className="subtitle">Critical Alerts</h3>
        <div className="alerts">
          <p><strong>CNC Machine #4 - Overheating:</strong> Temperature exceeded 85°C - Immediate attention required</p>
          <p><strong>Assembly Line B - Excessive Vibration:</strong> Vibration levels 40% above normal threshold</p>
        </div>
        
        <h3 className="subtitle">Machine Status Details</h3>
        <table className="machine-table">
          <thead>
            <tr>
              <th>Machine</th>
              <th>Status</th>
              <th>Efficiency</th>
              <th>Uptime</th>
              <th>Temperature</th>
            </tr>
          </thead>
          <tbody>
            {machines.map((machine, index) => (
              <tr key={index}>
                <td>{machine.name}</td>
                <td>{machine.status}</td>
                <td>{machine.efficiency}</td>
                <td>{machine.uptime}</td>
                <td>{machine.temp}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </main>
    </div>
  );
};

export default Machine;
