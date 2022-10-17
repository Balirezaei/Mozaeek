import {combineReducers} from 'redux';
import Info from './Info';
import User from './User'

const rootReducers = combineReducers({
   user: User,
   info: Info
    
  });
  
  export default rootReducers;