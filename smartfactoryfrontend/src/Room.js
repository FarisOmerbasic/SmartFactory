import React, { useEffect, useState } from "react";
import { LineChart, Line, XAxis, YAxis, Tooltip, ResponsiveContainer } from "recharts";
import Menu from "./menu";
import "./Room.css";
const Room = () => {
  const [sensors, setSensors] = useState([]);
  const [rooms, setRooms] = useState([]);
  const [currentRoomIndex, setCurrentRoomIndex] = useState(0);
  const [selectedSensor, setSelectedSensor] = useState(null);
  const [chartData, setChartData] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [modalType, setModalType] = useState("");
  const [thresholds, setThresholds] = useState({
    warningUpper: null,
    warningLower: null,
    criticalUpper: null,
    criticalLower: null,
    normalUpper: null,
    normalLower: null,
  });
  
  const fetchSensors = async (roomName) => {
    try {
      const response = await fetch(`http://localhost:5270/api/Device/GetDevicesByRoomName?roomName=${roomName}`);
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      const data = await response.json();
      setSensors(data);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  const fetchDeviceTrending = async (deviceId) => {
    try {
      const response = await fetch(`http://localhost:5270/api/Trending/GetDeviceTrendingAverage/${deviceId}`);
      if (!response.ok) {
        throw new Error("Failed to fetch trending data");
      }
      const data = await response.json();
      const formattedData = data.map(item => ({
        time: new Date(item.time).toLocaleTimeString(),
        averageValue: item.averageValue
      }));
      setChartData(formattedData);
    } catch (error) {
      console.error("Error fetching trending data:", error);
    }
  };

  const fetchThresholds = async (sensorId) => {
    try {
      const response = await fetch(`http://localhost:5270/api/Device/GetThresholdById/${sensorId}`);
      if (!response.ok) {
        throw new Error("Failed to fetch threshold data");
      }
      const data = await response.json();
      console.log("Fetched threshold data:", data); // Debugging
  
      setThresholds({
        warningUpper: data.warningHighThreshold !== undefined ? data.warningHighThreshold : 0,
        warningLower: data.warningLowThreshold !== undefined ? data.warningLowThreshold : 0,
        criticalUpper: data.criticalHighThreshold !== undefined ? data.criticalHighThreshold : 0,
        criticalLower: data.criticalLowThreshold !== undefined ? data.criticalLowThreshold : 0,
        normalUpper: data.normalHighThreshold !== undefined ? data.normalHighThreshold : 0,
        normalLower: data.normalLowThreshold !== undefined ? data.normalLowThreshold : 0,
      });
      
      console.log("Updated state:",thresholds);
    } catch (error) {
      console.error("Error fetching threshold data:", error);
    }
  };
  

  const fetchCategories = async () => {
    try {
      const response = await fetch("http://localhost:5270/api/Category/GetAllCategories");
      const data = await response.json();
      const filteredRooms = data
        .filter(category => category.categoryNumber === 1)
        .flatMap(category => category.categoryNames);
      setRooms(filteredRooms);
    } catch (error) {
      console.error("Error fetching categories:", error);
    }
  };
  useEffect(() => {
    fetchCategories();
  }, []);
  useEffect(() => {
    if (rooms.length > 0) {
      fetchSensors(rooms[currentRoomIndex]);
    }
  }, [currentRoomIndex, rooms]);

  useEffect(() => {
    if (selectedSensor) {
      fetchDeviceTrending(selectedSensor.id);
    }
  }, [selectedSensor]);

  const nextRoom = () => {
    setCurrentRoomIndex((prevIndex) => (prevIndex + 1) % rooms.length);
  };
  const prevRoom = () => {
    setCurrentRoomIndex((prevIndex) => (prevIndex - 1 + rooms.length) % rooms.length);
  };
  const openThresholdModal = async (sensor) => {
    setSelectedSensor(sensor);
    setModalType("threshold");
    await fetchThresholds(sensor.id);
    setShowModal(true);
  };
  const handleThresholdChange = (e) => {
    const value = e.target.value === "" ? "" : parseFloat(e.target.value);
    setThresholds((prevThresholds) => ({
      ...prevThresholds,
      [e.target.name]: value
    }));
  };
  
  const saveThreshold = async () => {
    console.log("Threshold Data Before Sending:", thresholds);
    try {
      const response = await fetch("http://localhost:5270/api/Device/UpdateDeviceThreshold", {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          deviceId: selectedSensor.id,
          normalLowThreshold: thresholds.normalLowThreshold ? parseFloat(thresholds.normalLowThreshold) : 0.0,
          normalHighThreshold: thresholds.normalHighThreshold ? parseFloat(thresholds.normalHighThreshold) : 0.0,
          warningLowThreshold: thresholds.warningLowThreshold ? parseFloat(thresholds.warningLowThreshold) : 0.0,
          warningHighThreshold: thresholds.warningHighThreshold ? parseFloat(thresholds.warningHighThreshold) : 0.0,
          criticalLowThreshold: thresholds.criticalLowThreshold ? parseFloat(thresholds.criticalLowThreshold) : 0.0,
          criticalHighThreshold: thresholds.criticalHighThreshold ? parseFloat(thresholds.criticalHighThreshold) : 0.0,
        }),
      });
      if (!response.ok) {
        throw new Error(`Failed to update threshold: ${response.statusText}`);
      }
      setShowModal(false);
    } catch (error) {
      console.error("Error updating threshold:", error);
    }
  };
  


  const openChartModal = (sensor) => {
    setSelectedSensor(sensor);
    fetchDeviceTrending(sensor.id);
    setModalType("chart"); // Fetch data for the selected sensor
    setShowModal(true); // Show modal with chart
  };

  return (
    <div className="dashboard-container">
      <Menu />
      <main className="main-content">
        <div className="room-container">
          <h1>{rooms[currentRoomIndex]}</h1>
          <h2>Sensors</h2>
          <table className="system-health">
            <thead>
              <tr>
                <th>Sensor</th>
                <th>Value</th>
                <th>Status</th>
                <th>Last Updated</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {sensors.map(sensor => (
                <tr key={sensor.id}>
                  <td>{sensor.name}</td>
                  <td>{sensor.numericValue} {sensor.unit || 'N/A'}</td>
                  <td className={sensor.isActive ? "success" : "danger"}>{sensor.isActive ? "Active" : "Inactive"}</td>
                  <td>{new Date().toLocaleTimeString()}</td>
                  <td>
                    <button className="threshold-button" onClick={() => openThresholdModal(sensor)}>Set Threshold</button>
                    <button className="chart-button" onClick={() => openChartModal(sensor)}>Show Trending</button> {/* Button to open the graph */}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>

          <div className="button-container">
            <button className="room-navigation prev-room" onClick={prevRoom}>Previous Room</button>
            <button className="room-navigation next-room" onClick={nextRoom}>Next Room</button>
          </div>
        </div>
      </main>
      {showModal && selectedSensor && (
  <div className="modal-room">
    <div className="modal-content-room">
      <h2>{selectedSensor.name}</h2>
      
      {/* Conditional rendering based on modalType */}
      {modalType === "chart" && chartData.length > 0 && (
        <div>
          <h3>Trending Data</h3>
          <ResponsiveContainer width="100%" height={400}>
            <LineChart data={chartData}>
              <XAxis dataKey="time" />
              <YAxis />
              <Tooltip />
              <Line type="monotone" dataKey="averageValue" stroke="#8884d8" />
            </LineChart>
          </ResponsiveContainer>
        </div>
      )}

      {modalType === "threshold" && (
        <div>
          <h3>Set Threshold for {selectedSensor?.name}</h3>
          <div className="threshold-form">
          <label>Normal Lower: <input type="number" name="normalLower" value={thresholds.normalLower ?? ""} onChange={handleThresholdChange} /></label>
            <label>Normal Upper: <input type="number" name="normalUpper" value={thresholds.normalUpper ?? ""} onChange={handleThresholdChange} /></label>
            <label>Warning Lower: <input type="number" name="warningLower" value={thresholds.warningLower ?? ""} onChange={handleThresholdChange} /></label>
            <label>Warning Upper: <input type="number" name="warningUpper" value={thresholds.warningUpper ?? ""} onChange={handleThresholdChange} /></label>
            <label>Critical Lower: <input type="number" name="criticalLower" value={thresholds.criticalLower ?? ""} onChange={handleThresholdChange} /></label>
            <label>Critical Upper: <input type="number" name="criticalUpper" value={thresholds.criticalUpper ?? ""} onChange={handleThresholdChange} /></label>
            <button className="confirm-btn" onClick={saveThreshold}>Save</button>
          </div>
        </div>
      )}

      <button className="cancel-btn" onClick={() => setShowModal(false)}>Close</button>
    </div>
  </div>

      )}
    </div>
  );
};
export default Room;