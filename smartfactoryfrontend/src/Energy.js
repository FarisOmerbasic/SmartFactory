import React from "react";
import Menu from "./menu"; 
import "./energy.css";

const Energy = () => {
  return (
    <div className="energy-page">
      <Menu /> {/* Menu component */}
      
      <main className="main-content">
        <h1>Current Energy Usage</h1>

        {/* Status Cards */}
        <div className="status-cards">
          <div className="card">
            <p>Total Power</p>
            <h2>487 kWh</h2>
            <small>12% above average</small>
          </div>
          <div className="card">
            <p>Efficiency Rate</p>
            <h2>92%</h2>
            <small>3% improvement</small>
          </div>
          <div className="card">
            <p>Cost Today</p>
            <h2>€1,245</h2>
            <small>€180 saved vs yesterday</small>
          </div>
        </div>

        {/* Machine Energy Consumption Table */}
        <h2>Machine Energy Consumption</h2>
        <div className="table-container">
          <table className="energy-table">
            <thead>
              <tr>
                <th>Machine</th>
                <th>Consumption</th>
                <th>Status</th>
                <th>Activity</th>
                <th>Change</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>Assembly Line A</td>
                <td>125 kWh</td>
                <td>High</td>
                <td>Active</td>
                <td className="increase">32% ↑</td>
              </tr>
              <tr>
                <td>Packaging Unit B</td>
                <td>98 kWh</td>
                <td>Normal</td>
                <td>Active</td>
                <td className="decrease">5% ↓</td>
              </tr>
              <tr>
                <td>Welding Station C</td>
                <td>156 kWh</td>
                <td>Critical</td>
                <td>Active</td>
                <td className="increase">45% ↑</td>
              </tr>
            </tbody>
          </table>
        </div>

        {/* Optimization Suggestions */}
        <h2>Optimization Suggestions</h2>
        <div className="suggestions">
          <div className="suggestion">
            <p>Reduce Assembly Line A power consumption</p>
            <small>Schedule maintenance check for potential inefficiencies</small>
          </div>
          <div className="suggestion warning">
            <p>Critical alert: Welding Station C</p>
            <small>Immediate inspection required - unusual power spike detected</small>
          </div>
        </div>
      </main>
    </div>
  );
};

export default Energy;
