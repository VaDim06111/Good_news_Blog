import React from 'react';
import { BrowserRouter} from 'react-router-dom';

import NavbarMain from '../components/shared/navbar/navbarMain';
import Footer from '../components/shared/footer/footer';

import Lottie from 'react-lottie';
import animationDataLoad from '../assets/lego-loader.json';
import animationDataError500 from '../assets/error-500.json';
import animationDataError404 from '../assets/error-404.json';

import { MDBRow,
    MDBView,
    MDBMask,
    MDBCardBody, 
    MDBCol,
    MDBContainer} from 'mdbreact';



class ReadNewsPage extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            error: null,
            isLoaded: false,
            news: null,
            comments: null
        }
    }
    
    
    async componentDidMount() {
        const { id } = this.props.match.params;

        await fetch(`https://localhost:44308/api/comment/${id}`)
        .then(res => res.json())
        .then(
          (result) => {
            this.setState({
              isLoaded: true,
              news : result.news,
              comments: result.comments
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

    render() {
      const { error, isLoaded, news, comments } = this.state;

      const defaultOptionsLoad = {
        loop: true,
        autoplay: true, 
        animationData: animationDataLoad,
        rendererSettings: {
          preserveAspectRatio: 'xMidYMid slice'
        }
      };

      const defaultOptionsError500 = {
        loop: true,
        autoplay: true, 
        animationData: animationDataError500,
        rendererSettings: {
          preserveAspectRatio: 'xMidYMid slice'
        }
      };

      const defaultOptionsError404 = {
        loop: true,
        autoplay: true, 
        animationData: animationDataError404,
        rendererSettings: {
          preserveAspectRatio: 'xMidYMid slice'
        }
      };

      if(error) {
        return(
            <div className="h-100 w-100 justify-content center" style={{marginTop:'10%'}}>
                  <Lottie options={defaultOptionsError500} height={500} width={500} />
              </div> 
        )} else if(!isLoaded) {
        return <div className="h-100 w-100 justify-content center" style={{marginTop:'10%'}}>
                  <Lottie options={defaultOptionsLoad} height={500} width={500} />
              </div>;
    } else if(news === null) {
        return <div className="h-100 w-100 justify-content center" style={{marginTop:'10%'}}>
                <Lottie options={defaultOptionsError404} height={300} width={500}/>
              </div>;
    } else {
        return(
            <BrowserRouter>
                <NavbarMain />
                <MDBRow>
                  <MDBCol className="justify-content-center">
                    <MDBView hover cascade zoom>
                      <img
                        src="https://mdbootstrap.com/img/Photos/Slides/img%20(142).jpg"
                        alt="News"
                        className="img-fluid"
                      />
                      <MDBMask overlay="white-slight" className="waves-light" />
                      </MDBView>
                      <MDBCardBody cascade className="text-center">
                      <h2 className="font-weight-bold">
                        <p><strong>{news.title}</strong></p>
                      </h2>
                      <p>
                        Источник:
                        <a href={news.source} rel='noopener noreferrer' target='_blank'>
                          <strong> Перейти к источнику</strong>
                        </a>
                        , {new Date(news.datePublication).toLocaleDateString([],{ year: 'numeric', month: 'long', day: 'numeric' })} 
                        {new Date(news.datePublication).toLocaleTimeString()}
                      </p>
                      </MDBCardBody>
                      <MDBContainer className="mt-5">
                        <p style={{fontSize: '1.5rem', whiteSpace: 'pre-wrap'}}>{news.text}</p>
                      </MDBContainer>
                  </MDBCol>
                </MDBRow> 
                <Footer />          
            </BrowserRouter>
        )
    }
}
}

export default ReadNewsPage;