import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';

import '@fortawesome/fontawesome-free/css/all.min.css';
import 'bootstrap-css-only/css/bootstrap.min.css';
import 'mdbreact/dist/css/mdb.css';

import Routes from './routes/routes';  

ReactDOM.render((
    <BrowserRouter>
        <Routes />
    </BrowserRouter>
), document.getElementById('root'));

