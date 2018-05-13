import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import moment from 'moment';
import checkmark from '../checkmark.svg';

class TimeRegistrations extends Component {
    constructor(props) {
        super(props);
        
        this.state = {
            timeRegistrations: []
        };
    }

    componentDidMount() {
        this.fetch();
    }

    fetch = async() => {
        const response = await this.props.fetchAllTimeRegistrations();
        const timeRegistrations = response.content;
        this.setState({ timeRegistrations: timeRegistrations });
    }

    delete = async(timeRegistration) => {
        if (window.confirm(`Delete Time Registration with id ${timeRegistration.id}. Are you sure?`)) {
            await this.props.deleteTimeRegistration(timeRegistration.id);
            let updatedTimeRegistrations = this.state.timeRegistrations;
            updatedTimeRegistrations.splice(updatedTimeRegistrations.indexOf(timeRegistration), 1);
            this.setState({ timeRegistration: updatedTimeRegistrations });
        }
    }

    createInvoices = async() => {
        if (window.confirm('Do you really want to create invoices for not accounted time registrations?')) {
            const response = await this.props.createInvoices();
            if (!response.is_error) {
                this.props.history.push('/invoices');
            } else {
                this.setState({ errors: response.error_content });
            }
        }
    }

    render() {
        return (
            <div className="panel panel-default">
                <div className="panel-heading">
                    <h3 className="panel-title">Time Registrations</h3>
                </div>
                <div className="panel-body">
                    <Link className="btn btn-primary" to="/timeregistrations/create">Add Time Registration</Link>
                    <table className="table table-stripe" data-testid="timeregistrationtable">
                        <thead>
                            <tr>
                                <th>Project</th>
                                <th>Customer</th>
                                <th>Date</th>
                                <th>Hours Worked</th>
                                <th>Accounted</th>
                                <th>Edit/Delete</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.timeRegistrations.map(timeRegistration =>
                                <tr key={timeRegistration.id}>
                                    <td>{timeRegistration.projectName}</td>
                                    <td>{timeRegistration.customerName}</td>
                                    <td>{moment(timeRegistration.date).format('L')}</td>
                                    <td>{timeRegistration.hoursWorked}</td>
                                    <td>{timeRegistration.accounted ? <img src={checkmark} width="30px" height="40px" alt="accounted" /> : ''}</td>
                                    <td>{!timeRegistration.accounted ?
                                        (<span>
                                            <Link to={`/timeregistrations/edit/${timeRegistration.id}`}>Edit</Link>
                                            <button type="button" className="btn btn-danger" onClick={(e) => this.delete(timeRegistration)}>Delete</button></span>)
                                        : ''}
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                    <button type="button" className="btn btn-primary" onClick={(e) => this.createInvoices()}>Create invoices for not accounted items</button>
                </div>
            </div>
        );
    }
}

TimeRegistrations.propTypes = {
    fetchAllTimeRegistrations: PropTypes.func.isRequired,
    deleteTimeRegistration: PropTypes.func.isRequired,
    createInvoices: PropTypes.func.isRequired
};

export default TimeRegistrations;