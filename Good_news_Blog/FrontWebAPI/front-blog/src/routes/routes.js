import React from 'react'
import { Switch, Route, Redirect } from 'react-router-dom'
import { authenticationService } from '../services/authenticationService';

import App from '../App';
import SignInPage from '../pages/signInPage';
import SignUpPage from '../pages/signUpPage';
import ReadNewsPage from '../pages/readNews';
import AdminPage from '../pages/adminPage';

const admin = (element) => element === 'admin';

const Routes = () => (
    <main>
      <Switch>
        <Route exact path='/' component={App}/>      
        <Route path='/login' component={SignInPage}/>   
        <Route path='/register' component={SignUpPage}/>
        <Route exact path='/admin' component={() => {
          return (authenticationService.currentUserValue !== null && authenticationService.currentUserValue.roles.some(admin))
          ?  <AdminPage />
          :  <Redirect to='/' />
        }        
        } />  
        <Route exact path='/admin/swagger' component={() => {
          return (authenticationService.currentUserValue !== null && authenticationService.currentUserValue.roles.some(admin))
          ?  () => {window.location.href = 'http://goodnewsblog.azurewebsites.net/swagger/index.html';}
          :  <Redirect to='/' />           
           }}/>
        <Route exact path='/admin/hangfire' component={() => { 
          return (authenticationService.currentUserValue !== null && authenticationService.currentUserValue.roles.some(admin))
          ?  () => {window.location.href = 'http://goodnewsblog.azurewebsites.net/api/admin/hangfire';}
          :  <Redirect to='/' />          
           }}/>   
        <Route path='/:id' component={ReadNewsPage} />     
      </Switch>
    </main>
  )
  
  export default Routes;