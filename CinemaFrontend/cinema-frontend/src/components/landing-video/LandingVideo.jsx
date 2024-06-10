import React from 'react'
import ReactPlayer from 'react-player'
import "./LandingVideo.css"

function LandingVideo(props) {
  return (
    <div className="playerContainer">
      <ReactPlayer url={props.movie.movieTrailer}
                    controls={false}
                    muted={true}
                    playIcon={null}
                    playing={true}
                    loop={true}
                    width='1920px'
                    height='1080px'
                    className="videoPlayer"
                />
    </div>
  )
}

export default LandingVideo
