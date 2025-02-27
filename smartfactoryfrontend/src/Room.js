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
    warningUpper: "",
    warningLower: "",
    criticalUpper: "",
    criticalLower: "",
    normalUpper: "",
    normalLower: "",
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
  const openThresholdModal = (sensor) => {
    setSelectedSensor(sensor);
    setModalType("threshold");
    setShowModal(true);
  };
  const handleThresholdChange = (e) => {
    setThresholds({ ...thresholds, [e.target.name]: e.target.value });
  };
  const saveThreshold = async () => {
    try {
      const response = await fetch("http://localhost:5270/api/Device/UpdateThreshold", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          sensorId: selectedSensor.id,
          ...thresholds,
        }),
      });
      if (!response.ok) {
        throw new Error("Failed to update threshold");
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
          <label>Normal Lower: <input type="number" name="normalLowThreshold" onChange={handleThresholdChange} /></label>
            <label>Normal Upper: <input type="number" name="normalHighThreshold" onChange={handleThresholdChange} /></label>
            <label>Warning Lower: <input type="number" name="warningLowThreshold" onChange={handleThresholdChange} /></label>
            <label>Warning Upper: <input type="number" name="warningHighThreshold" onChange={handleThresholdChange} /></label>
            <label>Critical Lower: <input type="number" name="criticalLowThreshold" onChange={handleThresholdChange} /></label>
            <label>Critical Upper: <input type="number" name="criticalHighThreshold" onChange={handleThresholdChange} /></label>
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