import React from "react";
import { BrowserRouter, Redirect } from 'react-router-dom';
import { authenticationService } from '../services/authenticationService';
import NavbarMain from '../components/shared/navbar/navbarMain';
import { TabContent, 
    TabPane, 
    Nav, 
    NavItem, 
    NavLink, 
    Row, 
    Col } from 'reactstrap';
import classnames from 'classnames';
import { MDBContainer } from "mdbreact";




class AdminPage extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            currentUser: authenticationService.currentUserValue,
            activeTab: '1'
        }
        this.toggle = this.toggle.bind(this);
    }

    componentDidUpdate(prevProps) {
        if(this.props !== prevProps) {
            this.setState({
                currentUser: authenticationService.currentUserValue
            })
        }
    }

    toggle(tab) {
        if (this.state.activeTab !== tab) {
          this.setState({
            activeTab: tab
          });
        }
      }

      updateState = (value) => {
        this.setState({
          currentUser: value
        })
      }

    render() {
        const { currentUser } = this.state;
        const admin = (element) => element === 'admin';

        if(currentUser == null ||(currentUser !== null && !currentUser.roles.some(admin))) {
           return <Redirect to='/' />
        } else {
            return(
                <BrowserRouter>
                    <NavbarMain updateState={this.updateState} />
                    <MDBContainer>
                    <Row style={{marginTop: '15%'}}>
                        <Col xs="6" sm="4" md="4">
                        <Nav tabs vertical pills>
                            <NavItem>
                                <NavLink
                                    className={classnames({active: this.state.activeTab === '1'})}
                                    onClick={() => {
                                    this.toggle('1');
                                    }}
                                >
                                    Список пользователей
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink
                                    className={classnames({active: this.state.activeTab === '2'})}
                                    onClick={() => {
                                    this.toggle('2');
                                    }}
                                >
                                    Список модераторов
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink
                                    className={classnames({active: this.state.activeTab === '3'})}
                                    onClick={() => {
                                    this.toggle('3');
                                    }}
                                >
                                    Список ролей
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink
                                    className={classnames({active: this.state.activeTab === '4'})}
                                    onClick={() => {
                                    this.toggle('4');
                                    }}
                                >
                                    Создать пользователя
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink
                                    className={classnames({active: this.state.activeTab === '5'})}
                                    onClick={() => {
                                    this.toggle('5');
                                    }}
                                >
                                    Создать роль
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink
                                    className={classnames({active: this.state.activeTab === '6'})}
                                    onClick={() => {
                                    this.toggle('6');
                                    }}
                                >
                                   Редактирование прав доступа
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink
                                    className={classnames({active: this.state.activeTab === '7'})}
                                    onClick={() => {
                                    window.location.href = 'http://goodnewsblog.azurewebsites.net/swagger/index.html';
                                    }}
                                >
                                   Перейти на swagger
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink
                                    className={classnames({active: this.state.activeTab === '8'})}
                                    onClick={() => {
                                    window.location.href = 'http://goodnewsblog.azurewebsites.net/api/admin/hangfire';
                                    }}
                                >
                                   Перейти на hangfire
                                </NavLink>
                            </NavItem>
                        </Nav>
                        </Col>
                        <Col xs="6" sm="6" md="6">
                        <TabContent activeTab={this.state.activeTab}>
                            <TabPane tabId="1">
                            <h4>Список пользователей</h4>
                            </TabPane>
                            <TabPane tabId="2">
                            <h4>Список модераторов</h4>
                            </TabPane>
                            <TabPane tabId="3">
                            <h4>Список ролей</h4>
                            </TabPane>
                            <TabPane tabId="4">
                            <h4>Создать пользователя</h4>
                            </TabPane>
                            <TabPane tabId="5">
                            <h4>Создать роль</h4>
                            </TabPane>
                            <TabPane tabId="6">
                            <h4>Редактирование прав доступа</h4>
                            </TabPane>
                        </TabContent>
                        </Col>
                    </Row>
                    </MDBContainer>
                </BrowserRouter>
            )
        }      
    }
}

export default AdminPage;