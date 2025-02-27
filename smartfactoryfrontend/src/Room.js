import React from "react";
import Menu from "./menu"; 
import "./Room.css";

const Room = () => {
  return (
    <div className="dashboard-container">
      <Menu /> 
      <main className="main-content">
        <div className="room-container">
          <h1>Production Room A</h1>

          <div className="cards">
            <div className="card">
              <p>🌡 Temperature</p>
              <h2>24.5°C</h2>
              <small>Normal operating range</small>
            </div>
            <div className="card">
              <p>💧 Humidity</p>
              <h2>45%</h2>
              <small>Within acceptable limits</small>
            </div>
            <div className="card">
              <p>🌬 Air Quality</p>
              <h2>Good</h2>
              <small>CO2: 650 ppm</small>
            </div>
            <div className="card">
              <p>🔊 Noise Level</p>
              <h2>68 dB</h2>
              <small>Normal production noise</small>
            </div>
          </div>

          <h2>Sensors</h2>
          <table className="system-health">
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
                <td className="success">Active</td>
                <td>2 min ago</td>
              </tr>
              <tr>
                <td>📡 Humidity Sensor</td>
                <td>45%</td>
                <td className="success">Active</td>
                <td>2 min ago</td>
              </tr>
              <tr>
                <td>📡 Air Quality Sensor</td>
                <td>650 ppm</td>
                <td className="success">Active</td>
                <td>3 min ago</td>
              </tr>
              <tr>
                <td>📡 Noise Level Sensor</td>
                <td>68 dB</td>
                <td className="success">Active</td>
                <td>1 min ago</td>
              </tr>
              <tr>
                <td>📡 Motion Sensor</td>
                <td>No motion</td>
                <td className="success">Active</td>
                <td>5 min ago</td>
              </tr>
            </tbody>
          </table>

          <h2>Machines</h2>
          <div className="cards">
            <div className="card">
              <p>⚙ Assembly Line A1</p>
              <h2>92% Efficiency</h2>
              <small>Operating normally</small>
            </div>
            <div className="card">
              <p>📦 Packaging Unit P3</p>
              <h2>87% Efficiency</h2>
              <small>Running smoothly</small>
            </div>
            <div className="card warning">
              <p>🔍 Quality Control Scanner</p>
              <h2>Idle</h2>
              <small>Scheduled maintenance</small>
            </div>
            <div className="card">
              <p>🤖 Robotic Arm R2</p>
              <h2>95% Efficiency</h2>
              <small>Operating at peak performance</small>
            </div>
          </div>

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
