import React from "react";
import { MDBContainer, 
    MDBRow, 
    MDBCol, 
    MDBInput, 
    MDBBtn } from 'mdbreact';
import { BrowserRouter, Redirect } from 'react-router-dom';
import NavbarMain from '../components/shared/navbar/navbarMain';
import Footer from '../components/shared/footer/footer';
import { authenticationService } from '../services/authenticationService';

class SignInPage extends React.Component {
    
    constructor(props) {
        super(props);
        this.state = {
            redirect: false
        }
        this.submitHandler = this.submitHandler.bind(this);
    }

    submitHandler = event => {
        event.preventDefault();
        event.target.className += " was-validated";

        let email = this.loginInput.state.innerValue;
        let password = this.passwordInput.state.innerValue;
        authenticationService.login(email, password)
            .then(
                user => {
                    this.setState({
                        redirect: true
                    })
                }
            );
    };

    changeHandler = event => {
        this.setState({ [event.target.name]: event.target.value });
    };

    render() {
        const { redirect } = this.state;

        if(redirect) {
            return <Redirect to="/" />;
        } else {
            return(
                <BrowserRouter>
                <NavbarMain />
                <div className="purple lighten-2 mt-0 pt-0" style = {{height:"61.1vh"}}>
                <MDBContainer>
                            <MDBRow className="text-white">
                                <MDBCol md="6" className="mx-auto" style={{marginTop:'20%'}}>
                                <form className="needs-validation" onSubmit={this.submitHandler} noValidate>
                                    <p className="h5 text-center mb-4">Войти</p>
                                    <div className="grey-text">
                                    <MDBInput className="text-white" id="loginInput" name="login"
                                        label="Ваш email"
                                        labelClass="text-white"
                                        icon="envelope"
                                        size="lg"
                                        group
                                        type="email"
                                        validate
                                        required
                                        onChange={this.changeHandler}
                                        ref={ ref => this.loginInput = ref}
                                    />
                                    <MDBInput className="text-white" id="passwordInput" name="password"
                                        label="Ваш пароль"
                                        labelClass="text-white"
                                        icon="lock"
                                        size="lg"
                                        group
                                        type="password"
                                        validate
                                        required
                                        onChange={this.changeHandler}
                                        ref={ ref => this.passwordInput = ref}
                                    />
                                    </div>
                                    <div className="text-center">
                                    <MDBBtn type="submit" >Войти</MDBBtn>
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
}

export default SignInPage;