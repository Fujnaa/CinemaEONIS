import React from 'react'

function Screening({ screenings, screening, onChange }) {



    return (
        <div className="Movies">
          <label htmlFor="movie">Pick a date</label>
          <select
            id="movie"
            className="screeningSelect"
            value={screening.screeningDate}
            onChange={e => {
              onChange(screenings.find(selectedScreening => selectedScreening.screeningDate === e.target.value))
            }}
          >
            {screenings.map(screening => (
              <option key={screening.screeningDate} value={screening.screeningDate}>
                {screening.screeningDate}
              </option>
            ))}
          </select>
        </div>
      )
}

export default Screening
