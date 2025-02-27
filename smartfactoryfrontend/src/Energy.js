import React, { useEffect, useState } from "react";
import "./energy.css";
import Menu from "./menu";

const Energy = () => {
  const [energyData, setEnergyData] = useState({
    totalPower: 0,
    efficiencyRate: 0,
    totalCost: 0,
  });

  const [suggestions, setSuggestions] = useState([]);

  const fetchDevices = async () => {
    try {
      const response = await fetch("http://localhost:5270/api/Energy/CalculateTotalPower");
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }
      const data = await response.json();

      setEnergyData({
        totalPower: data.totalPower || 0,
        efficiencyRate: data.efficiencyRate || 0,
        totalCost: data.totalCost || 0,
      });
    } catch (error) {
      console.error("Error fetching energy data:", error);
    }
  };

  const fetchDeviceStatus = async () => {
    try {
      const response = await fetch("http://localhost:5270/api/Device/GetAllDevices");
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }
      const devices = await response.json();

      let newSuggestions = [];
      let allNormal = true;

      devices.forEach((device) => {
        if (device.currentThreshold === "Warning") {
          allNormal = false;
          newSuggestions.push({
            message: `Schedule maintenance check for ${device.name}.`,
            type: "warning",
          });
        } else if (device.currentThreshold === "Critical") {
          allNormal = false;
          newSuggestions.push({
            message: `Immediate inspection required for ${device.name} - unusual power spike detected!`,
            type: "critical",
          });
        }
      });

      if (allNormal) {
        newSuggestions = [{ message: "All devices are in good condition!", type: "normal" }];
      }

      setSuggestions(newSuggestions);
    } catch (error) {
      console.error("Error fetching device statuses:", error);
    }
  };

  useEffect(() => {
    fetchDevices();
    fetchDeviceStatus();
    const interval = setInterval(() => {
      fetchDevices();
      fetchDeviceStatus();
    }, 60000); // Osvežavanje podataka svakih 60 sekundi
    return () => clearInterval(interval); // Čišćenje intervala pri unmount-u
  }, []);

  return (
    <div className="energy-page">
      <Menu /> 
      <main className="main-content">
        <h1>Current Energy Usage</h1>
        <div className="status-cards">
          <div className="card">
            <p>Total Power</p>
            <h2>{energyData.totalPower} kWh</h2>
            <small>Updated in real-time</small>
          </div>
          <div className="card">
            <p>Efficiency Rate</p>
            <h2>{energyData.efficiencyRate}%</h2>
            <small>Efficiency level</small>
          </div>
          <div className="card">
            <p>Cost Today</p>
            <h2>€{energyData.totalCost.toFixed(2)}</h2>
            <small>Daily energy cost</small>
          </div>
        </div>

        <h2>Machine Energy Consumption</h2>
        <table className="energy-table">
          <thead>
            <tr>
              <th>Machine</th>
              <th>Consumption</th>
              <th>Status</th>
              <th>Activity</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>Assembly Line A</td>
              <td>125 kWh</td>
              <td>High</td>
              <td>Active</td>
            </tr>
            <tr>
              <td>Packaging Unit B</td>
              <td>98 kWh</td>
              <td>Normal</td>
              <td>Active</td>
            </tr>
            <tr>
              <td>Welding Station C</td>
              <td>156 kWh</td>
              <td>Critical</td>
              <td>Active</td>
            </tr>
          </tbody>
        </table>

        <h2>Optimization Suggestions</h2>
        <div className="suggestions">
          {suggestions.map((suggestion, index) => (
            <div key={index} className={`suggestion ${suggestion.type}`}>
              <p>{suggestion.message}</p>
            </div>
          ))}
        </div>
      </main>
    </div>
  );
};

export default Energy;