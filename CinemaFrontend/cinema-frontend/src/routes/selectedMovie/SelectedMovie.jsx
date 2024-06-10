import React from 'react'
import NavBar from '../../components/navbar/NavBar'
import { useParams } from 'react-router-dom';
import {useState, useEffect} from 'react'
import './SelectedMovie.css'
import Cinema from '../../components/cinema/Cinema'
import ShowCase from '../../components/showcase/Showcase.jsx';
import Screening from '../../components/screening/Screening';
import api from '../../api/api.js';
import Cookies from 'js-cookie';
import {loadStripe} from '@stripe/stripe-js'
import {jwtDecode} from 'jwt-decode'

function SelectedMovie() {

  var { title } = useParams();
  title = title.toString().replace('%20', ' ');

  const [selectedMovieBefore, setSelectedMovieBefore] = useState({})
  const [selectedScreening, setSelectedScreening] = useState({});
  const [tickets, setTickets] = useState([]);
  const [token, setToken] = useState('');
  const [userEmail, setUserEmail] = useState('');

  useEffect(() => {   
    
    fetchMovie();
    setToken(Cookies.get('token'));
    if (token) {
      const decoded = jwtDecode(token);
      setUserEmail(decoded[["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"]]);

  }
    
  }, []);

    const updateTickets = async(selectedScreening) => {
      
    
    const result = await api.get('Screening/' + selectedScreening.screeningId);
    console.log(result.data);
    setSelectedScreening(await result.data);
    setTickets(await result.data.tickets);

  }

  const makePayment = async() => {

    const stripe = await loadStripe('pk_test_51PPWyl06lQBVcsANfaYKyyZkCpeQvUGEpzraiwXeLVVyb0tJKEK6MnGo6gdLM78tgska3mZZFmZRliG54VgYmvDY00pbWZr2AN');
    const products = [];

    selectedSeats.forEach(seat => {

      if(seat > 47) {

        let product = {
          amount: 12,
          name: seat + ' Seat, VIP Ticket for ' + selectedMovieBefore.movieTitle + ', ' + selectedScreening.screeningDate,
          quantity: 1
        }
        products.push(product);

      } else {

        let product = {
          amount: 10,
          name: seat + ' Seat, Standard Ticket for ' + selectedMovieBefore.movieTitle + ', ' + selectedScreening.screeningDate,
          quantity: 1
        }
        products.push(product);
      }

    });

    console.log(userEmail);

    const response = await api.post('Stripe/Checkout',
      {products: products,
        screeningId: selectedScreening.screeningId,
        customerEmail: userEmail

      });

    const session = await response.data;

    const result = stripe.redirectToCheckout({
      sessionId:session.sessionId
    });

    if(result.error)
      console.log(result.error);
  }


  const fetchMovie = async() => {

    const res = await api.get('Movie/Title/' + title);

    await setSelectedMovieBefore(res.data);
    
    const fetchedScreening = await api.get('Screening/' + res.data.screenings[0].screeningId);

    await setSelectedScreening(fetchedScreening.data);

    await setTickets(fetchedScreening.data.tickets);

    await setScreeningTickets(fetchedScreening.data.tickets);

  }

    const moviePosterBg = {
      backgroundImage: "url(" + selectedMovieBefore.moviePoster + ")",
    }

    const movies = [
      {
        name: 'Avenger',
        price: 10,
        occupied: [20, 21, 30, 1, 2, 8],
      },
      {
        name: 'Joker',
        price: 12,
        occupied: [9, 41, 35, 11, 65, 26],
      },
      {
        name: 'Toy story',
        price: 8,
        occupied: [37, 25, 44, 13, 2, 3],
      },
      {
        name: 'the lion king',
        price: 9,
        occupied: [10, 12, 50, 33, 28, 47],
      },
    ]


    const [selectedMovie, setSelectedMovie] = useState(movies[0])
    const [selectedSeats, setSelectedSeats] = useState([])
    const [screeningTickets, setScreeningTickets] = useState([])
    const [currentPrice, setcurrentPrice] = useState(0);

    useEffect(() => {
      if(selectedSeats != []){

        setcurrentPrice(0);

            selectedSeats.forEach(seat => {
              if(seat > 47)
                setcurrentPrice(currentPrice + selectedMovie.price + 2);
              else
                setcurrentPrice(currentPrice + selectedMovie.price);
            })}
      else
          setcurrentPrice(0);

    }, [selectedSeats]);

  return (
    <div>
      <NavBar />
      <div className="navBarSpace">
      </div>

      <div className="pageContainer">

        <div className="topContainer" >

            <div className="moviePosterBg" style={moviePosterBg}>
            </div>
          <h1>
          {selectedMovieBefore.movieTitle}
          </h1>

          <img className="moviePoster" src={selectedMovieBefore.moviePoster}></img> : 

        </div>

        <div className="bottomContainer">



        <ShowCase />

        {
          selectedMovieBefore.screenings !== undefined ? 

          <Screening
          screenings={selectedMovieBefore.screenings}
          screening={selectedScreening}
          onChange={selectedScreening => {
            setSelectedSeats([]);
            setSelectedScreening(selectedScreening);
            updateTickets(selectedScreening);
          }}
          /> :
          <div></div>
        }

        {
          tickets !== undefined ?

          <Cinema
          tickets={tickets}
          selectedSeats={selectedSeats}
          onSelectedSeatsChange={selectedSeats => setSelectedSeats(selectedSeats)}/>
          
          : <div></div>

        }

        {token === '' ?
        <button className='purchaseButton' disabled>Purchase</button> :
        <button className='purchaseButton' onClick={ () => makePayment()}>Purchase</button>}


        <p className="info">
          You have selected <span className="count">{selectedSeats.length}</span>{' '}
          seats for the price of{' '}
          <span className="total">
            {currentPrice}$
          </span>
        </p>

        </div>

      </div>

    </div>
  )
}

export default SelectedMovie
