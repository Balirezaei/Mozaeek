import {put, takeLatest, all, call, takeEvery} from 'redux-saga/effects';
import API from './Api'
import NetInfo from '@react-native-community/netinfo';

export function* callApis(action) {
    const {isInternetReachable} = yield call([NetInfo, NetInfo.fetch]);
    if (isInternetReachable) {
      try {
        // yield put(setLoading({value: true, type: action.type}));
        let result = {};
        switch (action.param.apiType) {
          case 1:
            result = yield call(
              API.post,
              action.param.path,
              action.param.params,
            );
            break;
          case 2:
            result = yield call(
              API.put,
              action.param.path,
              action.param.params,
            );
            break;
            case 3 : 
            result = yield call(
              API.delete,
              action.param.path,
              action.param.params,
            );
            break;
          default:
            result = yield call(
              API.get,
              action.param.path,
              action.param.params,
            );
            break;
        }
  
        console.log(result)
        if (result.status === 200)
          yield put({type: action.type + '_SUCCESS', payload: result.data});
        else  if(result.status === 404){
          yield put({type: INVALID_API_KEY});
        }else{
          if (action.type + '_FAILED' !== undefined)
            yield put({type: action.type + '_FAILED', payload: result});
          // else yield put(getDataFailed());
        }
      } catch (err) {
       console.error(err)
        // yield put(getDataFailed());
        yield put({type: action.type + '_FAILED', payload: ''});
      } finally {
        // yield put(setLoading({value: false, type: action.type}));
        yield put({type: action.type + '_FAILED', payload: 'result'});
      }
    } else {
      // yield put(setNetworkError());
      yield put({type: action.type + '_FAILED', payload: 'result'});
    }
  }