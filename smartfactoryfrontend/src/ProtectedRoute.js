import React from "react";
import { Navigate } from "react-router-dom";

const ProtectedRoute = ({ element, allowedRoles }) => {
  const user = JSON.parse(localStorage.getItem("user")); // Get user from local storage

  if (!user) {
    return <Navigate to="/login" />; // Redirect to login if not logged in
  }

  if (!allowedRoles.includes(user.role)) {
    alert(`❌ Access Denied! You do not have permission to view this page.`);
    return <Navigate to="/dashboard" />; // Redirect to dashboard if unauthorized
  }

  return element;
};

export default ProtectedRoute;
