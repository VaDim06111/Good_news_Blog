import React from "react";
import { MDBContainer, 
    MDBRow, 
    MDBCol, 
    MDBInput, 
    MDBBtn } from 'mdbreact';
import { BrowserRouter } from 'react-router-dom';

class SignInPage extends React.Component {

    render() {
        return(
            <BrowserRouter>
            <MDBContainer>
                    <MDBRow style={{marginTop:'20%'}}>
                        <MDBCol md="6" className="mx-auto">
                        <form>
                            <p className="h5 text-center mb-4">Войти</p>
                            <div className="grey-text">
                            <MDBInput
                                label="Type your email"
                                icon="envelope"
                                group
                                type="email"
                                validate
                                error="wrong"
                                success="right"
                            />
                            <MDBInput
                                label="Type your password"
                                icon="lock"
                                group
                                type="password"
                                validate
                            />
                            </div>
                            <div className="text-center">
                            <MDBBtn>Войти</MDBBtn>
                            </div>
                        </form>
                        </MDBCol>
                    </MDBRow>
                </MDBContainer>
            </BrowserRouter>     
        )
    }
}

export default SignInPage;