import React from "react";
import { MDBNavbar, 
    MDBNavbarBrand, 
    MDBNavbarNav, 
    MDBNavItem,   
    MDBBtn,
    MDBNavbarToggler,
    MDBCollapse, 
    MDBContainer} from "mdbreact";   
import SignIn from "./signIn";
import { authenticationService } from '../../../services/authenticationService';

class NavbarMain extends React.Component {
state = {
    collapseID: "",
    currentUser: authenticationService.currentUserValue
};

componentDidUpdate(prevProps) {
    if(this.props !== prevProps) {
        this.setState({
            currentUser: authenticationService.currentUserValue
        })
    }
}

toggleCollapse = collapseID => () =>
this.setState(prevState => ({
collapseID: prevState.collapseID !== collapseID ? collapseID : ""
}));

    render() {
        const overlay = (
            <div id="sidenav-overlay" style={{ backgroundColor: "transparent" }} onClick={this.toggleCollapse("navbarCollapse")} />
            );

            const admin = (element) => element === 'admin';
            
        return(
        <MDBNavbar color='purple-gradient' dark expand="md" fixed="top">
            <MDBContainer>
            <MDBNavbarBrand>
                <a href="/"><span className="white-text">Good news Blog</span></a>
            </MDBNavbarBrand>
            <MDBNavbarToggler onClick={this.toggleCollapse("navbarCollapse")} />
          <MDBCollapse id="navbarCollapse" isOpen={this.state.collapseID} navbar>
                <MDBNavbarNav left>
                <MDBNavItem className={'m-1' + (this.state.currentUser !== null && this.state.currentUser.roles.some(admin) ? '' : ' sr-only')}>
                <a href="/admin"><MDBBtn outline color="white">Панель администратора</MDBBtn></a>               
                </MDBNavItem>
                </MDBNavbarNav>
                <SignIn updateState={this.props.updateState}/>
                </MDBCollapse>
            </MDBContainer>           
            {this.state.collapseID && overlay}
        </MDBNavbar>  
        )
    }
}

export default NavbarMain;
 