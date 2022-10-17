import { createStore, combineReducers, applyMiddleware } from 'redux';
import createSagaMiddleware from 'redux-saga'
import { createLogger } from 'redux-logger';

import rootSaga from './Saga'
import rootReducer from './Reducer'

const sagaMiddleware = createSagaMiddleware()

const middleware = [sagaMiddleware]

if (__DEV__) {
    middleware.push(createLogger());
}
const store  = createStore(rootReducer, {},applyMiddleware(...middleware));


    sagaMiddleware.run(rootSaga)
export {store}