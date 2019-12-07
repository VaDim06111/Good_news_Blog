import React from 'react';
import ReactDOM from 'react-dom';
import '@fortawesome/fontawesome-free/css/all.min.css';
import 'bootstrap-css-only/css/bootstrap.min.css';
import 'mdbreact/dist/css/mdb.css';

import NavbarPage from './components/shared/navbar/navbar';
import CardDeck from './components/cardDeck';

ReactDOM.render(<NavbarPage />, document.getElementById('root'));
ReactDOM.render(<CardDeck />, document.getElementById('news'));

