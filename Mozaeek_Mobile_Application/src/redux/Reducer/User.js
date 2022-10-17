import createReducer from './createReducer';
import { OTP_START, OTP_START_SUCCESS,OTP_VERIFY,OTP_VERIFY_FAILED,OTP_VERIFY_SUCCESS } from './types';

const initialState = {
  OTP_status:false,
  OTP_verify:-1,
  message: '',
};

export default createReducer(initialState, {
  [OTP_START_SUCCESS](state, {payload}) {
   console.log('reducer',payload)
   state.OTP_status = true
  },
   [OTP_VERIFY_SUCCESS](state, {payload}) {
   console.log('reducer',payload)
   state.OTP_verify = payload.data
  },[OTP_VERIFY_FAILED](state, {payload}) {
   console.log('reducer',payload)
   state.OTP_verify = false
  },

});

export const OTPStart = (param) => ({type: OTP_START, param});
export const OTPVerify = (param) => ({type: OTP_VERIFY, param});

