import React from 'react';
import { Navigate } from 'react-router-dom';
import Cookies from 'js-cookie';
import {jwtDecode} from 'jwt-decode';

const RequireAuth = ({ children, redirectTo }) => {
  const token = Cookies.get('token');

  if (token) {
    const decodedToken = jwtDecode(token);
    const userRole = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    if (userRole === 'Worker') {
      return children;
    }
  }

  return <Navigate to={redirectTo} />;
};

export default RequireAuth;
