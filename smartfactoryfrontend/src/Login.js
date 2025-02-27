import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Login.css";

const users = [
  { id: 1, firstName: "John", lastName: "Doe", email: "john@gmail.com", password: "password", role: "SuperUser" },
  { id: 2, firstName: "Jane", lastName: "Smith", email: "jane@gmail.com", password: "password", role: "Maintenance" },
  { id: 3, firstName: "Alice", lastName: "Johnson", email: "alice@gmail.com", password: "password", role: "FactoryManager" },
  { id: 4, firstName: "Bob", lastName: "Brown", email: "bob@gmail.com", password: "password", role: "Administrator" },
  { id: 5, firstName: "Charlie", lastName: "Davis", email: "charlie@gmail.com", password: "password", role: "Supervisors" },
  { id: 6, firstName: "David", lastName: "Williams", email: "david@gmail.com", password: "password", role: "User" },
];

const Login = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  const handleSignIn = () => {
    const user = users.find(u => u.email === email);

    if (!user) {
      setErrorMessage("❌ No account found with this email.");
      return;
    }

    if (user.password !== password) {
      setErrorMessage("❌ Incorrect password. Try again.");
      return;
    }

    // Allowed roles
    const allowedRoles = ["SuperUser", "Maintenance", "FactoryManager", "Supervisors", "Administrator", "User"];
    if (!allowedRoles.includes(user.role)) {
      setErrorMessage("❌ Access denied for this role.");
      return;
    }

    // Store only necessary user details
    localStorage.setItem("user", JSON.stringify({ id: user.id, firstName: user.firstName, role: user.role }));

    // Redirect to Dashboard
    navigate("/dashboard");
  };

  return (
    <div className="login-container">
      <div className="login-box">
        <h2 className="welcome">User Login</h2>
        <p className="subtitle">Enter your credentials to access your account</p>

        {errorMessage && <p className="error-message">{errorMessage}</p>}

        <div className="input-group">
          <label>Email</label>
          <input 
            type="email" 
            placeholder="Enter your email" 
            value={email} 
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>

        <div className="input-group">
          <label>Password</label>
          <input 
            type="password" 
            placeholder="Enter your password" 
            value={password} 
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>

        <button className="login-btn" onClick={handleSignIn}>Sign In</button>
      </div>
    </div>
  );
};

export default Login;
