import React from "react";
import { MDBContainer, 
    MDBRow, 
    MDBCol, 
    MDBBtn, 
    MDBIcon, 
    MDBInput } from 'mdbreact';

class FormContact extends React.Component {

    render() {
        return (
            <MDBContainer className="text-white">
            <p className="h5 mb-4">Напишите нам!</p>
              <MDBRow>
                <MDBCol md="6">
                  <form>
                    <div>
                      <MDBInput
                        label="Your email"
                        icon="envelope"
                        group
                        type="email"
                        labelClass='white-text'
                        validate
                        error="wrong"
                        success="right"
                      />
                      <MDBInput
                                label="Subject"
                                icon="tag"
                                group
                                type="text"
                                labelClass='white-text'
                                validate
                                error="wrong"
                                success="right"
                            />
                    </div>
                  </form>
                </MDBCol>
                <MDBCol md="6">
                    <form>
                        <div>
                            <MDBInput
                                type="textarea"
                                rows="1"
                                label="Your message"
                                labelClass='white-text'
                                icon="pencil-alt"
                            />
                        </div>
                    <div className="text-center">
                      <MDBBtn outline color="warning">
                        Send <MDBIcon far icon="paper-plane" />
                      </MDBBtn>
                    </div>
                  </form>
                </MDBCol>
              </MDBRow>
            </MDBContainer>
          );
    }
  
};

export default FormContact;