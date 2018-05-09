import React, { Component } from "react";
import { Route, Switch } from "react-router-dom";
import Header from './Header';
import TimeRegistrations from './TimeRegistrations';
import TimeRegistrationForm from './TimeRegistrationForm';
import Projects from './Projects';
import Invoices from './Invoices';

export default class Routes extends Component {
    render() {
        return <Switch>
            <DefaultLayout exact path="/" component={TimeRegistrations} />
            <DefaultLayout exact path="/timeregistrations" component={TimeRegistrations} />
            <DefaultLayout exact path="/timeregistrations/create" component={TimeRegistrationForm} />
            <DefaultLayout exact path="/timeregistrations/edit/:id" component={TimeRegistrationForm} />
            <DefaultLayout exact path="/projects" component={Projects} />
            <DefaultLayout exact path="/invoices" component={Invoices} />
        </Switch>;
    }
}

const DefaultLayout = ({ component: Component, ...rest }) => {
    return (
        <Route {...rest} render={props => (
            <div>
                <Header {...props} />
                <div className="container">
                    <Component {...props} />
                </div>
            </div>
        )} />
    );
};