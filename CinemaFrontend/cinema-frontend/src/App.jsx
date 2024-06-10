import './App.css'
import NavBar from './components/navbar/NavBar'
import LandingVideo from './components/landing-video/LandingVideo'
import api from './api/api'
import { useState, useEffect } from 'react'


function App() {

    const [movies, setMovies] = useState([]);
    const [movie, setMovie] = useState([]);

    useEffect(() => {
    
        fetchMovies();
    
    }, []);

    const fetchMovies = async() => {

        const res = await api.get('Movie');

        setMovie(res.data[0]);
        setMovies(res.data);

    }

    return(
        <div>
            <NavBar/>
            <LandingVideo movie={movie}/>
        </div>

    );
}

export default App
