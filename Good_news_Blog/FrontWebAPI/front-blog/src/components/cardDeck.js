import React from 'react';
//import Card from './components/card';
import { MDBBtn,
    MDBCard, 
    MDBCardGroup,
    MDBCardBody, 
    MDBCardImage, 
    MDBCardTitle, 
    MDBCardText, 
    MDBCol } from 'mdbreact';

function CardDeck(props) {
    const cards = props.cards;
    const listItems = cards.map((card) =>
      <MDBCol className="m-1 pt-5 pb-4">{card}</MDBCol>
    );
    return (
      <MDBCardGroup>{listItems}</MDBCardGroup>
    );
  }

class cardDeck extends React.Component {

    render() {
        const cards = [
                            <MDBCard size='4' style={{ width: "22rem" }}>
                            <MDBCardImage className="img-fluid" src="https://mdbootstrap.com/img/Photos/Others/images/43.jpg" waves />
                            <MDBCardBody>
                                <MDBCardTitle>Card title</MDBCardTitle>
                                <MDBCardText>
                                    Some quick example text to build on the card title and make
                                up the bulk of the card&apos;s content.
                                </MDBCardText>
                            <MDBBtn href="#">Читать...</MDBBtn>
                            </MDBCardBody>
                        </MDBCard>, 
                                <MDBCard style={{ width: "22rem" }}>
                                <MDBCardImage className="img-fluid" src="https://mdbootstrap.com/img/Photos/Others/images/43.jpg" waves />
                                <MDBCardBody>
                                <MDBCardTitle>Card title</MDBCardTitle>
                                <MDBCardText>
                                    Some quick example text to build on the card title and make
                                    up the bulk of the card&apos;s content.
                                </MDBCardText>
                                <MDBBtn href="#">Читать...</MDBBtn>
                            </MDBCardBody>
                        </MDBCard>,
                        <MDBCard size='4' style={{ width: "22rem" }}>
                            <MDBCardImage className="img-fluid" src="https://mdbootstrap.com/img/Photos/Others/images/43.jpg" waves />
                            <MDBCardBody>
                                <MDBCardTitle>Card title</MDBCardTitle>
                                <MDBCardText>
                                    Some quick example text to build on the card title and make
                                up the bulk of the card&apos;s content.
                                </MDBCardText>
                            <MDBBtn href="#">Читать...</MDBBtn>
                            </MDBCardBody>
                        </MDBCard>, 
                                <MDBCard style={{ width: "22rem" }}>
                                <MDBCardImage className="img-fluid" src="https://mdbootstrap.com/img/Photos/Others/images/43.jpg" waves />
                                <MDBCardBody>
                                <MDBCardTitle>Card title</MDBCardTitle>
                                <MDBCardText>
                                    Some quick example text to build on the card title and make
                                    up the bulk of the card&apos;s content.
                                </MDBCardText>
                                <MDBBtn href="#">Читать...</MDBBtn>
                            </MDBCardBody>
                        </MDBCard>]
        return(
            <div id="news">
                <CardDeck className="d-flex justify-content-center" cards={cards}/>
            </div>     
        )
    }
}

export default cardDeck;