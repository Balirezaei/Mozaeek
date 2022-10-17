import jwtDecode from 'jwt-decode';

import { ClaimNames } from '../constants/claimNames';
import { JwtTokenClaims } from '../types';

export const decodeAppJwt = (token: string): JwtTokenClaims | undefined => {
  try {
    const jwt = jwtDecode<{
      [ClaimNames.Username]: string;
      [ClaimNames.UserData]: string;
      [ClaimNames.UserId]: string;
      [ClaimNames.UserRole]: string;
      [ClaimNames.NotValidBefore]: string;
      [ClaimNames.ExpirationTime]: string;
      [ClaimNames.Issuer]: string;
      [ClaimNames.Audience]: string;
    }>(token);
    return {
      Username: jwt[ClaimNames.Username],
      UserData: jwt[ClaimNames.UserData],
      UserId: jwt[ClaimNames.UserId],
      UserRole: jwt[ClaimNames.UserRole],
      NotValidBefore: jwt[ClaimNames.NotValidBefore],
      ExpirationTime: jwt[ClaimNames.ExpirationTime],
      Issuer: jwt[ClaimNames.Issuer],
      Audience: jwt[ClaimNames.Audience],
    };
  } catch (e) {
    return undefined;
  } finally {
  }
};
