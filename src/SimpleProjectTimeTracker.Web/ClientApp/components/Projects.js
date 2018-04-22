﻿import * as React from 'react';
import ReactDOM from 'react-dom';
import { Link } from 'react-router-dom';
import axios from 'axios';

export class Projects extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            projects: []
        };
    }

    componentDidMount() {
        axios.get('api/projects')
            .then(res => {
                this.setState({ projects: res.data });
            });
    }

    render() {
        return (
            <div className="container">
                <div className="panel panel-default">
                    <div className="panel-heading">
                        <h3 className="panel-title">Projects</h3>
                    </div>
                    <div className="panel-body">
                        <table className="table table-stripe">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Customer</th>
                                    <th>Due Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.state.projects.map(project =>
                                    <tr>
                                        <td>{project.name}</td>
                                        <td>{project.customerName}</td>
                                        <td>{project.dueDate}</td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        );
    }
}