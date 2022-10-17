import Axios from 'axios';

import { AxiosCustomRequestConfig, AxiosInstanceData } from '../../../types';
import { TestApiDataMap, TestApiUrls } from './realtest-apiData';

const axiosInstance = Axios.create();
axiosInstance.defaults.baseURL = 'http://localhost:5000';

export const helloHttp = async () => {
  return await axiosInstance.post(TestApiUrls.Hello, null, {
    apiDataItem: TestApiDataMap.get(TestApiUrls.Hello),
  } as AxiosCustomRequestConfig);
};

export const TestAxiosData: AxiosInstanceData = {
  instance: axiosInstance,
  DataMap: {
    MapObject: TestApiDataMap,
    Urls: TestApiUrls,
  },
};
