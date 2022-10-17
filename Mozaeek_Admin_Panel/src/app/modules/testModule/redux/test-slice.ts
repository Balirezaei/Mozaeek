import { PayloadAction, createSlice } from '@reduxjs/toolkit';

type TestState = {
  counter: number;
  text: string;
};

const testSlice = createSlice({
  name: 'test',
  initialState: { counter: 0 } as TestState,
  reducers: {
    increment: (state, action: PayloadAction<number>) => {
      state.counter = state.counter + action.payload;
    },
    decrement: (state, action: PayloadAction<number>) => {
      state.counter = state.counter - action.payload;
    },
  },
});

export default testSlice;
