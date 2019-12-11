import React from "react";
import { MDBContainer, 
    MDBRow, 
    MDBCol, 
    MDBInput, 
    MDBBtn } from 'mdbreact';
import { BrowserRouter } from 'react-router-dom';
import NavbarMain from '../components/shared/navbar/navbarMain';

class SignInPage extends React.Component {

    render() {
        return(
            <BrowserRouter>
            <NavbarMain />
            <div className="night-fade-gradient mt-0 pt-0" style = {{height:"100vh"}}>
            <MDBContainer>
                        <MDBRow>
                            <MDBCol md="6" className="mx-auto" style={{marginTop:'20%'}}>
                            <form>
                                <p className="h5 text-center mb-4">Войти</p>
                                <div className="grey-text">
                                <MDBInput
                                    label="Ваш email"
                                    icon="envelope"
                                    group
                                    type="email"
                                    validate
                                    error="wrong"
                                    success="right"
                                />
                                <MDBInput
                                    label="Ваш пароль"
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
            </div>
                
            </BrowserRouter>     
        )
    }
}

export default SignInPage;