import React from "react";
import "./Room.css";

const Room = () => {
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
        <div className="room-container">
          {/* Room Header */}
          <h1>Production Room A</h1>

          {/* Sensor Overview */}
          <div className="sensor-overview">
            <div className="sensor-card">
              <h3>🌡 TEMPERATURE</h3>
              <h2>24.5°C</h2>
              <p>Normal operating range</p>
            </div>
            <div className="sensor-card">
              <h3>💧 HUMIDITY</h3>
              <h2>45%</h2>
              <p>Within acceptable limits</p>
            </div>
            <div className="sensor-card">
              <h3>🌬 AIR QUALITY</h3>
              <h2>Good</h2>
              <p>CO2: 650 ppm</p>
            </div>
            <div className="sensor-card">
              <h3>🔊 NOISE LEVEL</h3>
              <h2>68 dB</h2>
              <p>Normal production noise</p>
            </div>
          </div>

          {/* Sensors List */}
          <h2>Sensors</h2>
          <table className="sensor-table">
            <thead>
              <tr>
                <th>Sensor</th>
                <th>Value</th>
                <th>Status</th>
                <th>Last Updated</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>📡 Temperature Sensor</td>
                <td>24.5°C</td>
                <td>Active</td>
                <td>2 min ago</td>
              </tr>
              <tr>
                <td>📡 Humidity Sensor</td>
                <td>45%</td>
                <td>Active</td>
                <td>2 min ago</td>
              </tr>
              <tr>
                <td>📡 Air Quality Sensor</td>
                <td>650 ppm</td>
                <td>Active</td>
                <td>3 min ago</td>
              </tr>
              <tr>
                <td>📡 Noise Level Sensor</td>
                <td>68 dB</td>
                <td>Active</td>
                <td>1 min ago</td>
              </tr>
              <tr>
                <td>📡 Motion Sensor</td>
                <td>No motion</td>
                <td>Active</td>
                <td>5 min ago</td>
              </tr>
            </tbody>
          </table>

          {/* Machines List */}
          <h2>Machines</h2>
          <div className="machines-list">
            <div className="machine-card">
              <h3>⚙ Assembly Line A1</h3>
              <p>Operating at 92% efficiency</p>
            </div>
            <div className="machine-card">
              <h3>📦 Packaging Unit P3</h3>
              <p>Operating at 87% efficiency</p>
            </div>
            <div className="machine-card">
              <h3>🔍 Quality Control Scanner</h3>
              <p>Idle - Scheduled maintenance</p>
            </div>
            <div className="machine-card">
              <h3>🤖 Robotic Arm R2</h3>
              <p>Operating at 95% efficiency</p>
            </div>
          </div>

          {/* Buttons */}
          <div className="button-container">
            <button className="add-sensor">Add Sensor</button>
            <button className="add-machine">Add Machine</button>
          </div>
        </div>
      </main>
    </div>
  );
};

export default Room;
