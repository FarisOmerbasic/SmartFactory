import React, { useEffect, useState } from "react";
import Menu from "./menu";
import "./Room.css";

const Room = () => {
  const [sensors, setSensors] = useState([]);
  const [rooms, setRooms] = useState([]);
  const [currentRoomIndex, setCurrentRoomIndex] = useState(0);
  const [showModal, setShowModal] = useState(false);
  const [selectedSensor, setSelectedSensor] = useState(null);
  const [thresholds, setThresholds] = useState({
    normalLowThreshold: "",
    normalHighThreshold: "",
    warningLowThreshold: "",
    warningHighThreshold: "",
    criticalLowThreshold: "",
    criticalHighThreshold: "",
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

  const nextRoom = () => {
    setCurrentRoomIndex((prevIndex) => (prevIndex + 1) % rooms.length);
  };

  const prevRoom = () => {
    setCurrentRoomIndex((prevIndex) => (prevIndex - 1 + rooms.length) % rooms.length);
  };

  const openThresholdModal = (sensor) => {
    setSelectedSensor(sensor);
    setShowModal(true);
  };

  const handleThresholdChange = (e) => {
    setThresholds({ ...thresholds, [e.target.name]: e.target.value });
  };

  const saveThreshold = async () => {
    try {
      const response = await fetch("http://localhost:5270/api/Device/UpdateDeviceThreshold", {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          "Accept": "application/json"
        },
        body: JSON.stringify({
          deviceId: selectedSensor.id,
          normalLowThreshold: Number(thresholds.normalLower) || 0,
          normalHighThreshold: Number(thresholds.normalUpper) || 0,
          warningLowThreshold: Number(thresholds.warningLower) || 0,
          warningHighThreshold: Number(thresholds.warningUpper) || 0,
          criticalLowThreshold: Number(thresholds.criticalLower) || 0,
          criticalHighThreshold: Number(thresholds.criticalUpper) || 0,
        }),
      });
  
      if (!response.ok) {
        const errorMessage = await response.text();
        throw new Error(`Failed to update threshold: ${response.status} ${response.statusText} - ${errorMessage}`);
      }
  
      console.log("Threshold updated successfully");
      setShowModal(false);
    } catch (error) {
      console.error("Error updating threshold:", error);
    }
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
      {showModal && (
        <div className="modal-room">
          <div className="modal-content-room">
            <h2>Set Threshold for {selectedSensor?.name}</h2>
            <label>Normal Lower: <input type="number" name="normalLowThreshold" onChange={handleThresholdChange} /></label>
            <label>Normal Upper: <input type="number" name="normalHighThreshold" onChange={handleThresholdChange} /></label>
            <label>Warning Lower: <input type="number" name="warningLowThreshold" onChange={handleThresholdChange} /></label>
            <label>Warning Upper: <input type="number" name="warningHighThreshold" onChange={handleThresholdChange} /></label>
            <label>Critical Lower: <input type="number" name="criticalLowThreshold" onChange={handleThresholdChange} /></label>
            <label>Critical Upper: <input type="number" name="criticalHighThreshold" onChange={handleThresholdChange} /></label>
            <button className="confirm-btn" onClick={saveThreshold}>Save</button>
            <button className="cancel-btn" onClick={() => setShowModal(false)}>Close</button>
          </div>
        </div>
      )}
    </div>
  );
};

export default Room;
