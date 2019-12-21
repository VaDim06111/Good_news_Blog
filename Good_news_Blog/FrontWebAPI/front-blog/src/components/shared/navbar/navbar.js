import React from "react";
import { BrowserRouter } from "react-router-dom";
import { MDBNavbar, 
    MDBNavbarBrand, 
    MDBNavbarNav, 
    MDBNavItem,  
    MDBNavbarToggler, 
    MDBCollapse, 
    MDBMask,
    MDBRow, 
    MDBCol, 
    MDBIcon, 
    MDBBtn, 
    MDBView, 
    MDBContainer} from "mdbreact";
import AnchorLink from 'react-anchor-link-smooth-scroll';   
import "./navbar.css";
import SignIn from "./signIn";
import { authenticationService } from '../../../services/authenticationService';

class NavbarComponent extends React.Component {
state = {
collapseID: "",
currentUser: authenticationService.currentUserValue
};

toggleCollapse = collapseID => () =>
this.setState(prevState => ({
collapseID: prevState.collapseID !== collapseID ? collapseID : ""
}));

componentDidUpdate(prevProps) {
  if(this.props !== prevProps) {
    this.setState({
      currentUser: authenticationService.currentUserValue
    })
  }
}
render() {
const overlay = (
<div id="sidenav-overlay" style={{ backgroundColor: "transparent" }} onClick={this.toggleCollapse("navbarCollapse")} />
);

const admin = (element) => element === 'admin';

return (
<div id="videobackground">
  <BrowserRouter>
    <div>
      <MDBNavbar color='purple-gradient' dark expand="md" fixed="top"scrolling transparent>
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
      </MDBNavbar>
      {this.state.collapseID && overlay}
    </div>
  </BrowserRouter>

  <MDBView>
    <video className="video-intro" poster="https://mdbootstrap.com/img/Photos/Others/background.jpg" playsInline
      autoPlay muted="" loop>
      <source src="https://mdbootstrap.com/img/video/animation.mp4" type="video/mp4" />
    </video>
    <MDBMask className="d-flex justify-content-center align-items-center gradient">
      <MDBContainer className="px-md-3 px-sm-0">
        <MDBRow>
          <MDBCol md="12" className="mb-4 white-text text-center">
            <h3 className="display-3 font-weight-bold mb-0 pt-md-5">
              Лучшие новости Гродно
            </h3>
            <hr className="hr-light my-4 w-75" />
            <h4 className="subtext-header mt-2 mb-4">
              Самые свежие и позитивные новости из вашего любимого города!
            </h4>
            <AnchorLink href='#news'>
              <MDBBtn outline rounded color="white">
                Читать <MDBIcon icon="angle-double-down" />
              </MDBBtn>
            </AnchorLink>
          </MDBCol>
        </MDBRow>
      </MDBContainer>
    </MDBMask>
  </MDBView>
</div>
);
}
}

export default NavbarComponent;