import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { useState, useEffect } from "react";
import SplashScreen from "./SplashScreen"; 
import ProtectedRoute from "./ProtectedRoute";
import Dashboard from "./Dashboard";
import Machine from "./Machine";
import Energy from "./Energy";
import Maintenance from "./Maintenance";
import Production from "./Production";
import Room from "./Room";
import Login from "./Login";
import AddUser from "./AddUser";

function App() {
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        setTimeout(() => setLoading(false), 3000); // Hide splash after 3s
    }, []);

    return (
        <Router>
            {loading ? (
                <SplashScreen />
            ) : (
                <Routes>
                    {/* Public Routes */}
                    <Route path="/" element={<Login />} />
                    <Route path="/login" element={<Login />} />

                    {/* Protected Routes */}
                    <Route path="/dashboard" element={<ProtectedRoute element={<Dashboard />} allowedRoles={["SuperUser", "Maintenance", "FactoryManager", "Supervisors", "Administrator", "Operations", "User"]} />} />
                    <Route path="/machine" element={<ProtectedRoute element={<Machine />} allowedRoles={["SuperUser", "FactoryManager", "Maintenance", "Supervisors", "Administrator"]} />} />
                    <Route path="/energy" element={<ProtectedRoute element={<Energy />} allowedRoles={["Operations", "Administrator", "SuperUser", "FactoryManager"]} />} />
                    <Route path="/maintenance" element={<ProtectedRoute element={<Maintenance />} allowedRoles={["SuperUser", "FactoryManager", "Maintenance", "Administrator"]} />} />
                    <Route path="/production" element={<ProtectedRoute element={<Production />} allowedRoles={["SuperUser", "FactoryManager", "Administrator"]} />} />
                    <Route path="/room" element={<ProtectedRoute element={<Room />} allowedRoles={["SuperUser", "Maintenance", "FactoryManager", "Supervisors", "Administrator", "User"]} />} />
                    <Route path="/add-user" element={<ProtectedRoute element={<AddUser />} allowedRoles={["Administrator"]} />} />
                </Routes>
            )}
        </Router>
    );
}

export default App;
