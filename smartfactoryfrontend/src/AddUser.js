import React, { useState, useEffect } from "react";
import "./AddUser.css";
import Menu from "./menu";

const AddUser = () => {
    const [userData, setUserData] = useState({
        firstName: "",
        lastName: "",
        email: "",
        role: "",
    });
    const [users, setUsers] = useState([]);

    // Mapa koja povezuje uloge sa brojevima
    const roleMap = {
        SuperUser: 1,
        FactoryManager: 2,
        Maintenance: 3,
        Supervisors: 4,
        Administrator: 5,
        Operations: 6,
        User: 7,
    };

    useEffect(() => {
        fetchUsers();
    }, []);

    const fetchUsers = async () => {
        try {
            const response = await fetch("http://localhost:5270/api/User/getAllUsers");
            if (!response.ok) {
                throw new Error("Failed to fetch users");
            }
            const data = await response.json();
            setUsers(data);
        } catch (error) {
            console.error("Error fetching users:", error);
        }
    };

    const handleChange = (e) => {
        setUserData({ ...userData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        // Pretvori ulogu u broj pomoću roleMap
        const roleNumber = roleMap[userData.role] || null; // Ako nema uloge, setuj null

        const requestBody = {
            firstName: userData.firstName,
            lastName: userData.lastName,
            email: userData.email,
            role: roleNumber,
        };

        // Pozovi funkciju za generisanje PDF-a sa brojem uloge
        generatePDF(requestBody);
    };

    const handleDelete = async (id) => {
        try {
            const response = await fetch(`http://localhost:5270/api/User/removeUser/${id}`, {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ id }),
            });

            if (response.ok) {
                alert("✅ User deleted successfully");
                fetchUsers(); // Refresh users list
            } else {
                const text = await response.text();
                alert("❌ Error deleting user: " + text);
            }
        } catch (error) {
            console.error("Error deleting user:", error);
            alert("❌ Failed to delete user. Check console for details.");
        }
    };

    const generatePDF = async (userData) => {
        try {
            const requestBody = {
                firstName: userData.firstName,
                lastName: userData.lastName,
                email: userData.email,
                role: userData.role, // Ovdje će biti broj umesto imena
            };

            console.log('Request body:', requestBody); // Proveri šta šalješ

            const response = await fetch("http://localhost:5270/api/User/register", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(requestBody),
            });

            console.log('Response status:', response.status); // Loguj status odgovora

            if (!response.ok) {
                const errorText = await response.text();
                console.error("Error response from server:", errorText);
                throw new Error("Failed to generate PDF");
            }

            const blob = await response.blob();
            console.log('Blob received:', blob); // Loguj blob kako bi proverio da li je vraćen PDF

            const pdfUrl = URL.createObjectURL(blob);
            window.open(pdfUrl);

        } catch (error) {
            console.error("Error generating PDF:", error);
            alert("❌ Failed to generate PDF. Check console for details.");
        }
    };

    return (
        <div className="add-user-container">
            <Menu />
            <main className="add-user-main">
                <h1>Generate PDF with User Data</h1>
                <form onSubmit={handleSubmit} className="add-user-form">
                    <div className="input-group">
                        <label>First Name</label>
                        <input type="text" name="firstName" value={userData.firstName} onChange={handleChange} required />
                    </div>
                    <div className="input-group">
                        <label>Last Name</label>
                        <input type="text" name="lastName" value={userData.lastName} onChange={handleChange} required />
                    </div>
                    <div className="input-group">
                        <label>Email</label>
                        <input type="email" name="email" value={userData.email} onChange={handleChange} required />
                    </div>
                    <div className="input-group">
                        <label>Role</label>
                        <select name="role" value={userData.role} onChange={handleChange} required>
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
                    <button type="submit" className="submit-btn">Generate PDF</button>
                </form>

                <h2>User List</h2>
                <table className="user-table">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Email</th>
                            <th>Role</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {users.map((user) => (
                            <tr key={user.id}>
                                <td>{user.id}</td>
                                <td>{user.firstName}</td>
                                <td>{user.lastName}</td>
                                <td>{user.email}</td>
                                <td>{roleMap[user.role] || user.role}</td> {/* Prikazivanje broja ili imena uloge */}
                                <td>
                                    <button className="delete-btn" onClick={() => handleDelete(user.id)}>Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </main>
        </div>
    );
};

export default AddUser;
