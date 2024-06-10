import React from 'react'

function Showcase() {
    return (
        <ul className="ShowCase">
        <li>
            <span className="seat" /> <small>N/A</small>
        </li>
        <li>
            <span className="seat selected" /> <small>Selected</small>
        </li>
        <li>
            <span className="seat occupied" /> <small>Occupied</small>
        </li>
        <li>
            <span className="seat vip" /> <small>VIP</small>
        </li>
        </ul>
    )
}

export default Showcase


