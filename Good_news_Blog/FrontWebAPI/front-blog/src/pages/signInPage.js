import React from "react";
import { MDBContainer, 
    MDBRow, 
    MDBCol, 
    MDBInput, 
    MDBBtn, 
    MDBAlert} from 'mdbreact';
import { BrowserRouter, Redirect } from 'react-router-dom';
import NavbarMain from '../components/shared/navbar/navbarMain';
import Footer from '../components/shared/footer/footer';
import { authenticationService } from '../services/authenticationService';

class SignInPage extends React.Component {
    
    constructor(props) {
        super(props);
        this.state = {
            redirect: false,
            error: false
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
                (user) => {
                    if(user !== null) {
                        this.setState({
                            redirect: true
                        })
                    } else {
                        this.setState({
                            error: true
                        })
                    }
                   
                }
            );
    };


    render() {
        const { redirect, error } = this.state;

        if(redirect) {
            return <Redirect to="/" />;
        } else {
            return(
                <BrowserRouter>
                <NavbarMain />
                <div className="purple lighten-2 mt-0 pt-0" style = {{height:"75vh"}}>
                <MDBContainer>
                            <MDBRow className="text-white">
                                <MDBCol md="6" className="mx-auto" style={{marginTop:'10%'}}>
                                <form onSubmit={this.submitHandler} >
                                    <p className="h5 text-center mb-4">Войти</p>
                                    <div className="grey-text">
                                    <MDBInput className="text-white" id='loginInput'
                                        label="Ваш email"
                                        labelClass="text-white"
                                        icon="envelope"
                                        size="sm"
                                        group
                                        type="email"
                                        validate
                                        error="wrong"
                                        success="right"
                                        ref={ref => this.loginInput = ref}
                                    />
                                    <MDBInput className={'text-white' + (error ? ' is-invalid' : '')} id="passwordInput" name="password"
                                        label="Ваш пароль"
                                        labelClass="text-white"
                                        icon="lock"
                                        size="sm"
                                        group
                                        type="password"
                                        validate
                                        required
                                        ref={ ref => this.passwordInput = ref}
                                    />
                                    </div>
                                    <MDBAlert  color='danger' className={error ? '' : ' sr-only'} dismiss>Проверьте правильность введённых данных</MDBAlert>
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