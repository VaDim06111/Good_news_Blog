import React from "react";
import { MDBContainer, 
    MDBRow, 
    MDBCol, 
    MDBInput, 
    MDBBtn } from 'mdbreact';
import { BrowserRouter } from 'react-router-dom';
import NavbarMain from '../components/shared/navbar/navbarMain';
import Footer from '../components/shared/footer/footer';

class SignInPage extends React.Component {

    render() {
        return(
            <BrowserRouter>
            <NavbarMain />
            <div className="purple lighten-2 mt-0 pt-0" style = {{height:"61.1vh"}}>
            <MDBContainer>
                        <MDBRow className="text-white">
                            <MDBCol md="6" className="mx-auto" style={{marginTop:'20%'}}>
                            <form>
                                <p className="h5 text-center mb-4">Войти</p>
                                <div className="grey-text">
                                <MDBInput className="text-white"
                                    label="Ваш email"
                                    labelClass="text-white"
                                    icon="envelope"
                                    size="lg"
                                    group
                                    type="email"
                                    validate
                                    error="wrong"
                                    success="right"
                                />
                                <MDBInput className="text-white"
                                    label="Ваш пароль"
                                    labelClass="text-white"
                                    icon="lock"
                                    size="lg"
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
             <Footer />   
            </BrowserRouter>     
        )
    }
}

export default SignInPage;