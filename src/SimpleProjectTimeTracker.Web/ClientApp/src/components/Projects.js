import React, { Component } from 'react';
import moment from 'moment';

class Projects extends Component {
    constructor(props) {
        super(props);
        this.state = {
            projects: []
        };
    }

    componentDidMount() {
        this.props.fetchAllProjects()
            .then((response) => {
                const projects = response.content;
                this.setState({ projects: projects });
            });
    }

    render() {
        return (
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
                                <th>Hourly Rate</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.projects.map(project =>
                                <tr>
                                    <td>{project.name}</td>
                                    <td>{project.customerName}</td>
                                    <td>{moment(project.dueDate).format('L')}</td>
                                    <td>{project.hourlyRate} &euro;</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
        );
    }
}

export default Projects;