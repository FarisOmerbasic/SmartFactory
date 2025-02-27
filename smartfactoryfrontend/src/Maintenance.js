import React from "react";
import Menu from "./menu"; 
import "./Maintenance.css";

const alerts = [
  { priority: "High", issue: "Bearing Wear", description: "CNC Machine #3 - Estimated 48h until critical.", action: "Schedule Service" },
  { priority: "Medium", issue: "Motor Temperature", description: "Assembly Line B motor temperature trending above normal.", action: "View Details" },
  { priority: "Low", issue: "Belt Tension", description: "Conveyor #2 belt tension below optimal.", action: "Add to Schedule" }
];

const maintenanceHistory = [
  { machine: "🔧 CNC Machine #2", task: "Bearing Replacement", cost: "$2,400", downtime: "12h Downtime", status: "Completed" },
  { machine: "🔧 Assembly Line A", task: "Motor Overhaul", cost: "$3,800", downtime: "24h Downtime", status: "Completed" },
  { machine: "🔧 Conveyor #1", task: "Belt Replacement", cost: "$1,200", downtime: "6h Downtime", status: "Completed" }
];

const scheduledMaintenance = [
  { title: "CNC Machine #3 Bearing Service", schedule: "Tomorrow, 8:00 AM - Expected duration: 4h" },
  { title: "Assembly Line B Motor Check", schedule: "Next Week, Tuesday - Expected duration: 2h" },
  { title: "Conveyor #2 Belt Inspection", schedule: "Next Week, Friday - Expected duration: 1h" }
];

const Maintenance = () => {
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

        <h2>Maintenance History</h2>
        <div className="table-container">
          <table className="system-health">
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
              {maintenanceHistory.map((record, index) => (
                <tr key={index}>
                  <td>{record.machine}</td>
                  <td>{record.task}</td>
                  <td>{record.cost}</td>
                  <td>{record.downtime}</td>
                  <td className="completed">{record.status}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>

        <h2>Scheduled Maintenance</h2>
        <div className="scheduled-list">
          {scheduledMaintenance.map((item, index) => (
            <div key={index} className="scheduled-item">
              <h3>{item.title}</h3>
              <p>{item.schedule}</p>
            </div>
          ))}
        </div>
      </main>
    </div>
  );
};

export default Maintenance;
