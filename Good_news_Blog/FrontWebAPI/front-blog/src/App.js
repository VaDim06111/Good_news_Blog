import React from 'react';
import { BrowserRouter} from 'react-router-dom';
import '@fortawesome/fontawesome-free/css/all.min.css';
import 'bootstrap-css-only/css/bootstrap.min.css';
import 'mdbreact/dist/css/mdb.css';

import NavbarComponent from './components/shared/navbar/navbar';
import CardDeck from './components/cardDeck';
import PaginationComponent from './components/pagination';
import FooterComponent from './components/shared/footer/footer';
    

class App extends React.Component {

    render() {
        return(
                <BrowserRouter>
                    <NavbarComponent />
                    <CardDeck/>
                    <PaginationComponent />
                    <FooterComponent />
                </BrowserRouter> 
        )
    }
}

export default App;