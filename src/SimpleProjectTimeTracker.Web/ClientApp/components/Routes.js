import React, { Component } from "react";
import { Route, Switch } from "react-router-dom";
import { Home } from "./Home";
import { Header } from './Header';
import { TimeRegistrations } from './TimeRegistrations';
import { TimeRegistrationForm } from './TimeRegistrationForm';
import { Projects } from './Projects';

export default class Routes extends Component {
    render() {
        return <Switch>
            <DefaultLayout exact path="/" component={TimeRegistrations} />
            <DefaultLayout exact path="/timeregistrations" component={TimeRegistrations} />
            <DefaultLayout exact path="/timeregistrations/create" component={TimeRegistrationForm} />
            <DefaultLayout exact path="/timeregistrations/edit/:id" component={TimeRegistrationForm} />
            <DefaultLayout exact path="/projects" component={Projects} />
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
