import React from "react";
import { MDBContainer, 
    MDBRow, 
    MDBCol, 
    MDBInput, 
    MDBBtn,
    MDBAlert } from 'mdbreact';
import { BrowserRouter, Redirect } from 'react-router-dom';
import NavbarMain from '../components/shared/navbar/navbarMain';
import Footer from '../components/shared/footer/footer';

class SignUpPage extends React.Component {

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

        let userName = this.userNameInput.state.innerValue;
        let email = this.emailInput.state.innerValue;
        let password = this.passwordInput.state.innerValue;
        let confirmPassword = this.confirmPasswordInput.state.innerValue;

        if(password === confirmPassword) {   
            try {
                const requestOptions = {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(password)
                };

                fetch(`https://goodnewsblog.azurewebsites.net/api/register?userName=${userName}&email=${email}`, requestOptions)
                .then(this.setState({
                    redirect: true
                }));
              
            }
            catch(ex) {
                this.setState({
                    error: true
                })
            }
                
        } else {
            this.setState({
                error: true
            })
        }
    };

    render() {
        const { redirect, error } = this.state;

        if(redirect) {
            return <Redirect to="/" />;
        } else {
            return(
                <BrowserRouter>
                <NavbarMain />
                <div className="purple lighten-2 mt-0 pt-0" style = {{height:"85vh"}}>
                <MDBContainer>
                    <MDBRow className="text-white">
                        <MDBCol md="6" className="mx-auto" style={{marginTop:'10%'}}>
                        <form onSubmit={this.submitHandler}>
                            <p className="h5 text-center mb-4">Регистрация</p>
                            <div className="grey-text">
                            <MDBInput className="text-white" id='userNameInput'
                                label="Ваше логин (английские символы)"
                                labelClass="text-white"
                                icon="user"
                                size="sm"
                                group
                                type="text"
                                validate
                                error="wrong"
                                success="right"
                                ref={ref => this.userNameInput = ref}
                            />
                            <MDBInput className="text-white" id='emailInput'
                                label="Ваш email"
                                labelClass="text-white"
                                icon="envelope"
                                size="sm"
                                group
                                type="email"
                                validate
                                error="wrong"
                                success="right"
                                ref={ref => this.emailInput = ref}
                            />
                            <MDBInput className="text-white" id='passwordInput'
                                label="Ваш пароль"
                                labelClass="text-white"
                                icon="lock"
                                size="sm"
                                group
                                type="password"
                                validate
                                ref={ref => this.passwordInput = ref}
                            />
                            <MDBInput className="text-white" id='confirmPasswordInput'
                                label="Подтвердите ваш пароль"
                                labelClass="text-white"
                                icon="exclamation-triangle"
                                size="sm"
                                group
                                type="password"
                                validate
                                error="wrong"
                                success="right"
                                ref={ref => this.confirmPasswordInput = ref}
                            />
                            </div>
                            <MDBAlert  color='danger' className={error ? '' : ' sr-only'} dismiss>Проверьте правильность введённых данных</MDBAlert>
                            <div className="text-center">
                            <MDBBtn type='submit'>Зарегистрироваться</MDBBtn>
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

export default SignUpPage;