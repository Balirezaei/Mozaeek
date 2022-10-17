import { refreshAuthToken } from '../account';

export const startAppIntervals = (options: { refreshToken: boolean }) => {
  if (options.refreshToken) {
    setRefreshTokenInterval();
  }
};

//#region StartRefreshToken Interval

let refreshTokenInterval: NodeJS.Timeout;

export const setRefreshTokenInterval = () => {
  refreshTokenInterval = setInterval(() => {
    refreshAuthToken().then();
  }, 1.5 * 60 * 1000);
};

export const clearRefreshTokenInterval = () => {
  if (refreshTokenInterval) {
    clearInterval(refreshTokenInterval);
  }
};
