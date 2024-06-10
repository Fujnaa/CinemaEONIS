import React from 'react'
import { FaRegFaceSadTear } from "react-icons/fa6";
import './CancelledPayment.css';
import NavBar from '../../components/navbar/NavBar';


function CancelledPayment() {
  return (
      <div>
          <NavBar />
          <div className="navBarSpace">
          </div>
          <div className="cancelContainer">
              <FaRegFaceSadTear className='cancelIcon' />
              <h1>Payment Cancelled</h1>
              <p>We'll see you next time!</p>
          </div>
      </div>
  )
}

export default CancelledPayment
