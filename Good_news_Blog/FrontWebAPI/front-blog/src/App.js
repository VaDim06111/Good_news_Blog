import React from 'react';
import { BrowserRouter} from 'react-router-dom';


import NavbarComponent from './components/shared/navbar/navbar';
import CardDeck from './components/cardDeck';
import PaginationComponent from './components/pagination';
import FooterComponent from './components/shared/footer/footer';
    

class App extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
          error: null,
          isLoaded: false,
          page: 1,
          total: null,
          items: []
        };
      }

      async componentDidMount() {
        await fetch(`https://localhost:44308/api/home/${this.state.page}`)
        .then(res => res.json())
        .then(
          (result) => {
            this.setState({
              isLoaded: true,
              items : result.news,
              total: result.countPages
            });
          },
          (error) => {
            this.setState({
              isLoaded: true,
              error
            })
          }
        );
    }

    updateData = (value) => {
        this.setState({ page: value })
     }

    render() {
        const { error, isLoaded, items, page, total } = this.state;
        if(error) {
            return(
                <BrowserRouter>
                    <NavbarComponent />
                    <div>Ошибка: {error.message}</div>;
                    <FooterComponent />
                </BrowserRouter> 
            )} else if(!isLoaded) {
            return <div>Загрузка...</div>;
        } else {
            return(
                <BrowserRouter>
                    <NavbarComponent />
                    <CardDeck items={items}/>
                    <PaginationComponent page={page} total={total} updateData={this.updateData} />
                    <FooterComponent />
                </BrowserRouter> 
            )}
    }
}

export default App;