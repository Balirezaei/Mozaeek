import { combineReducers } from '@reduxjs/toolkit';

import { accountSlice } from '../../app/modules/account';
import { authenticationSlice } from '../../app/modules/authentication';
import { coreSlice } from '../../app/modules/core';
import { sharedSlice } from '../../app/modules/shared';
import { testSlice } from '../../app/modules/testModule';

const rootReducer = () => {
  return combineReducers({
    shared: sharedSlice.reducer,
    test: testSlice.reducer,
    authentication: authenticationSlice.reducer,
    account: accountSlice.reducer,
    core: coreSlice.reducer,
  });
};

export default rootReducer;
