import React from "react";
import "./SplashScreen.css";
import logo from "./assets/logo512.png"; 

const SplashScreen = () => {
  return (
    <div className="splash-screen">
      <img src={logo} alt="SmartFactory Logo" className="splash-logo" />
      <h1 className="splash-text">SMART FACTORY</h1>
    </div>
  );
};

export default SplashScreen;
