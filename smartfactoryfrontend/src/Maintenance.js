import React, { useEffect, useState } from "react";
import Menu from "./menu";
import "./Maintenance.css";

const Maintenance = () => {
  const [scheduledMaintenance, setScheduledMaintenance] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedIssue, setSelectedIssue] = useState(null);
  const [successMessage, setSuccessMessage] = useState("");
  const [lowPriorityMachine, setLowPriorityMachine] = useState(null);
  const [formData, setFormData] = useState({
    date: "",
    time: "",
    expectedDuration: "",
    problem: "",
    place: "",
  });
  const [criticalMachine, setCriticalMachine] = useState(null);
  const [runningMachine, setRunningMachine] = useState(null);

  // First useEffect for fetching machine overview
  useEffect(() => {
    const fetchMachineOverview = async () => {
      try {
        const response = await fetch("http://localhost:5270/api/Machine/machineOverview");
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const data = await response.json();
        console.log("Fetched machine overview:", data); // Proverite šta je sadržaj data

        // Proverite da li 'machines' postoji i da li je niz
        if (Array.isArray(data.machines)) {
          // Sortiraj mašine po temperaturi (pretpostavljamo da svaka mašina ima 'temperature' atribut)
          const sortedMachines = data.machines.sort((a, b) => b.temperature - a.temperature); // Sortiraj po temperaturi od najveće ka najmanjoj
          
          // Uzmi prvu tri mašine za visok, srednji i nizak prioritet
          const highPriorityMachine = sortedMachines[0];  // Mašina sa najvećom temperaturom
          const mediumPriorityMachine = sortedMachines[1]; // Mašina sa drugom najvećom temperaturom
          const lowPriorityMachine = sortedMachines[sortedMachines.length - 1]; // Mašina sa najmanjom temperaturom

          setCriticalMachine(highPriorityMachine);  // Dodeli high priority mašinu
          setRunningMachine(mediumPriorityMachine);  // Dodeli medium priority mašinu
          setLowPriorityMachine(lowPriorityMachine);  // Dodeli low priority mašinu
        } else {
          console.error("machines is not an array:", data.machines);
        }
      } catch (error) {
        console.error("Error fetching machine overview:", error);
      }
    };

    fetchMachineOverview();
  }, []);
  

  // Second useEffect for fetching scheduled maintenance
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

  const handleDelete = (index) => {
    setScheduledMaintenance((prev) => prev.filter((_, i) => i !== index));
  };
  

  // Handle form submission
  const handleSubmit = async (e) => {
    e.preventDefault();
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
      const response = await fetch("http://localhost:5270/api/ScheduledMaintenance/AddSchedule", { 
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newScheduled),
      });
      if (!response.ok) {
        throw new Error("Failed to schedule maintenance");
      }
      setScheduledMaintenance((prev) => [...prev, newScheduled]);
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
        {successMessage && <div className="success-message">{successMessage}</div>}
        <h2>Critical Alerts</h2>
        <div className="cards">
  {criticalMachine && (
    <div className="card high-priority">
      <p>High Priority</p>
      <h2>{criticalMachine.machineName}</h2> {/* Koristite 'machineName' umesto 'name' */}
      <small>Critical Status</small>
      <button className="high-btn" onClick={() => openModal(criticalMachine.machineName)}>
        Schedule Service
      </button>
    </div>
  )}
  {runningMachine && (
    <div className="card medium-priority">
      <p>Medium Priority</p>
      <h2>{runningMachine.machineName}</h2> {/* Koristite 'machineName' umesto 'name' */}
      <small>Running Status</small>
      <button className="medium-btn" onClick={() => openModal(runningMachine.machineName)}>
        Schedule Service
      </button>
    </div>
  )}
  {lowPriorityMachine && (
    <div className="card low-priority">
      <p>Low Priority</p>
      <h2>{lowPriorityMachine.machineName}</h2> {/* Koristite 'machineName' umesto 'name' */}
      <small>Low Status</small>
      <button className="low-btn" onClick={() => openModal(lowPriorityMachine.machineName)}>
        Schedule Service
      </button>
    </div>
  )}
</div>


        <h2>Scheduled Maintenance</h2>
        <div className="scheduled-list">
          {scheduledMaintenance.length > 0 ? (
            scheduledMaintenance.map((item, index) => (
              <div key={index} className="scheduled-item">
                <h3>{item.machine} - {item.task}</h3>
                <p>{item.scheduledTime} - Expected duration: {item.expectedDuration}</p>
                <button className="delete-btn" onClick={() => handleDelete(index)}>Delete</button>
              </div>
            ))
          ) : (
            <p>No scheduled maintenance yet.</p>
          )}
        </div>
      </main>

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
                <button type="button" className="cancel" onClick={closeModal}>Cancel</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default Maintenance;
