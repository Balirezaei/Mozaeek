import axios from 'axios';
import isEmpty from 'lodash/isEmpty';
import Storage from '../utils/Storage';
import { BASE_URL } from '../utils/Constant';



const client = axios.create({baseURL: BASE_URL, json: true});

const call = async (method, url, data = {}) => {
  // const token = await Storage.get(UNIQUE_ID);
  const token = '';

  const headers = {
    Accept: 'application/json',
    'Content-Type': 'application/json',
  };

  if (token !== '') {
    headers.Authorization = `Bearer ${token}`;
  }

  const request = {headers, method, url};

  if (!isEmpty(data)) {
    if (method !== 'get') {
      request.data = data;
    } else {
      request.params = data;
    }
  }

  try {
    const response = await client(request);
    return Promise.resolve(response);
  } catch (error) {
    return Promise.reject(error.response);
  }
};

// const auth = {
//   async signOut(url) {
//     const token = await tokenHelper.get();

//     token.clear();
//     call('post', url);
//   },
// };

export default {
 //...auth,

  delete: (url, data = {}) => call('delete', url, data),
  get: (url, data = {}) => call('get', url, data),
  patch: (url, data = {}) => call('patch', url, data),
  post: (url, data = {}) => call('post', url, data),
  put: (url, data = {}) => call('put', url, data),
};