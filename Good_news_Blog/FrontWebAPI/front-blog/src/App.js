import React from 'react';
import { BrowserRouter} from 'react-router-dom';

import NavbarComponent from './components/shared/navbar/navbar';
import CardDeck from './components/cardDeck';
import PaginationComponent from './components/pagination';
import FooterComponent from './components/shared/footer/footer';
import { authenticationService } from './services/authenticationService';

import Lottie from 'react-lottie';
import animationDataLoad from './assets/lego-loader.json';
import animationDataError from './assets/error-500.json';

class App extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
          error: null,
          isLoaded: false,
          page: 1,
          total: null,
          items: [],
          currentUser: authenticationService.currentUser
        };
       this.updateData = this.updateData.bind(this);
      }

      async componentDidMount() {
        await this.fetchData(this.state.page);
      }

     async fetchData(value) {
     fetch(`https://goodnewsblog.azurewebsites.net/api/home/${value}`)
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
        this.setState({ 
          page: value
        })
        this.fetchData(value);
     }

     updateState = (value) => {
      this.setState({
        currentUser: value
      })
    }

    render() {
        const { error, isLoaded, items, page, total } = this.state;

        const defaultOptionsLoad = {
          loop: true,
          autoplay: true, 
          animationData: animationDataLoad,
          rendererSettings: {
            preserveAspectRatio: 'xMidYMid slice'
          }
        };

        const defaultOptionsError = {
          loop: true,
          autoplay: true, 
          animationData: animationDataError,
          rendererSettings: {
            preserveAspectRatio: 'xMidYMid slice'
          }
        };

        if(error) {
            return(
               <div className="h-100 w-100 justify-content center" style={{marginTop:'10%'}}>
                  <Lottie options={defaultOptionsError} height={500} width={500} />
              </div> 
            )} else if(!isLoaded) {
            return <div className="h-100 w-100 justify-content center" style={{marginTop:'10%'}}>
                      <Lottie options={defaultOptionsLoad} height={500} width={500}  />
                  </div>;
        } else {
            return(
                <BrowserRouter>
                    <NavbarComponent updateState={this.updateState}/>
                    <CardDeck items={items}/>
                    <PaginationComponent page={page} total={total} updateData={this.updateData} />
                    <FooterComponent />
                </BrowserRouter> 
            )}
    }
}

export default App;