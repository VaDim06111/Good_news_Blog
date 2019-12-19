import React from 'react';
import { MDBBtn,
    MDBCard, 
    MDBCardBody, 
    MDBCardImage, 
    MDBCardTitle, 
    MDBCardText, 
    MDBCol,
    MDBCardGroup} from 'mdbreact';

class CardDeck extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
          items: props.items
        };
      }

      componentDidUpdate(prevProps) {
        if(this.props !== prevProps) {
            this.setState({
                items: this.props.items
          })
        }
    }

    render() {
      const { items } = this.state;
        return(
            <div id="news">
              <MDBCardGroup className="ml-5">
                {items.map(item => (
                            <MDBCol key={item.id} className="m-1 pt-5 pb-4">
                              <MDBCard style={{ width: "22rem" }}>
                                  <MDBCardImage className="img-fluid" src="https://mdbootstrap.com/img/Photos/Others/images/43.jpg" waves />
                                  <MDBCardBody>
                                      <MDBCardTitle>{item.title}</MDBCardTitle>
                                      <MDBCardText>
                                          {item.description.length > 350 ? item.description.substr(0,350) + '...' : item.description}
                                      </MDBCardText>
                                      <MDBBtn href={'/'+ item.id}>Читать...</MDBBtn>
                                  </MDBCardBody>
                              </MDBCard>
                            </MDBCol>
                        ))}       
              </MDBCardGroup>                                 
            </div>     
        );
    }
}

export default CardDeck;