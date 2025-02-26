import React from "react";
import "./Login.css";

const Login = () => {
  return (
    <div className="login-container">
      <h1 className="title">Production Throughput</h1>
      <div className="login-box">
        <h2 className="welcome">Welcome back</h2>
        <p className="subtitle">
          Please enter your credentials to access the dashboard
        </p>
        <div className="input-group">
          <label>Email</label>
          <input type="email" placeholder="Enter your email" />
        </div>
        <div className="input-group">
          <label>Password</label>
          <input type="password" placeholder="Enter your password" />
        </div>
        <button className="login-btn">Sign in</button>
      </div>
    </div>
  );
};

export default Login;
