import { AxiosError, AxiosResponse } from 'axios';

export const parseAxiosResponse = (response: AxiosResponse | AxiosError) => {
  const errorResponse = response as AxiosError;
  if (errorResponse.isAxiosError) {
  } else {
    return response;
  }
};
