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
                        <a href="/login" style={{margin:'0', padding:'0'}}><MDBDropdownItem>Вход</MDBDropdownItem></a>
                        <a href="!#" style={{margin:'0', padding:'0'}}><MDBDropdownItem>Мой профиль</MDBDropdownItem></a>
                        <a href="!#" style={{margin:'0', padding:'0'}}><MDBDropdownItem>Регистрация</MDBDropdownItem></a>
                        <a href="!#" style={{margin:'0', padding:'0'}}><MDBDropdownItem>Выход</MDBDropdownItem></a>
                        </MDBDropdownMenu>
                    </MDBDropdown>
              </MDBNavItem>
            </MDBNavbarNav>  
        )
    }   
}

export default SignIn;