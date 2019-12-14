import React from 'react';
import { BrowserRouter} from 'react-router-dom';

import NavbarMain from '../components/shared/navbar/navbarMain';
import { MDBContainer } from 'mdbreact';


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
        
        return(
            <BrowserRouter>
                <NavbarMain />
                <MDBContainer className="justify-content-center">
                    
                </MDBContainer>           
            </BrowserRouter>
        )
    }
}

export default ReadNewsPage;