import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Login.css";


const users = [
  { id: 1, firstName: "John", lastName: "Doe", email: "john@gmail.com", password: "password", role: "SuperUser" },
  { id: 2, firstName: "Jane", lastName: "Smith", email: "jane@gmail.com", password: "password", role: "Maintenance" },
  { id: 3, firstName: "Alice", lastName: "Johnson", email: "alice@gmail.com", password: "password", role: "FactoryManager" },
  { id: 4, firstName: "Bob", lastName: "Brown", email: "bob@gmail.com", password: "password", role: "Administrator" },
  { id: 5, firstName: "Charlie", lastName: "Davis", email: "charlie@gmail.com", password: "password", role: "Supervisors" },
];

const Login = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSignIn = () => {
    const user = users.find(u => u.email === email && u.password === password);

    if (!user || !["SuperUser", "Maintenance", "FactoryManager", "Supervisors", "Administrator"].includes(user.role)) {
      alert("❌ Invalid credentials entered! Try again."); // Show alert

      return;
    }

    localStorage.setItem("user", JSON.stringify(user)); // Store user info
    navigate("/dashboard");
  };


  return (
    <div className="login-container">
      <div className="login-box">
        <h2 className="welcome">User Login</h2>
        <p className="subtitle">Enter your credentials to access your account</p>

        <div className="input-group">
          <label>Email</label>
          <input type="email" placeholder="Enter your email" value={email} onChange={(e) => setEmail(e.target.value)} />
        </div>

        <div className="input-group">
          <label>Password</label>
          <input type="password" placeholder="Enter your password" value={password} onChange={(e) => setPassword(e.target.value)} />
        </div>

        <button className="login-btn" onClick={handleSignIn}>Sign In</button>
      </div>
    </div>
  );
};

export default Login;
