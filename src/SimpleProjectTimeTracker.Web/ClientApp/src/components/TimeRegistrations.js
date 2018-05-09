import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import TimeRegistrationService from '../services/TimeRegistrationService';
import InvoiceService from '../services/InvoiceService';
import moment from 'moment';
import checkmark from '../checkmark.svg';

export default class TimeRegistrations extends Component {
    constructor(props) {
        super(props);

        this.timeRegistrationService = new TimeRegistrationService();
        this.invoiceService = new InvoiceService();

        this.state = {
            timeRegistrations: []
        };
    }

    componentDidMount() {
        this.timeRegistrationService.fetchAll()
            .then((response) => {
                const timeRegistrations = response.content;
                this.setState({ timeRegistrations: timeRegistrations });
            });
    }

    delete(timeRegistration) {
        if (window.confirm(`Delete Time Registration with id ${timeRegistration.id}. Are you sure?`)) {
            this.timeRegistrationService.delete(timeRegistration.id).then((response) => {
                let updatedTimeRegistrations = this.state.timeRegistrations;
                updatedTimeRegistrations.splice(updatedTimeRegistrations.indexOf(timeRegistration), 1);
                this.setState({ timeRegistration: updatedTimeRegistrations });
            });
        }
    }

    createInvoices() {
        if (window.confirm('Do you really want to create invoices for not accounted time registrations?')) {
            this.invoiceService.createInvoices().then(response => {
                if (!response.is_error) {
                    this.props.history.push('/invoices');
                } else {
                    this.setState({ errors: response.error_content });
                }
            });
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