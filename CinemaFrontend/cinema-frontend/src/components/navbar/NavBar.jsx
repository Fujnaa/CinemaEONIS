import React from 'react'
import { useState, useEffect } from 'react'
import "./NavBar.css"
import logoPic from "../../assets/ParagonLogoFast.png"
import { Link } from 'react-router-dom'
import Cookies from 'js-cookie';
import {jwtDecode} from 'jwt-decode';


function NavBar() {

  const [token, setToken] = useState('');
  const [userRole, setUserRole] = useState('');



  useEffect(() => {
    
      setToken(Cookies.get('token'));

      if(Cookies.get('token') !== ''){
        const decodedToken = jwtDecode(Cookies.get('token'));
        setUserRole(decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);
      }

  }, []);

  const logout = () => {
    if(token !== ''){
      Cookies.set('token', '')
      setToken('');
    }
  }

  return (
    <div className="container">
      <div className="logoContainer">
        <img src={logoPic}></img>
        <h1>PARAG0N_C1NEM4</h1>
      </div>

      <nav>
        <ul>
          <li><Link to="/" className="link">Home</Link></li>
          <li><Link to="/tickets" className="link">Buy Tickets</Link></li>
          {userRole == 'Worker' ? 
          <li><Link to="/admin/tickets" className="link">Admin</Link></li>
            :<></>
          }
          {token === '' ?
          <li><Link to="/login">Login</Link></li> :
          <li><Link to="/" onClick={() => logout()}>Logout</Link></li>
          }
        </ul>
      </nav>
    </div>
  )
}

export default NavBar
