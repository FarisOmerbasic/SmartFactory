import React, { useState } from "react";
import "./AddUser.css";
import Menu from "./menu";

const AddUser = () => {
    const [userData, setUserData] = useState({
        firstName: "",
        lastName: "",
        email: "",
        password: "",
        role: "",
    });

    const handleChange = (e) => {
        setUserData({ ...userData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
    
        const roleMapping = {
            SuperUser: 1,
            FactoryManager: 2,
            Maintenance: 3,
            Supervisors: 4,
            Administrator: 5,
            Operations: 6,
            User: 7
        };
    
        const payload = {
            id: 0,
            firstName: userData.firstName,
            lastName: userData.lastName,
            email: userData.email,
            password: userData.password,
            role: roleMapping[userData.role] || 0
        };
    
        try {
            const response = await fetch("http://localhost:5270/api/User/register", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(payload),
            });
    
            const text = await response.text(); // Uzimamo odgovor kao običan tekst
            console.log("Server Response:", text);
    
            if (response.ok) {
                alert("✅ " + text); // Prikazujemo poruku iz API-ja
                setUserData({
                    firstName: "",
                    lastName: "",
                    email: "",
                    password: "",
                    role: "",
                });
            } else {
                alert("❌ Error adding user: " + text);
            }
        } catch (error) {
            console.error("Error:", error);
            alert("❌ Failed to send request. Check console for details.");
        }
    };
    

    return (
        <div className="add-user-container">
            <Menu />
            <main className="add-user-main">
                <h1>Add New User</h1>
                <form onSubmit={handleSubmit} className="add-user-form">
                    <div className="input-group">
                        <label>First Name</label>
                        <input
                            type="text"
                            name="firstName"
                            value={userData.firstName}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="input-group">
                        <label>Last Name</label>
                        <input
                            type="text"
                            name="lastName"
                            value={userData.lastName}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="input-group">
                        <label>Email</label>
                        <input
                            type="email"
                            name="email"
                            value={userData.email}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="input-group">
                        <label>Password</label>
                        <input
                            type="password"
                            name="password"
                            value={userData.password}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="input-group">
                        <label>Role</label>
                        <select
                            name="role"
                            value={userData.role}
                            onChange={handleChange}
                            required
                        >
                            <option value="">Select Role</option>
                            <option value="SuperUser">SuperUser</option>
                            <option value="FactoryManager">Factory Manager</option>
                            <option value="Maintenance">Maintenance</option>
                            <option value="Supervisors">Supervisors</option>
                            <option value="Administrator">Administrator</option>
                            <option value="Operations">Operations</option>
                            <option value="User">User</option>
                        </select>
                    </div>
                    <button type="submit" className="submit-btn">Add User</button>
                </form>
            </main>
        </div>
    );
};

export default AddUser;
