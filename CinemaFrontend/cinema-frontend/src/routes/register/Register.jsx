import React from 'react'
import NavBar from '../../components/navbar/NavBar'
import { FaUser, FaLock, FaPhone } from "react-icons/fa";
import { IoMail } from "react-icons/io5";
import './Register.css';
import { useState } from 'react';
import api from '../../api/api';

function Register() {

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [username, setUsername] = useState('');

    const registerUser = () => {

        const res = api.post('Auth/Register', {userName: username, userEmailAdress: email,
            userPhoneNumber: phoneNumber, userPassword: password})
        .then(response => {console.log(response.data);
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
                <h1>Register</h1>
                <div className="input-box">
                    <input type='text' placeholder='Username' value={username} onChange={(ev) => setUsername(ev.target.value)} required />
                    <FaUser className='icon'/>
                </div>
                <div className="input-box">
                    <input type='text' placeholder='E-Mail' value={email} onChange={(ev) => setEmail(ev.target.value)} required />
                    <IoMail className='icon'/>
                </div>
                <div className="input-box">
                    <input type='text' placeholder='Phone Number' value={phoneNumber} onChange={(ev) => setPhoneNumber(ev.target.value)} required />
                    <FaPhone className='icon'/>
                </div>
                <div className='input-box'>
                    <input type='password' placeholder='Password' value={password} onChange={(ev) => setPassword(ev.target.value)} required />
                    <FaLock className='icon'/>
                </div>

                <button type="sumbit" onClick={() => registerUser()}>Register</button>
            </form>
        </div>
      </div>


    </div>
  )
}

export default Register


