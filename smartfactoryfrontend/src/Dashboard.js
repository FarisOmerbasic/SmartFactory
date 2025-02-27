import Menu from "./menu";
import "./Dashboard.css";
import React, { useState, useEffect } from "react";


const Dashboard = () => {
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
        const [productionData, setProductionData] = useState({
                lineA: {},
                lineB: {},
                lineC: {},
            });
        
            const fetchProductionData = async (line) => {
                try {
                    const response = await fetch(`http://localhost:5270/api/Production/GetProductionData/${line}`);
                    if (!response.ok) {
                        throw new Error("Network response was not ok");
                    }
                    const data = await response.json();
                    return data;
                } catch (error) {
                    console.error(`Error fetching data for ${line}:`, error);
                    return null;
                }
            };
        
            useEffect(() => {
                const fetchAllProductionData = async () => {
                    const lineAData = await fetchProductionData("Line A");
                    const lineBData = await fetchProductionData("Line B");
                    const lineCData = await fetchProductionData("Line C");
        
                    setProductionData({
                        lineA: lineAData || {},
                        lineB: lineBData || {},
                        lineC: lineCData || {},
                    });
                };
        
                fetchAllProductionData();
            }, []);
      
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
        <div className="dashboard-container">
            <Menu />
            <main className="main-content">
                <h1>Overview</h1>
                
                {/* Cards */}
                <div className="cards">
                    <div className="card">
                        <p>Production Rate</p>
                        <h2>98.5%</h2>
                        <small>2% increase from last week</small>
                    </div>
                    <div className="card">
                        <p>Energy Efficiency</p>
                        <h2>85%</h2>
                        <small>Carbon neutral operations</small>
                    </div>
                    <div className="card">
                        <p>Alerts</p>
                        <h2>3 Active</h2>
                        <small>2 high priority</small>
                    </div>
                    <div className="card">
                        <p>Maintenance</p>
                        <h2>4 Scheduled</h2>
                        <small>Next in 2 days</small>
                    </div>
                </div>
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
            <h2>€{energyData?.totalCost ? energyData.totalCost.toFixed(2) : "0.00"}</h2>

            <small>Daily energy cost</small>
          </div>
        </div>      

        <div className="header-container">
                    <h1>Real-time Production Metrics</h1>
                </div>
                <div className="cards">
                    <div className="card">
                        <p>Today's Projected Output</p>
                        <h2>{productionData.lineA.todaysProjectedOutput || 0} units</h2>
                        <small>{productionData.lineA.todaysProjectedOutput ? `${(productionData.lineA.todaysProjectedOutput / 2150 * 100).toFixed(2)}%` : '0%'} of target</small>
                    </div>
                    <div className="card">
                        <p>Week's Projection</p>
                        <h2>{productionData.lineA.weeksProjection || 0} units</h2>
                        <small>{productionData.lineA.weeksProjection ? `${(productionData.lineA.weeksProjection / 10500 * 100).toFixed(2)}%` : '0%'} of target</small>
                    </div>
                </div>

            </main>
        </div>
    );
};

export default Dashboard;
