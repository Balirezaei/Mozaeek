import { PayloadAction, createSlice } from '@reduxjs/toolkit';

type CoreSliceState = {
  temp: {
    labels: {
      updatedDate?: number;
    };
  };
};

const initialState: CoreSliceState = {
  temp: {
    labels: {},
  },
};

const coreSlice = createSlice({
  name: 'account',
  initialState: initialState,
  reducers: {
    tempLabelsForceUpdate: (state, action: PayloadAction<number>) => {
      state.temp.labels.updatedDate = action.payload;
    },
  },
});

export default coreSlice;
