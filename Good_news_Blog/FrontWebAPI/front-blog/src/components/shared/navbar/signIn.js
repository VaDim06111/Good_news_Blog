import React from 'react';
import {  MDBNavbarNav, 
    MDBNavItem, 
    MDBDropdown, 
    MDBDropdownToggle, 
    MDBDropdownMenu, 
    MDBDropdownItem } from "mdbreact";

class SignIn extends React.Component {

    render() {
        return(
        <MDBNavbarNav right>
              <MDBNavItem>
                    <MDBDropdown>
                        <MDBDropdownToggle outline caret color="light">
                            Войти
                        </MDBDropdownToggle>
                    <MDBDropdownMenu basic>
                        <MDBDropdownItem>Вход</MDBDropdownItem>
                        <MDBDropdownItem>Мой профиль</MDBDropdownItem>
                        <MDBDropdownItem>Регистрация</MDBDropdownItem>
                        <MDBDropdownItem>Выйти</MDBDropdownItem>
                        </MDBDropdownMenu>
                    </MDBDropdown>
              </MDBNavItem>
            </MDBNavbarNav>
        )
    }   
}

export default SignIn;