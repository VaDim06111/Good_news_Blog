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
          error: null,
          isLoaded: false,
          items: []
        };
      }
    
      componentDidMount() {

        fetch("https://localhost:44308/api/home/1")
          .then(res => res.json())
          .then(
            (result) => {
              this.setState({
                isLoaded: true,
                items: result
              });
            },
            (error) => {
              this.setState({
                isLoaded: true,
                error
              });
            }
          )
      }

    render() {
        const { error, isLoaded, items } = this.state;
            if (error) {
                return <div>Ошибка: {error.message}</div>;
              } else if (!isLoaded) {
                return <div>Загрузка...</div>;
              } else {
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
                                          {item.description}
                                      </MDBCardText>
                                      <MDBBtn href="#">Читать...</MDBBtn>
                                  </MDBCardBody>
                              </MDBCard>
                            </MDBCol>
                        ))}       
              </MDBCardGroup>                                 
            </div>     
        );
    }
}
}

export default CardDeck;