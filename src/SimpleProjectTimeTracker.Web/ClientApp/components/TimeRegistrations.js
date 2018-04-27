import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Link } from 'react-router-dom';
import SimpleProjectTimeTrackerService from '../services/SimpleProjectTimeTrackerService';
import Octicon from 'react-octicon';
import moment from 'moment';

export class TimeRegistrations extends Component {
    constructor(props) {
        super(props);
        this.state = {
            timeRegistrations: []
        };

        this.SimpleProjectTimeTrackerService = new SimpleProjectTimeTrackerService('api');

        this.delete = this.delete.bind(this);
    }

    componentDidMount() {
        this.SimpleProjectTimeTrackerService.getTimeRegistrations()
            .then(response => {
                const timeRegistrations = response.data;
                this.setState({ timeRegistrations: timeRegistrations });
            });
    }

    delete(timeRegistration) {
        if (confirm('Delete Time Registration with id ' + timeRegistration.id + '. Are you sure?')) {
            this.SimpleProjectTimeTrackerService.deleteTimeRegistration(timeRegistration)
                .then(res => {
                    if (!res.is_error) {
                        this.props.history.push('/timeregistrations');
                    }
                    else {
                        alert('Error!');
                    }
                });
        }
    }

    render() {
        return (
            <div className="container">
                <div className="panel panel-default">
                    <div className="panel-heading">
                        <h3 className="panel-title">Time Registrations</h3>
                    </div>
                    <div className="panel-body">
                        <Link className="btn btn-primary" to="/timeregistrations/create">Add Time Registration</Link>
                        <table className="table table-stripe">
                            <thead>
                                <tr>
                                    <th>Project</th>
                                    <th>Customer</th>
                                    <th>Date</th>
                                    <th>Hours Worked</th>
                                    <th>Accounted</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.state.timeRegistrations.map(timeRegistration =>
                                    <tr key={timeRegistration.id}>
                                        <td>{timeRegistration.projectName}</td>
                                        <td>{timeRegistration.customerName}</td>
                                        <td>{moment(timeRegistration.date).format('L')}</td>
                                        <td>{timeRegistration.hoursWorked}</td>
                                        <td>{timeRegistration.accounted ? <Octicon mega name="check" /> : '' }</td>
                                        <td>{!timeRegistration.accounted ?
                                            (<span>
                                                <Link to={'/timeregistrations/edit/' + timeRegistration.id.toString()}>edit</Link>
                                                <button type="button" className="btn btn-link" onClick={(e) => this.delete(timeRegistration)}>delete</button></span>)
                                            : '' }
                                        </td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                        <Link className="btn btn-primary" to="/invoices/create">Create invoice for not accounted items</Link>
                    </div>
                </div>
            </div>
        );
    }
}