import React from "react";
import { MDBCol, 
    MDBContainer, 
    MDBRow, 
    MDBFooter } from "mdbreact";
import FormContact from './formContact';    

 class FooterComponent extends React.Component {

    render() {
        return (
            <MDBFooter color="ripe-malinka-gradient" className="font-small pt-4 mt-4">
              <MDBContainer fluid className="text-center text-md-left">
                <MDBRow>
                  <MDBCol md="4">
                    <h5 className="title">Footer Content</h5>
                    <p>
                      Here you can use rows and columns here to organize your footer
                      content.
                    </p>
                  </MDBCol>
                  <MDBCol md="8">
                    <FormContact />
                  </MDBCol>
                </MDBRow>
              </MDBContainer>
              <div className="footer-copyright text-center py-3">
                <MDBContainer fluid>
                   &copy; {new Date().getFullYear()} Copyright: {/*<a href="https://www.MDBootstrap.com"></a> */} Good news Blog
                </MDBContainer>
              </div>
            </MDBFooter>
          );
    }
 }

export default FooterComponent;