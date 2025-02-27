import React, { useEffect, useState } from "react";
import Menu from "./menu"; 
import "./Room.css";

const Room = () => {
  const [sensors, setSensors] = useState([]);
  const [rooms, setRooms] = useState([]);
  const [currentRoomIndex, setCurrentRoomIndex] = useState(0);
  const [currentRoomData, setCurrentRoomData] = useState(null);

  // Fetch devices for the selected room
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

  // Fetch categories to get rooms with categoryNumber 1
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
      fetchSensors(rooms[currentRoomIndex]); // Fetch sensors for the current room
    }
  }, [currentRoomIndex, rooms]);

  const nextRoom = () => {
    setCurrentRoomIndex((prevIndex) => (prevIndex + 1) % rooms.length);
  };

  const prevRoom = () => {
    setCurrentRoomIndex((prevIndex) => (prevIndex - 1 + rooms.length) % rooms.length);
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
              </tr>
            </thead>
            <tbody>
              {sensors.map(sensor => (
                <tr key={sensor.id}>
                  <td>📡 {sensor.name}</td>
                  <td>{sensor.numericValue} {sensor.unit || 'N/A'}</td>
                  <td className={sensor.isActive ? "success" : "danger"}>
                    {sensor.isActive ? "Active" : "Inactive"}
                  </td>
                  <td>{new Date().toLocaleTimeString()}</td>
                </tr>
              ))}
            </tbody>
          </table>

          <div className="button-container">
            <button className="add-sensor">Add Sensor</button>
            <button className="add-machine">Add Machine</button>
            <button onClick={prevRoom}>Previous Room</button>
            <button onClick={nextRoom}>Next Room</button>
          </div>
        </div>
      </main>
    </div>
  );
};

export default Room;
