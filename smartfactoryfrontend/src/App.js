import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import ProtectedRoute from "./ProtectedRoute";
import Dashboard from "./Dashboard";
import Machine from "./Machine";
import Energy from "./Energy";
import Maintenance from "./Maintenance";
import Production from "./Production";
import Login from "./Login";

function App() {
  return (
    <Router>
      <Routes>
        {/* Public Routes */}
        <Route path="/" element={<Login />} />
        <Route path="/login" element={<Login />} />

        {/* Protected Routes */}
        <Route 
          path="/dashboard" 
          element={<ProtectedRoute element={<Dashboard />} allowedRoles={["SuperUser", "Maintenance", "FactoryManager", "Supervisors", "Administrator"]} />} 
        />
        <Route 
          path="/machine" 
          element={<ProtectedRoute element={<Machine />} allowedRoles={["SuperUser", "FactoryManager", "Supervisors", "Administrator"]} />} 
        />
        <Route 
          path="/energy" 
          element={<ProtectedRoute element={<Energy />} allowedRoles={["SuperUser", "FactoryManager", "Administrator"]} />} 
        />
        <Route 
          path="/maintenance" 
          element={<ProtectedRoute element={<Maintenance />} allowedRoles={["Maintenance", "SuperUser", "Administrator"]} />} 
        />
        <Route 
          path="/production" 
          element={<ProtectedRoute element={<Production />} allowedRoles={["SuperUser", "FactoryManager", "Administrator"]} />} 
        />
      </Routes>
    </Router>
  );
}

export default App;
