import {all, delay, put, takeLatest} from 'redux-saga/effects';
import { OTP_START, OTP_VERIFY } from '../Reducer/types';
import {callApis} from '../../api/CallApis'

export default function* root() {
    yield all([
        yield takeLatest(OTP_START,callApis),
        yield takeLatest(OTP_VERIFY,callApis),

    ]);
  }