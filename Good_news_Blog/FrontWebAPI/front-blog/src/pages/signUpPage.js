import React from "react";
import { MDBContainer, 
    MDBRow, 
    MDBCol, 
    MDBInput, 
    MDBBtn } from 'mdbreact';
import { BrowserRouter } from 'react-router-dom';
import NavbarMain from '../components/shared/navbar/navbarMain';

class SignUpPage extends React.Component {

    render() {
        return(
            <BrowserRouter>
            <NavbarMain />
            <div className="purple lighten-2 mt-0 pt-0" style = {{height:"100vh"}}>
            <MDBContainer>
                <MDBRow className="text-white">
                    <MDBCol md="6" className="mx-auto" style={{marginTop:'20%'}}>
                    <form>
                        <p className="h5 text-center mb-4">Регистрация</p>
                        <div className="grey-text">
                        <MDBInput className="text-white"
                            label="Ваше имя"
                            labelClass="text-white"
                            icon="user"
                            group
                            type="text"
                            validate
                            error="wrong"
                            success="right"
                        />
                        <MDBInput className="text-white"
                            label="Ваш email"
                            labelClass="text-white"
                            icon="envelope"
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
                            group
                            type="password"
                            validate
                        />
                        <MDBInput className="text-white"
                            label="Подтвердите ваш пароль"
                            labelClass="text-white"
                            icon="exclamation-triangle"
                            group
                            type="password"
                            validate
                            error="wrong"
                            success="right"
                        />
                        </div>
                        <div className="text-center">
                        <MDBBtn>Зарегистрироваться</MDBBtn>
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

export default SignUpPage;