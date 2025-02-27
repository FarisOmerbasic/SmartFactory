import React from "react";
import Menu from "./menu"; 
import "./Machine.css";
import { AlertTriangle, CircleCheck, CircleAlert, PauseCircle } from "lucide-react";

const machines = [
  { name: "CNC Machine #1", status: "Running", efficiency: "98%", uptime: "8.5 hrs", temp: "Normal temp" },
  { name: "CNC Machine #2", status: "Running", efficiency: "95%", uptime: "12.2 hrs", temp: "Normal temp" },
  { name: "CNC Machine #3", status: "Idle", efficiency: "0%", uptime: "0 hrs", temp: "Standby" },
  { name: "CNC Machine #4", status: "Warning", efficiency: "45%", uptime: "2.3 hrs", temp: "High temp" }
];

const statusCount = {
  Running: machines.filter(m => m.status === "Running").length,
  Warning: machines.filter(m => m.status === "Warning").length,
  Critical: machines.filter(m => m.status === "Critical").length,
  Idle: machines.filter(m => m.status === "Idle").length
};

const Machine = () => {
  return (
    <div className="dashboard-container">
      <Menu /> 
      <main className="main-content">
        <h1 color="darkblue">Machine Status Overview</h1>

        <div className="cards">
          <div className="card-run">
            <CircleCheck className="icon green" size={24} />
            <p>Running</p>
            <h2>{statusCount.Running}</h2>
          </div>
          <div className="card-warn">
            <CircleAlert className="icon yellow" size={24} />
            <p>Warning</p>
            <h2>{statusCount.Warning}</h2>
          </div>
          <div className="card-crit">
            <AlertTriangle className="icon red" size={24} />
            <p>Critical</p>
            <h2>{statusCount.Critical}</h2>
          </div>
          <div className="card-idle">
            <PauseCircle className="icon gray" size={24} />
            <p>Idle</p>
            <h2>{statusCount.Idle}</h2>
          </div>
        </div>

        <h2>Critical Alerts</h2>
        <div className="alerts">
          <p><strong>CNC Machine #4 - Overheating:</strong> Temperature exceeded 85°C - Immediate attention required</p>
          <p><strong>Assembly Line B - Excessive Vibration:</strong> Vibration levels 40% above normal threshold</p>
        </div>

        <h2>Machine Status Details</h2>
        <div className="table-container"> 
          <table className="system-health">
            <thead>
              <tr>
                <th>Machine</th>
                <th>Status</th>
                {/*<th>Efficiency</th>*/}
                <th>Uptime</th>
                <th>Temperature</th>
              </tr>
            </thead>
            <tbody>
              {machines.map((machine, index) => (
                <tr key={index}>
                  <td>{machine.name}</td>
                  <td className={machine.status.toLowerCase()}>{machine.status}</td>
                 {/* <td>{machine.efficiency}</td>*/}
                  <td>{machine.uptime}</td>
                  <td>{machine.temp}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </main>
    </div>
  );
};

export default Machine;
