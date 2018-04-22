import 'bootstrap';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { BrowserRouter as Router } from 'react-router-dom';
import Routes from './components/Routes';

// Polyfills
import 'whatwg-fetch';
import './polyfills/object-assign';
import './polyfills/array-find';

// Styles
import '../node_modules/bootstrap/dist/css/bootstrap.css';
import './css/site.css';

ReactDOM.render(
    <Router>
        <Routes />
    </Router>,
    document.getElementById("app")
);

// Allow Hot Module Reloading
if (module.hot) {
    module.hot.accept();
}