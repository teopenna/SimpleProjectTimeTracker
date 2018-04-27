﻿import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Link } from 'react-router-dom';
import SimpleProjectTimeTrackerService from '../services/SimpleProjectTimeTrackerService';
import Octicon from 'react-octicon';
import moment from 'moment';

export class Invoices extends Component {
    constructor(props) {
        super(props);
        this.state = {
            invoices: []
        };

        this.SimpleProjectTimeTrackerService = new SimpleProjectTimeTrackerService('api');
    }

    componentDidMount() {
        this.SimpleProjectTimeTrackerService.getInvoices()
            .then(response => {
                const invoices = response.data;
                this.setState({ invoices: invoices });
            });
    }
    
    render() {
        return (
            <div className="container">
                <div className="panel panel-default">
                    <div className="panel-heading">
                        <h3 className="panel-title">Invoices</h3>
                    </div>
                    <div className="panel-body">
                        <table className="table table-stripe">
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Customer</th>
                                    <th>Net Amount</th>
                                    <th>Gross Amount</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.state.invoices.map(invoice =>
                                    <tr key={invoice.id}>
                                        <td>moment(invoice.date).format('L')}</td>
                                        <td>{invoice.customerName}</td>
                                        <td>{timeRegistration.netAmount}</td>
                                        <td>{timeRegistration.grossAmount}</td>
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