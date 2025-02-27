import React, { useEffect, useState } from "react";
import Menu from "./menu";
import "./Machine.css";
import { AlertTriangle, CircleCheck, CircleAlert, PauseCircle } from "lucide-react";

const Machine = () => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [startTime, setStartTime] = useState(Date.now());

  // Funkcija za dohvat podataka s API-a
  const fetchData = () => {
    fetch("http://localhost:5270/api/Machine/machineOverview")
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((data) => {
        setData(data); // Postavljanje podataka iz API-ja
        setStartTime(Date.now()); // Resetujemo startno vrijeme kad stignu novi podaci
        setLoading(false);
      })
      .catch((error) => {
        setError(error);
        setLoading(false);
      });
  };

  // Učitaj podatke na početku i svakih 60 sekundi
  useEffect(() => {
    fetchData();
    const interval = setInterval(fetchData, 60000); // Refresh podataka svakih 60s
    return () => clearInterval(interval);
  }, []);

  // Funkcija koja ažurira uptime svakih 1 sekundu
  useEffect(() => {
    const interval = setInterval(() => {
      setData((prevData) => {
        if (!prevData) return prevData;
        const elapsedTime = (Date.now() - startTime) / 3600000; // Vrijeme u satima

        return {
          ...prevData,
          machines: prevData.machines.map((machine) => {
            const currentUpTime = parseFloat(machine.upTime) || 0; // Osiguraj da je upTime broj
            const updatedUpTime = currentUpTime + elapsedTime > currentUpTime 
              ? (currentUpTime + elapsedTime).toFixed(2) 
              : currentUpTime.toFixed(2); // Ne dodaj ako je elapsedTime 0

            return {
              ...machine,
              upTime: updatedUpTime, // Ažurirani uptime
            };
          }),
        };
      });
    }, 1000); // Ažuriraj uptime svake sekunde

    return () => clearInterval(interval);
  }, [startTime]);

  // Prikazivanje dok se podaci učitavaju ili ako dođe do greške
  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error.message}</p>;

  return (
    <div className="dashboard-container">
      <Menu />
      <main className="main-content">
        <h1>Machine Status Overview</h1>

        <div className="cards">
          <div className="card-run">
            <CircleCheck className="icon green" size={24} />
            <p>Running</p>
            <h2>{data.runningMachines}</h2>
          </div>
          <div className="card-warn">
            <CircleAlert className="icon yellow" size={24} />
            <p>Warning</p>
            <h2>{data.warningMachinesThreshold}</h2>
          </div>
          <div className="card-crit">
            <AlertTriangle className="icon red" size={24} />
            <p>Critical</p>
            <h2>{data.criticalMachinesThreshold}</h2>
          </div>
          <div className="card-idle">
            <PauseCircle className="icon gray" size={24} />
            <p>Idle</p>
            <h2>{data.idleMachines}</h2>
          </div>
        </div>

        <h2>Machine Status Details</h2>
        <div className="table-container">
          <table className="system-health">
            <thead>
              <tr>
                <th>Machine</th>
                <th>Status</th>
                <th>Uptime (hrs)</th>
                <th>Temperature (°C)</th>
              </tr>
            </thead>
            <tbody>
              {data.machines.map((machine) => (
                <tr key={machine.machineName}>
                  <td>{machine.machineName}</td>
                  <td className={machine.status === "Runing" ? "running" : "critical"}>
                    {machine.status === "Runing" ? "Running" : "Critical"}
                  </td>
                  <td>{machine.upTime}</td>
                  <td>{machine.temperature}</td>
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
