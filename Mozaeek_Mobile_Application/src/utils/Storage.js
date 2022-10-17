import AsyncStorage from '@react-native-community/async-storage';

class Storage {
 

  async get(key) {
   

    const accessToken = await AsyncStorage.getItem(key);

    return accessToken || '';
  }

  set(key,value) {
    // this._accessToken = accessToken;
    return AsyncStorage.setItem(key, value);
  }

  clear(key) {
    return AsyncStorage.removeItem(key);
  }
}

export default new Storage();