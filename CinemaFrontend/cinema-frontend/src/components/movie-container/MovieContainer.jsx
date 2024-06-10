import React from 'react'
import './MovieContainer.css'
import { Link } from 'react-router-dom'

function MovieContainer(props) {
  return (
    <div className="movieContainer">
        <Link to={"/tickets/" + props.movie.movieTitle}>
        <img src={props.movie.moviePoster} />
        <div className="detailsContainer">
            <p className="movieTitle">{props.movie.movieTitle}</p>
            <p className="movieDetails">{props.movie.movieReleaseDate} - {props.movie.movieGenre} - {props.movie.movieLength}min</p>
            <div className="ratingsContainer">
            <div>
                <p className="movieSynopsys">7/10</p>
                <p className="movieDetails">IMDB</p>
            </div>
            <div>
                <p className="movieSynopsys">95%</p>
                <p className="movieDetails">AudienceScore</p>
            </div>
            <div>
                <p className="movieSynopsys">92%</p>
                <p className="movieDetails">TomatoMeter</p>
            </div>

            </div>
            <div className="synopsysContainer">
            <p className="movieSynopsys">Short Desc.</p>
            <p className="movieDetails">{props.movie.movieDesc}</p>
            </div>
        </div>
        </Link>
        </div>
  )
}

export default MovieContainer
