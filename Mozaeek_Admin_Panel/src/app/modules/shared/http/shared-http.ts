import Axios from 'axios';

import { AxiosInstanceData } from '../../../../types';
import { isDevelopment } from '../../../../utils/helpers';
import { SharedApiDataMap, SharedApiUrls } from './shared-apiData';

const axiosInstance = Axios.create();

if (isDevelopment()) {
  // const mock = new MockAdapter(axiosInstance, { onNoMatch: 'passthrough', delayResponse: 0 });
  // sharedMock(mock);
}

export const SharedAxiosData: AxiosInstanceData = {
  instance: axiosInstance,
  DataMap: {
    MapObject: SharedApiDataMap,
    Urls: SharedApiUrls,
  },
};
