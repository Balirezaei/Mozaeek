import {all, fork} from 'redux-saga/effects';
import Info from './Info'
import User from './User'


export default function* root() {
  yield all([ fork(User)]);
}