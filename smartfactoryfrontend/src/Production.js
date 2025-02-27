import React, { useEffect, useState } from "react";
import Menu from "./menu";
import "./Production.css";

const Production = () => {
    const [productionData, setProductionData] = useState({
        lineA: {},
        lineB: {},
        lineC: {},
    });

    const fetchProductionData = async (line) => {
        try {
            const response = await fetch(`http://localhost:5270/api/Production/GetProductionData/${line}`);
            if (!response.ok) {
                throw new Error("Network response was not ok");
            }
            const data = await response.json();
            return data;
        } catch (error) {
            console.error(`Error fetching data for ${line}:`, error);
            return null;
        }
    };

    useEffect(() => {
        const fetchAllProductionData = async () => {
            const lineAData = await fetchProductionData("Line A");
            const lineBData = await fetchProductionData("Line B");
            const lineCData = await fetchProductionData("Line C");

            setProductionData({
                lineA: lineAData || {},
                lineB: lineBData || {},
                lineC: lineCData || {},
            });
        };

        fetchAllProductionData();
    }, []);

    const generatePDF = async () => {
        try {
            const requestBody = {
                lineA: [productionData.lineA],
                lineB: [productionData.lineB],
                lineC: [productionData.lineC],
                todayOutput: productionData.lineA.todaysProjectedOutput || 0,
                weekOutput: productionData.lineA.weeksProjection || 0
            };

            const response = await fetch("http://localhost:5270/api/PDFReport/GeneratePdf", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(requestBody),
            });

            if (!response.ok) {
                throw new Error("Failed to generate PDF");
            }

            const blob = await response.blob();
            const pdfUrl = URL.createObjectURL(blob);
            window.open(pdfUrl);
        } catch (error) {
            console.error("Error generating PDF:", error);
        }
    };

    return (
        <div className="dashboard-container">
            <Menu />
            <main className="main-content">
                <div className="header-container">
                    <h1>Real-time Production Metrics</h1>
                    <button className="report-btn" onClick={generatePDF}>
                        Generate Detailed Report
                    </button>
                </div>

                <div className="cards">
                    <div className="card">
                        <p>Today's Projected Output</p>
                        <h2>{productionData.lineA.todaysProjectedOutput || 0} units</h2>
                        <small>{productionData.lineA.todaysProjectedOutput ? `${(productionData.lineA.todaysProjectedOutput / 2150 * 100).toFixed(2)}%` : '0%'} of target</small>
                    </div>
                    <div className="card">
                        <p>Week's Projection</p>
                        <h2>{productionData.lineA.weeksProjection || 0} units</h2>
                        <small>{productionData.lineA.weeksProjection ? `${(productionData.lineA.weeksProjection / 10500 * 100).toFixed(2)}%` : '0%'} of target</small>
                    </div>
                </div>

                <h2>Production Bottlenecks</h2>
                <div className="table-container">
                    <table className="system-health">
                        <thead>
                            <tr>
                                <th>Line</th>
                                <th>Machine</th>
                                <th>Issue</th>
                                <th>Efficiency</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Line A</td>
                                <td>{productionData.lineA.machine || "No Machine Assigned"}</td>
                                <td>{productionData.lineA.issue || "No Issue"}</td>
                                <td>{productionData.lineA.efficiency || 0}% Efficiency</td>
                            </tr>
                            <tr>
                                <td>Line B</td>
                                <td>{productionData.lineB.machine || "No Machine Assigned"}</td>
                                <td>{productionData.lineB.issue || "No Issue"}</td>
                                <td>{productionData.lineB.efficiency || 0}% Efficiency</td>
                            </tr>
                            <tr>
                                <td>Line C</td>
                                <td>{productionData.lineC.machine || "No Machine Assigned"}</td>
                                <td>{productionData.lineC.issue || "No Issue"}</td>
                                <td>{productionData.lineC.efficiency || 0}% Efficiency</td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <h2>Real-time Production Metrics</h2>
                <div className="cards">
                    <div className="card">
                        <p>Active</p>
                        <h2>Line A Throughput</h2>
                        <small>{productionData.lineA.throughput || 0} units/hour</small>
                    </div>
                    <div className="card">
                        <p>Active</p>
                        <h2>Line B Throughput</h2>
                        <small>{productionData.lineB.throughput || 0} units/hour</small>
                    </div>
                    <div className="card warning">
                        <p>Warning</p>
                        <h2>Line C Throughput</h2>
                        <small>{productionData.lineC.throughput || 0} units/hour</small>
                    </div>
                </div>
            </main>
        </div>
    );
};

export default Production;
