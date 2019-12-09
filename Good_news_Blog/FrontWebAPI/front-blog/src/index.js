import React from 'react';
import ReactDOM from 'react-dom';
import '@fortawesome/fontawesome-free/css/all.min.css';
import 'bootstrap-css-only/css/bootstrap.min.css';
import 'mdbreact/dist/css/mdb.css';

import NavbarComponent from './components/shared/navbar/navbar';
import CardDeck from './components/cardDeck';
import PaginationComponent from './components/pagination';
import FooterComponent from './components/shared/footer/footer';
    

ReactDOM.render(<NavbarComponent />, document.getElementById('root'));
ReactDOM.render(<CardDeck />, document.getElementById('news'));
ReactDOM.render(<PaginationComponent />, document.getElementById('pagination'));
ReactDOM.render(<FooterComponent />, document.getElementById('footer'));

