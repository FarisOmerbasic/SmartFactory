import React from "react";
import Menu from "./menu"; 
import "./Production.css";

const Production = () => {
    return (
        <div className="dashboard-container">
            <Menu /> 
            <main className="main-content">
                <h1>Real-time Production Metrics</h1>

                <div className="cards">
                    <div className="card">
                        <p>Active</p>
                        <h2>Line A Throughput</h2>
                        <small>92 units/hour</small>
                    </div>
                    <div className="card">
                        <p>Active</p>
                        <h2>Line B Throughput</h2>
                        <small>87 units/hour</small>
                    </div>
                    <div className="card warning">
                        <p>Warning</p>
                        <h2>Line C Throughput</h2>
                        <small>45 units/hour</small>
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
                                <th>Target Deviation</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Line C</td>
                                <td>Machine M103</td>
                                <td>Slow Feed Rate</td>
                                <td>45% Efficiency</td>
                                <td className="warning">-47% vs Target</td>
                            </tr>
                            <tr>
                                <td>Line B</td>
                                <td>Machine M205</td>
                                <td>Idle</td>
                                <td>0% Efficiency</td>
                                <td className="critical">-100% vs Target</td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <h2>Predictive Output Analysis</h2>
                <div className="cards">
                    <div className="card">
                        <p>Today's Projected Output</p>
                        <h2>2,150 units</h2>
                        <small>93% of target</small>
                    </div>
                    <div className="card">
                        <p>Week's Projection</p>
                        <h2>10,500 units</h2>
                        <small>89% of target</small>
                    </div>
                </div>

                <button className="report-btn">Generate Detailed Report</button>
            </main>
        </div>
    );
};

export default Production;
