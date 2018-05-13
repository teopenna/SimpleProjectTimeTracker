import React, { Component } from "react";
import { Route, Switch } from "react-router-dom";
import Header from './Header';
import { fetchAllTimeRegistrations, fetchTimeRegistration, deleteTimeRegistration, saveTimeRegistration } from '../services/TimeRegistrationService';
import { createInvoices, fetchAllInvoices } from '../services/InvoiceService';
import { fetchAllProjects } from '../services/ProjectService';
import TimeRegistrations from './TimeRegistrations';
import TimeRegistrationForm from './TimeRegistrationForm';
import Projects from './Projects';
import Invoices from './Invoices';

class Routes extends Component {
    render() {
        return (
            <div>
                <Header />
                <div className="container">
                    <Switch>
                        <Route exact path="/" render={(props) => <TimeRegistrations {...props} 
                            fetchAllTimeRegistrations={fetchAllTimeRegistrations}
                            deleteTimeRegistration={deleteTimeRegistration}
                            createInvoices={createInvoices}
                            />} 
                        />
                        <Route exact path="/timeregistrations" render={(props) => <TimeRegistrations {...props} 
                            fetchAllTimeRegistrations={fetchAllTimeRegistrations}
                            deleteTimeRegistration={deleteTimeRegistration} 
                            createInvoices={createInvoices}
                            />} 
                        />
                        <Route exact path="/timeregistrations/create" render={(props) => <TimeRegistrationForm {...props} 
                            fetchAllProjects={fetchAllProjects}
                            fetchTimeRegistration={fetchTimeRegistration}
                            saveTimeRegistration={saveTimeRegistration}
                            />} 
                        />
                        <Route exact path="/timeregistrations/edit/:id" render={(props) => <TimeRegistrationForm {...props} 
                            fetchAllProjects={fetchAllProjects}
                            fetchTimeRegistration={fetchTimeRegistration}
                            saveTimeRegistration={saveTimeRegistration}
                            />} 
                        />
                        <Route exact path="/projects" render={(props) => <Projects {...props} 
                            fetchAllProjects={fetchAllProjects}
                            />} 
                        />
                        <Route exact path="/invoices" render={(props) => <Invoices {...props} 
                            fetchAllInvoices={fetchAllInvoices}
                            />} 
                        />
                    </Switch>
                </div>
            </div>
        );
    }
}

export default Routes;