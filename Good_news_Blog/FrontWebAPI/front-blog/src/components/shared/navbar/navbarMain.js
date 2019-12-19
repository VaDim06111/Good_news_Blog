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

class NavbarMain extends React.Component {
state = {
    collapseID: ""
};

toggleCollapse = collapseID => () =>
this.setState(prevState => ({
collapseID: prevState.collapseID !== collapseID ? collapseID : ""
}));

    render() {
        const overlay = (
            <div id="sidenav-overlay" style={{ backgroundColor: "transparent" }} onClick={this.toggleCollapse("navbarCollapse")} />
            );
        return(
        <MDBNavbar color='purple-gradient' dark expand="md" fixed="top">
            <MDBContainer>
            <MDBNavbarBrand>
                <a href="/"><span className="white-text">Good news Blog</span></a>
            </MDBNavbarBrand>
            <MDBNavbarToggler onClick={this.toggleCollapse("navbarCollapse")} />
          <MDBCollapse id="navbarCollapse" isOpen={this.state.collapseID} navbar>
                <MDBNavbarNav left>
                <MDBNavItem className="m-1">
                    <a href="/"><MDBBtn outline color="white">Новости</MDBBtn></a>             
                </MDBNavItem>
                <MDBNavItem className="m-1">
                <a href="#!"><MDBBtn outline color="white">Панель администратора</MDBBtn></a>               
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
 