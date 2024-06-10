import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import './index.css'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import BuyTickets from './routes/buy-tickets/BuyTickets.jsx'
import Contact from './routes/contact/Contact.jsx'
import SelectedMovie from './routes/selectedMovie/SelectedMovie.jsx'
import Login from './routes/login/Login.jsx'
import Register from './routes/register/Register.jsx'
import SuccessfulPayment from './routes/successfulPayment/SuccessfulPayment.jsx'
import CancelledPayment from './routes/cancelledPayment/CancelledPayment.jsx'
import Tickets from './routes/transactions/Tickets.jsx'
import RequireAuth from './RequireAuth.jsx'
const router = createBrowserRouter([
  { path: "/", element: <App /> },
  { path: "/tickets", element: <BuyTickets /> },
  { path: "/contact", element: <Contact /> },
  { path: "/login", element: <Login /> },
  { path: "/register", element: <Register /> },
  {
    path: "/admin/tickets",
    element: (
      <RequireAuth redirectTo="/login">
        <Tickets />
      </RequireAuth>
    ),
  },
  { path: "/tickets/:title", element: <SelectedMovie /> },
  { path: "/paymentSuccess", element: <SuccessfulPayment /> },
  { path: "/paymentCancelled", element: <CancelledPayment /> },
]);

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>,
);