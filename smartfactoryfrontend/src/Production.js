import React from "react";
import "./Production.css";

const Production = () => {
    return (
        <div className="production-page">
            <aside className="sidebar">
                <h2>SmartFactory</h2>
                <nav>
                    <ul>
                        <li>Overview</li>
                        <li>Machines</li>
                        <li>Energy</li>
                        <li className="active">Production</li>
                        <li>Maintenance</li>
                    </ul>
                </nav>
                <div className="user-info">
                    <p>John Smith</p>
                    <span>Production Manager</span>
                </div>
            </aside>
            
            <main className="main-content">
                <h1>Real-time Production Metrics</h1>
                <div className="metrics">
                    <div className="card">
                        <p>Active</p>
                        <h2>Line A Throughput</h2>
                        <span>92 units/hour</span>
                    </div>
                    <div className="card">
                        <p>Active</p>
                        <h2>Line B Throughput</h2>
                        <span>87 units/hour</span>
                    </div>
                    <div className="card warning">
                        <p>Warning</p>
                        <h2>Line C Throughput</h2>
                        <span>45 units/hour</span>
                    </div>
                </div>
                
                <h2>Production Bottlenecks</h2>
                <table className="bottlenecks-table">
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
                            <td>-47% vs Target</td>
                        </tr>
                        <tr>
                            <td>Line B</td>
                            <td>Machine M205</td>
                            <td>Idle</td>
                            <td>0% Efficiency</td>
                            <td>-100% vs Target</td>
                        </tr>
                    </tbody>
                </table>
                
                <h2>Predictive Output Analysis</h2>
                <div className="output-analysis">
                    <div className="analysis-card">
                        <p>Today's Projected Output</p>
                        <h3>2,150 units (93% of target)</h3>
                    </div>
                    <div className="analysis-card">
                        <p>Week's Projection</p>
                        <h3>10,500 units (89% of target)</h3>
                    </div>
                </div>
                
                <button className="report-btn">Generate Detailed Report</button>
            </main>
        </div>
    );
};

export default Production;