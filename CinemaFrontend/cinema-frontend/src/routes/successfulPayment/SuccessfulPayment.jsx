import React from 'react'
import NavBar from '../../components/navbar/NavBar'
import './SuccessfulPayment.css';
import { FaCheckCircle } from "react-icons/fa";

function SuccessfulPayment() {
  return (
    <div>
      <NavBar />
      <div className="navBarSpace">
      </div>
      <div className="successContainer">
        <FaCheckCircle className='successIcon'/>
        <h1>Payment Successful!</h1>
        <p>Get Ready For an Unforgetable Experience</p>
      </div>
    </div>
  )
}

export default SuccessfulPayment
