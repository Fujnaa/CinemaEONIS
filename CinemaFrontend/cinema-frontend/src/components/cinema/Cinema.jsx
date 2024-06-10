import React from 'react'
import { useState } from 'react'
import clsx from 'clsx'

function Cinema({ tickets, selectedSeats, onSelectedSeatsChange }) {

    const seats = Array.from({ length: 8 * 8 }, (_, i) => i)

    const occupiedSeats = [];
    
    tickets.forEach(ticket => {
      occupiedSeats.push(parseInt(ticket.ticketSeat));
    });

    function handleSelectedState(seat) {
        const isSelected = selectedSeats.includes(seat)
        if (isSelected) {
          onSelectedSeatsChange(
            selectedSeats.filter(selectedSeat => selectedSeat !== seat),
          )
        } else {
          onSelectedSeatsChange([...selectedSeats, seat])
        }
      }
    
      return (
        <div className="Cinema">
          <div className="screen" />
    
          <div className="seats">
            {seats.map(seat => {
              const isSelected = selectedSeats.includes(seat)
              const isOccupied = occupiedSeats.includes(seat)
              return (
                <span
                  tabIndex="0"
                  key={seat}
                  className={clsx(
                    'seat',
                    seat > 47 && 'vip',
                    isSelected && 'selected',
                    isOccupied && 'occupied',

                  )}
                  onClick={isOccupied ? null : () => handleSelectedState(seat)}
                  onKeyPress={
                    isOccupied
                      ? null
                      : e => {
                          if (e.key === 'Enter') {
                            handleSelectedState(seat)
                          }
                        }
                  }
                />
              )
            })}
          </div>
        </div>
      )
}

export default Cinema
