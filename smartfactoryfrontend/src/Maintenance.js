import React, { useEffect, useState } from "react";
import Menu from "./menu"; 
import "./Maintenance.css";

const alerts = [
  { priority: "High", issue: "Bearing Wear", description: "CNC Machine #3 - Estimated 48h until critical.", action: "Schedule Service" },
  { priority: "Medium", issue: "Motor Temperature", description: "Assembly Line B motor temperature trending above normal.", action: "View Details" },
  { priority: "Low", issue: "Belt Tension", description: "Conveyor #2 belt tension below optimal.", action: "Add to Schedule" }
];

const Maintenance = () => {
  const [scheduledMaintenance, setScheduledMaintenance] = useState([]);

  useEffect(() => {
    const fetchScheduledMaintenance = async () => {
      try {
        const response = await fetch("http://localhost:5270/api/ScheduledMaintenance/ScheduledMaintenance");
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const data = await response.json();
        setScheduledMaintenance(data.scheduled);
      } catch (error) {
        console.error("Error fetching scheduled maintenance:", error);
      }
    };

    fetchScheduledMaintenance();
  }, []);

  return (
    <div className="dashboard-container">
      <Menu /> 
      <main className="main-content">
        <h1>Maintenance Overview</h1>

        <h2>Critical Alerts</h2>
        <div className="cards">
          {alerts.map((alert, index) => (
            <div key={index} className={`card ${alert.priority.toLowerCase()}-priority`}>
              <p>{alert.priority} Priority</p>
              <h2>{alert.issue}</h2>
              <small>{alert.description}</small>
              <button className={`${alert.priority.toLowerCase()}-btn`}>{alert.action}</button>
            </div>
          ))}
        </div>

        <h2>Scheduled Maintenance</h2>
        <div className="scheduled-list">
          {scheduledMaintenance.length > 0 ? (
            scheduledMaintenance.map((item, index) => (
              <div key={index} className="scheduled-item">
                <h3>{item.machine} - {item.task}</h3>
                <p>{item.scheduledTime} - Expected duration: {item.expectedDuration}</p>
              </div>
            ))
          ) : (
            <p>Loading scheduled maintenance...</p>
          )}
        </div>
      </main>
    </div>
  );
};

export default Maintenance;