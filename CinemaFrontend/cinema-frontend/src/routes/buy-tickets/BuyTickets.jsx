import React from 'react';
import NavBar from '../../components/navbar/NavBar';
import './BuyTickets.css';
import MovieContainer from '../../components/movie-container/MovieContainer'
import Axios from 'axios';
import api from '../../api/api';
import { useEffect, useState } from 'react';


function BuyTickets() {

  const [movies, setMovies] = useState([])

  useEffect(() => {
  
    fetchMovies();
  
  }, []);

  const fetchMovies = async() => {

    const res = await api.get('Movie');

    setMovies(res.data);

  }

  return (

    <div>
        <NavBar/>
        <div className="navBarSpace">
        </div>

        <div className="movieList">
            {movies.map((movie, index) => (
            <MovieContainer movie={movie} key={movie + '-' + index}/>
          ))}
          

        </div>


    </div>
  )
}

export default BuyTickets
