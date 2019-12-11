import React from 'react';
import {  MDBNavbarNav, 
    MDBNavItem, 
    MDBDropdown, 
    MDBDropdownToggle, 
    MDBDropdownMenu, 
    MDBDropdownItem } from "mdbreact";
import { Link } from 'react-router-dom';

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
                        <MDBDropdownItem><Link to='/login'>Вход</Link></MDBDropdownItem>
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