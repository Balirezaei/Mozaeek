import { put } from 'redux-saga/effects';

import { handleHttpErrorDefaultSaga } from '../../app/modules/shared';
import { isAxiosError } from '../../utils/type-guards/axios-guard';

export function* handleHttpResponseDefault(response: any, handleError: boolean = false) {
  if (!response) {
    return undefined;
  }
  if (handleError && isAxiosError(response)) {
    yield put(handleHttpErrorDefaultSaga(response));
    return response;
  } else {
    return response;
  }
}
