import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';

class Header extends Component {
    render() {
        return (
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <div className="container">
                    <NavLink className="navbar-brand" to="/">Simple Project Time Tracker</NavLink>
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul className="navbar-nav mr-auto">
                            <li className="nav-item">
                                <NavLink className="nav-link" activeClassName="active" to="/timeregistrations">Time Registrations</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link" activeClassName="active" to="/projects">Projects</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link" activeClassName="active" to="/invoices">Invoices</NavLink>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        );
    }
}

export default Header;