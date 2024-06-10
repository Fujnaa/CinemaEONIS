import React from 'react'
import './Login.css'
import NavBar from '../../components/navbar/NavBar'
import { FaUser, FaLock } from "react-icons/fa";
import { useState, useEffect } from 'react';
import api from '../../api/api'
import Cookies from 'js-cookie';
import { Link } from 'react-router-dom';

function Login() {

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [token, setToken] = useState('');

  const loginUser = () => {

    const res = api.post('Auth/Login', {userEmailAdress: email, userPassword: password})
    .then(response => {Cookies.set('token', response.data, { expires: 7, secure: true });
    window.location.reload();
    })
    .catch(error => {console.error('Error: ', error)});

  }

  return (
    <div>
      <NavBar />
      <div className="navBarSpace">
      </div>
      <div className='loginContainer'>
        <div className='wrapper'>
            <form action="">
                <h1>Login</h1>
                <div className="input-box">
                    <input type='text' placeholder='E-Mail' value={email} onChange={(ev) => setEmail(ev.target.value)} required />
                    <FaUser className='icon'/>
                </div>
                <div className='input-box'>
                    <input type='password' placeholder='Password' value={password} onChange={(ev) => setPassword(ev.target.value)} required />
                    <FaLock className='icon'/>
                </div>

                <div className="remember-forgot">
                    <label><input type='checkbox' />Remember me</label>
                    <a href="#">Forgot Password</a>
                </div>

                <button type="button" onClick={() => loginUser()}>Login</button>

                <div className="register-link">
                    <p>Don't have an account? <Link to="/register">Register</Link></p>
                </div>
            </form>
        </div>
      </div>


    </div>
  )
}

export default Login
