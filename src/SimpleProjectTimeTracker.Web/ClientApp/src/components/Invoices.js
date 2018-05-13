import React, { Component } from 'react';
import moment from 'moment';

class Invoices extends Component {
    constructor(props) {
        super(props);
        this.state = {
            invoices: []
        };
    }

    componentDidMount() {
        this.props.fetchAllInvoices()
            .then((response) => {
                const invoices = response.content;
                this.setState({ invoices: invoices });
            });
    }

    render() {
        return (
            <div className="panel panel-default">
                <div className="panel-heading">
                    <h3 className="panel-title">Invoices</h3>
                </div>
                <div className="panel-body">
                    <table className="table table-stripe">
                        <thead>
                            <tr>
                                <th>Number</th>
                                <th>Date</th>
                                <th>Customer</th>
                                <th>Net Amount</th>
                                <th>VAT Perc.</th>
                                <th>Gross Amount</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.invoices.map(invoice =>
                                <tr key={invoice.id}>
                                    <td>{invoice.number}</td>
                                    <td>{moment(invoice.date).format('L')}</td>
                                    <td>{invoice.customerName}</td>
                                    <td>{invoice.netAmount} &euro;</td>
                                    <td>{invoice.vatPercentage}</td>
                                    <td>{invoice.grossAmount} &euro;</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
        );
    }
}

export default Invoices;