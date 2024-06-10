import React from 'react'
import NavBar from '../../components/navbar/NavBar'

import api from '../../api/api';
import { useState, useEffect } from 'react';
import Cookies from 'js-cookie'
import './Tickets.css';
import EnchancedTable from '../../components/enchancedTable/EnchancedTable';



function Transactions() {

  const [rows, setRows] = useState([]);
  const [token, setToken] = useState('');
  const [loading, setLoading] = useState(true);

    useEffect(() => {
      
      setToken(Cookies.get('token'));
      fetchTickets();
    
    }, []);

    useEffect(() => {
          fetchTickets();
  }, [token]);


    const fetchTickets = async() => {

    let config = {
        headers: { Authorization: `Bearer ${Cookies.get('token')}` }
    };

      const res = await api.get('Ticket/Admin',
        config
      ).catch(console.log);
      setRows(await res.data);

      setLoading(false);
    }
      
    const headCells = [

      {
        id: 'ticketId',
        numeric: false,
        disablePadding: false,
        label: 'ID',
      },
      {
        id: 'ticketType',
        numeric: false,
        disablePadding: false,
        label: 'Type',
      },
      {
        id: 'ticketSeat',
        numeric: true,
        disablePadding: false,
        label: 'Seat',
      },
      {
        id: 'ticketPrice',
        numeric: true,
        disablePadding: false,
        label: 'Price',
      }
    ];



  return (
    <div>
      <NavBar />
      <div className="navBarSpace">
      </div>

      {loading ? <div className='loader'></div> : 
      
      <div className='tableContainer' >
        <EnchancedTable 
          rows = {rows}
          headCells = {headCells}
          token = {token}
          apiRoute = 'Ticket'
        />
    </div>
      
      
      }

      
    </div>
  )
}

export default Transactions
