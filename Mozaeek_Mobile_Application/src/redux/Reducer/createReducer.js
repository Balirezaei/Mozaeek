import produce, {enableES5, } from 'immer';
import { RESET } from './types';

// performance
// setAutoFreeze(__DEV__);

export default (initialState, handlers) => {
  return (state = initialState, action) => {
    if (handlers[action.type] !== undefined) {
      switch (action.type) {
        case RESET:
          enableES5()

          return produce(state, (draftState) => initialState);

        default:
          enableES5()

          return produce(state, (draftState) =>
            handlers[action.type](draftState, action),
          );
      }
    }
    return state;
  };
};
