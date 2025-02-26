import React from "react";
import "./LoginAdmin.css";

const LoginAdmin = () => {
  return (
    <div className="login-container">
      <div className="login-box">
        <h2 className="welcome">Admin Login</h2>
        <p className="subtitle">
          Enter your credentials to access the production dashboard
        </p>
        <div className="input-group">
          <label>Email</label>
          <input type="email" placeholder="Enter your email" />
        </div>
        <div className="input-group">
          <label>Password</label>
          <input type="password" placeholder="Enter your password" />
        </div>
        <button className="login-btn">Sign In</button>
      </div>
    </div>
  );
};

export default LoginAdmin;
