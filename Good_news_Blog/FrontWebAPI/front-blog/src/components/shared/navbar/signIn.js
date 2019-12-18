import React from 'react';
import {  MDBNavbarNav, 
    MDBNavItem, 
    MDBDropdown, 
    MDBDropdownToggle, 
    MDBDropdownMenu, 
    MDBDropdownItem } from "mdbreact";

import { authenticationService } from '../../../services/authenticationService';

class SignIn extends React.Component {
    
    constructor(props) {
        super(props);
        this.state = {
            currentUser: authenticationService.currentUserValue,
            isAuthorize: false
        }
        this.logout = this.logout.bind(this);
    }

    componentDidMount() {
        if(this.state.currentUser !==null) {
            this.setState({
                isAuthorize: true
            })
        }
    }

    componentDidUpdate(props) {
        if(this.props.isAuthorize !== props.isAuthorize) {
            if(this.state.currentUser !==null) {
                this.setState({
                    isAuthorize: true
                })
            } else {
                this.setState({
                    isAuthorize: false
                })
            }
        }
    }

    logout() {
        authenticationService.logout();
        this.setState({
            isAuthorize: false
        })
    }


    render() {
        const { isAuthorize } = this.state;

        if(isAuthorize) {
            return (
                <MDBNavbarNav right>
                    <MDBNavItem>
                            <MDBDropdown>
                                <MDBDropdownToggle outline caret color="light">
                                    authorize
                                </MDBDropdownToggle>
                                <MDBDropdownMenu basic>                             
                                <a href="#!" style={{margin:'0', padding:'0'}}><MDBDropdownItem>Мой профиль</MDBDropdownItem></a>
                                <a href="#!" onClick={this.logout} style={{margin:'0', padding:'0'}}><MDBDropdownItem>Выход</MDBDropdownItem></a>
                                </MDBDropdownMenu>
                            </MDBDropdown>
                    </MDBNavItem>
                </MDBNavbarNav> 
            )
        } else {
            return(
                <MDBNavbarNav right>
                    <MDBNavItem>
                            <MDBDropdown>
                                <MDBDropdownToggle outline caret color="light">
                                    Войти
                                </MDBDropdownToggle>
                                <MDBDropdownMenu basic>
                                <a href="/login" style={{margin:'0', padding:'0'}}><MDBDropdownItem>Вход</MDBDropdownItem></a>                               
                                <a href="/register" style={{margin:'0', padding:'0'}}><MDBDropdownItem>Регистрация</MDBDropdownItem></a>
                                </MDBDropdownMenu>
                            </MDBDropdown>
                    </MDBNavItem>
                </MDBNavbarNav> 
        )
        }
       
    }   
}

export default SignIn;