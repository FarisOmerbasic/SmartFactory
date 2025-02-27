import React, { useEffect, useState } from "react";
import Menu from "./menu";
import "./Maintenance.css";

const alerts = [
  { priority: "High", issue: "Bearing Wear", description: "CNC Machine #3 - Estimated 48h until critical.", action: "Schedule Service" },
  { priority: "Medium", issue: "Motor Temperature", description: "Assembly Line B motor temperature trending above normal.", action: "Schedule Service" },
  { priority: "Low", issue: "Belt Tension", description: "Conveyor #2 belt tension below optimal.", action: "Schedule Service" }
];

const Maintenance = () => {
  const [scheduledMaintenance, setScheduledMaintenance] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedIssue, setSelectedIssue] = useState(null);
  const [successMessage, setSuccessMessage] = useState("");
  const [formData, setFormData] = useState({
    date: "",
    time: "",
    expectedDuration: "",
    problem: "",
    place: "",
  });

  // Fetch scheduled maintenance from API
  useEffect(() => {
    const fetchScheduledMaintenance = async () => {
      try {
        const response = await fetch("http://localhost:5270/api/ScheduledMaintenance/ScheduledMaintenance");
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const data = await response.json();
        setScheduledMaintenance(data.scheduled || []);
      } catch (error) {
        console.error("Error fetching scheduled maintenance:", error);
      }
    };
    fetchScheduledMaintenance();
  }, []);

  // Open modal
  const openModal = (issue) => {
    setSelectedIssue(issue);
    setFormData({ date: "", time: "", expectedDuration: "", problem: issue, place: "" });
    setIsModalOpen(true);
  };

  // Close modal
  const closeModal = () => {
    setIsModalOpen(false);
    setSelectedIssue(null);
    setFormData({ date: "", time: "", expectedDuration: "", problem: "", place: "" });
  };

  // Handle input changes
  const handleInputChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  // Handle form submission
  const handleSubmit = async (e) => {
    e.preventDefault();
    // Ensure all fields are filled before proceeding
    if (!formData.date || !formData.time || !formData.expectedDuration) {
      alert("Please fill in all fields before scheduling.");
      return;
    }
    const newScheduled = {
      machine: selectedIssue,
      task: formData.problem,
      scheduledTime: `${formData.date} ${formData.time}`,
      expectedDuration: formData.expectedDuration,
    };
    try {
      const response = await fetch("http://localhost:5270/api/ScheduledMaintenance/AddSchedule", { // Promijenjena ruta
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newScheduled),
      });
      if (!response.ok) {
        throw new Error("Failed to schedule maintenance");
      }
      // Update UI instantly
      setScheduledMaintenance((prev) => [...prev, newScheduled]);
      // Show success message
      setSuccessMessage(":white_tick: Maintenance successfully scheduled!");
      setTimeout(() => setSuccessMessage(""), 3000);
      closeModal();
    } catch (error) {
      console.error("Error scheduling maintenance:", error);
    }
  };

  return (
    <div className="dashboard-container">
      <Menu />
      <main className="main-content">
        <h1>Maintenance Overview</h1>
        {/* Success Message */}
        {successMessage && <div className="success-message">{successMessage}</div>}
        <h2>Critical Alerts</h2>
        <div className="cards">
          {alerts.map((alert, index) => (
            <div key={index} className={`card ${alert.priority.toLowerCase()}-priority`}>
              <p>{alert.priority} Priority</p>
              <h2>{alert.issue}</h2>
              <small>{alert.description}</small>
              <button
                className={`${alert.priority.toLowerCase()}-btn`}
                onClick={() => openModal(alert.issue)}
              >
                {alert.action}
              </button>
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
            <p>No scheduled maintenance yet.</p>
          )}
        </div>
      </main>
      {/* Modal Popup */}
      {isModalOpen && (
        <div className="modal-overlay">
          <div className="modal">
            <h2>Schedule Maintenance for {selectedIssue}</h2>
            <form onSubmit={handleSubmit}>
              <label>Date:</label>
              <input type="date" name="date" value={formData.date} onChange={handleInputChange} required />
              <label>Time:</label>
              <input type="time" name="time" value={formData.time} onChange={handleInputChange} required />
              <label>Expected Duration:</label>
              <input type="text" name="expectedDuration" value={formData.expectedDuration} onChange={handleInputChange} placeholder="e.g., 2 hours" required />
              <label>Problem Description:</label>
              <input type="text" name="problem" value={formData.problem} onChange={handleInputChange} required />
              <div className="modal-actions">
                <button type="submit" className="confirm-btn">Confirm</button>
                <button type="button" className="cancel-btn" onClick={closeModal}>Cancel</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default Maintenance;
