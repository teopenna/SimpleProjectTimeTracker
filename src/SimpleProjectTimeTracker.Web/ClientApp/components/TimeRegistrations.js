import * as React from 'react';
import ReactDOM from 'react-dom';
import { Link } from 'react-router-dom';
import axios from 'axios';

export class TimeRegistrations extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            timeRegistrations: []
        };
    }

    componentDidMount() {
        axios.get('api/timeregistrations')
            .then(res => {
                this.setState({ timeRegistrations: res.data });
            });
    }

    delete(timeRegistration) {
        console.log('Delete TR id ' + timeRegistration.id);
    }

    render() {
        return (
            <div className="container">
                <div className="panel panel-default">
                    <div className="panel-heading">
                        <h3 className="panel-title">Time Registrations</h3>
                    </div>
                    <div className="panel-body">
                        <Link className="btn btn-success" to="/create"><span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span> Add Time Registration</Link>
                        <table className="table table-stripe">
                            <thead>
                                <tr>
                                    <th>Project</th>
                                    <th>Date</th>
                                    <th>Hours Worked</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.state.timeRegistrations.map(timeRegistration => 
                                    <tr>
                                        <td>{timeRegistration.projectName}</td>
                                        <td>{timeRegistration.date}</td>
                                        <td>{timeRegistration.hoursWorked}</td>
                                        <td>
                                            <Link to={'/timeregistrations/' + timeRegistration.id.toString()}>edit</Link>
                                            <button type="button" className="btn btn-link" onClick={(e) => this.delete(timeRegistration)}>delete</button>
                                        </td>
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