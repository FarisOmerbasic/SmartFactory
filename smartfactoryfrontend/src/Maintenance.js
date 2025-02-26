import React from "react";
import "./Maintenance.css";

const Maintenance = () => {
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
  
        {/* Critical Alerts */}
        <h2>Critical Alerts</h2>
        <div className="alert-cards">
          <div className="alert-card high-priority">
            <h3>High Priority</h3>
            <h2>Bearing Wear</h2>
            <p>CNC Machine #3 showing early signs of bearing failure. Estimated 48h until critical.</p>
            <button className="schedule-btn">Schedule Service</button>
          </div>
          <div className="alert-card medium-priority">
            <h3>Medium Priority</h3>
            <h2>Motor Temperature</h2>
            <p>Assembly Line B motor temperature trending above normal. Maintenance recommended within 5 days.</p>
            <button className="details-btn">View Details</button>
          </div>
          <div className="alert-card low-priority">
            <h3>Low Priority</h3>
            <h2>Belt Tension</h2>
            <p>Conveyor #2 belt tension below optimal. Schedule routine check during next maintenance window.</p>
            <button className="add-btn">Add to Schedule</button>
          </div>
        </div>
  
        {/* Maintenance History */}
        <h2>Maintenance History</h2>
        <table className="history-table">
          <thead>
            <tr>
              <th>Machine</th>
              <th>Maintenance</th>
              <th>Cost</th>
              <th>Downtime</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>🔧 CNC Machine #2</td>
              <td>Bearing Replacement</td>
              <td>$2,400</td>
              <td>12h Downtime</td>
              <td>Completed</td>
            </tr>
            <tr>
              <td>🔧 Assembly Line A</td>
              <td>Motor Overhaul</td>
              <td>$3,800</td>
              <td>24h Downtime</td>
              <td>Completed</td>
            </tr>
            <tr>
              <td>🔧 Conveyor #1</td>
              <td>Belt Replacement</td>
              <td>$1,200</td>
              <td>6h Downtime</td>
              <td>Completed</td>
            </tr>
          </tbody>
        </table>
  
        {/* Scheduled Maintenance */}
        <h2>Scheduled Maintenance</h2>
        <div className="scheduled-list">
          <div className="scheduled-item">
            <h3>CNC Machine #3 Bearing Service</h3>
            <p>Scheduled for Tomorrow, 8:00 AM - Expected duration: 4h</p>
          </div>
          <div className="scheduled-item">
            <h3>Assembly Line B Motor Check</h3>
            <p>Scheduled for Next Week, Tuesday - Expected duration: 2h</p>
          </div>
          <div className="scheduled-item">
            <h3>Conveyor #2 Belt Inspection</h3>
            <p>Scheduled for Next Week, Friday - Expected duration: 1h</p>
          </div>
        </div>
      </main>
    </div>
  );
};

export default Maintenance;
