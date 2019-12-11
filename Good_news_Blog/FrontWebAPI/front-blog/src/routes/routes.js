import React from 'react'
import { Switch, Route } from 'react-router-dom'

import App from '../App';
import SignInPage from '../pages/signInPage';
import SignUpPage from '../pages/signUpPage';

const Routes = () => (
    <main>
      <Switch>
        <Route exact path='/' component={App}/>
        <Route path='/login' component={SignInPage}/>
        <Route path='/register' component={SignUpPage}/>
      </Switch>
    </main>
  )
  
  export default Routes;