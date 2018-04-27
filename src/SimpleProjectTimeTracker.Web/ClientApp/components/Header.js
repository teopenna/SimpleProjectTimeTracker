import React, { Component } from "react";
import { Link, Redirect } from 'react-router-dom';
import { RouteComponentProps } from 'react-router';

export class Header extends Component {
    render() {
        return (
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <a className="navbar-brand" href="#">Simple Project Time Tracker</a>
                <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>

                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav mr-auto">
                        <li className="nav-item active">
                            <a className="nav-link" href="#">Home <span className="sr-only">(current)</span></a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" href="/timeregistrations">Time Registrations</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" href="/projects">Projects</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" href="/invoices">Invoices</a>
                        </li>
                    </ul>
                </div>
            </nav>
        );
    }
}

Header.displayName = 'Header';