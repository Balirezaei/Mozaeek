import { AxiosError } from 'axios';
import React from 'react';

import { useAppSelector } from '../../../../../features/hooks';
import { AppResponse } from '../../../../../types';

function HttpError() {
  const httpError = useAppSelector<AxiosError<AppResponse> | undefined>((state) => state.shared.httpError);
  let toRender = false;

  let message = null;
  if (httpError) {
    toRender = true;
    try {
      //message = httpError.response?.data.error?.message;
    } catch {
      message = 'Unknown error!';
    }
  }

  return toRender ? (
    <>
      <h1>{message}</h1>
    </>
  ) : null;
}

export default HttpError;
