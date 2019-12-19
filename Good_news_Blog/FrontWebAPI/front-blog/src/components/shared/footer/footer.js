import React from "react";
import { MDBContainer, 
  MDBBtn, 
  MDBInput,
  MDBIcon, 
  MDBFooter} from "mdbreact";
   
 class FooterComponent extends React.Component {

    render() {
        return (
          <MDBFooter className="footer ripe-malinka-gradient text-white">
          <div className="container">
                  <div className="row pt-3">
                      <div className="col-md-5">
                          <h5><i className="fa fa-road"></i> Good news Blog</h5>
                          <div className="row">
                              <div className="col-6">
                                  <ul className="list-unstyled">
                                      <li><a href="#!">Product</a></li>
                                      <li><a href="#!">Benefits</a></li>
                                      <li><a href="#!">Partners</a></li>
                                      <li><a href="#!">Team</a></li>
                                  </ul>
                              </div>
                              <div className="col-6">
                                  <ul className="list-unstyled">
                                      <li><a href="#!">Documentation</a></li>
                                      <li><a href="#!">Support</a></li>
                                      <li><a href="#!">Legal Terms</a></li>
                                      <li><a href="#!">About</a></li>
                                  </ul>
                              </div>
                          </div>
                          <ul className="nav">
                              <li className="nav-item"><a href="#!" className="nav-link pl-0"><MDBIcon size="2x" fab icon="facebook-square" /></a></li>
                              <li className="nav-item"><a href="#!" className="nav-link"><MDBIcon size="2x" fab icon="twitter-square" /></a></li>
                              <li className="nav-item"><a href="#!" className="nav-link"><MDBIcon size="2x" fab icon="github-square" /></a></li>
                              <li className="nav-item"><a href="#!" className="nav-link"><MDBIcon size="2x" fab icon="instagram" /></a></li>
                          </ul>
                          <br/>
                      </div>
                      <div className="col-md-2">
                          <h5 className="text-md-right">Напишите нам</h5>
                          <hr/>
                      </div>
                      <div className="col-md-5 pb-2">
                          
                              <MDBInput className="text-white"
                                    label="Ваш email"
                                    labelClass="text-white"
                                    icon="at"
                                    size="sm"
                                    group
                                    type="email"
                                    validate
                                    error="wrong"
                                    success="right"
                                />
                                <MDBInput className="text-white"
                                    label="Сообщение"
                                    labelClass="text-white"
                                    icon="envelope"
                                    size="sm"
                                    rows="1"
                                    group
                                    type="textarea"
                                    validate
                                    error="wrong"
                                    success="right"
                                />
                                  <MDBBtn outline color='light'>Отправить</MDBBtn>        
                      </div>
                  </div>
              </div>
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